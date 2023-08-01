﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using IronCompress;
using Microsoft.IO;
using Parquet.Data;
using Parquet.Encodings;
using Parquet.Meta;
using Parquet.Schema;

namespace Parquet.File {
    class DataColumnWriter {
        private readonly Stream _stream;
        private readonly ThriftFooter _footer;
        private readonly SchemaElement _schemaElement;
        private readonly CompressionMethod _compressionMethod;
        private readonly CompressionLevel _compressionLevel;
        private readonly Dictionary<string, string>? _keyValueMetadata;
        private readonly ParquetOptions _options;
        private static readonly RecyclableMemoryStreamManager _rmsMgr = new RecyclableMemoryStreamManager();

        public DataColumnWriter(
           Stream stream,
           ThriftFooter footer,
           SchemaElement schemaElement,
           CompressionMethod compressionMethod,
           ParquetOptions options,
           CompressionLevel compressionLevel,
           Dictionary<string, string>? keyValueMetadata) {
            _stream = stream;
            _footer = footer;
            _schemaElement = schemaElement;
            _compressionMethod = compressionMethod;
            _compressionLevel = compressionLevel;
            _keyValueMetadata = keyValueMetadata;
            _options = options;
        }

        public async Task<ColumnChunk> WriteAsync(
            FieldPath fullPath, DataColumn column,
            CancellationToken cancellationToken = default) {

            List<Encoding> encoding = new List<Encoding>();
            if(_options.ColumnEncoding.TryGetValue(column.Field.Name, out string? evalue)) {

                if(evalue == Encoding.DELTA_BINARY_PACKED.ToString()) {
                    encoding.Add(Encoding.RLE);
                    encoding.Add(Encoding.BIT_PACKED);
                    encoding.Add(Encoding.DELTA_BINARY_PACKED);

                }
            }

            // Num_values in the chunk does include null values - I have validated this by dumping spark-generated file.
            ColumnChunk chunk = _footer.CreateColumnChunk(
                _compressionMethod, _stream, _schemaElement.Type!.Value, fullPath, column.NumValues,
                _keyValueMetadata, encoding);

            ColumnSizes columnSizes = await WriteColumnAsync(column, _schemaElement,
                cancellationToken);
            //generate stats for column chunk
            chunk.MetaData!.Statistics = column.Statistics.ToThriftStatistics(_schemaElement);

            //the following counters must include both data size and header size
            chunk.MetaData.TotalCompressedSize = columnSizes.CompressedSize;
            chunk.MetaData.TotalUncompressedSize = columnSizes.UncompressedSize;

            return chunk;
        }

        class ColumnSizes {
            public int CompressedSize;
            public int UncompressedSize;
        }

        private async Task CompressAndWriteAsync(
            PageHeader ph, MemoryStream data,
            ColumnSizes cs,
            CancellationToken cancellationToken) {

            using IronCompress.IronCompressResult compressedData = _compressionMethod == CompressionMethod.None
                ? new IronCompress.IronCompressResult(data.ToArray(), Codec.Snappy, false)
                : Compressor.Compress(_compressionMethod, data.ToArray(), _compressionLevel);

            ph.UncompressedPageSize = (int)data.Length;
            ph.CompressedPageSize = compressedData.AsSpan().Length;

            //write the header in
            using var headerMs = new MemoryStream();
            ph.Write(new Meta.Proto.ThriftCompactProtocolWriter(headerMs));
            int headerSize = (int)headerMs.Length;
            headerMs.Position = 0;
            _stream.Flush();
            await headerMs.CopyToAsync(_stream);

            // write data
#if NETSTANDARD2_0
            byte[] tmp = compressedData.AsSpan().ToArray();
            _stream.Write(tmp, 0, tmp.Length);
#else
            _stream.Write(compressedData);
#endif

            cs.CompressedSize += headerSize;
            cs.UncompressedSize += headerSize;

            cs.CompressedSize += ph.CompressedPageSize;
            cs.UncompressedSize += ph.UncompressedPageSize;
        }

        private async Task<ColumnSizes> WriteColumnAsync(DataColumn column,
           SchemaElement tse,
           CancellationToken cancellationToken = default) {

            column.Field.EnsureAttachedToSchema(nameof(column));

            var r = new ColumnSizes();

            /*
             * Page header must preceeed actual data (compressed or not) however it contains both
             * the uncompressed and compressed data size which we don't know! This somehow limits
             * the write efficiency.
             */

            using var pc = new PackedColumn(column);
            pc.Pack(_options.UseDictionaryEncoding, _options.DictionaryEncodingThreshold);

            // dictionary page
            if(pc.HasDictionary) {
                PageHeader ph = _footer.CreateDictionaryPage(pc.Dictionary!.Length);
                using MemoryStream ms = _rmsMgr.GetStream();
                ParquetPlainEncoder.Encode(pc.Dictionary, 0, pc.Dictionary.Length,
                       tse,
                       ms, column.Statistics);

                await CompressAndWriteAsync(ph, ms, r, cancellationToken);
            }

            // data page
            using(MemoryStream ms = _rmsMgr.GetStream()) {
                // data page Num_values also does include NULLs
                PageHeader ph = new PageHeader();

                if(_options.ColumnEncoding.TryGetValue(column.Field.Name, out string? evalue)) {
                    if(evalue == Encoding.DELTA_BINARY_PACKED.ToString()) {
                        ph = _footer.CreateDeltaPage(column.NumValues);

                    } else {
                        throw new Exception($"Not Supported encoding {evalue}");
                    }
                } else {
                    ph = _footer.CreateDataPage(column.NumValues, pc.HasDictionary);
                }



                if(pc.HasRepetitionLevels) {
                    WriteLevels(ms, pc.RepetitionLevels!, pc.RepetitionLevels!.Length, column.Field.MaxRepetitionLevel);
                }
                if(pc.HasDefinitionLevels) {
                    WriteLevels(ms, pc.DefinitionLevels!, column.DefinitionLevels!.Length, column.Field.MaxDefinitionLevel);
                }

                if(pc.HasDictionary) {
                    // dictionary indexes are always encoded with RLE
                    int[] indexes = pc.GetDictionaryIndexes(out int indexesLength)!;
                    int bitWidth = pc.Dictionary!.Length.GetBitWidth();
                    ms.WriteByte((byte)bitWidth);   // bit width is stored as 1 byte before encoded data
                    RleBitpackedHybridEncoder.Encode(ms, indexes.AsSpan(0, indexesLength), bitWidth);
                } else if(_options.ColumnEncoding.TryGetValue(column.Field.Name, out string? value)) {

                    if(value == Encoding.DELTA_BINARY_PACKED.ToString()) {

                        Array data = pc.GetPlainData(out int offset, out int count);
                        DeltaBinaryPackedEncoder.Encode(data, ms, column.Statistics);
                        ParquetPlainEncoder.FillStats((int[])data, column.Statistics);

                    } else {
                        throw new Exception($"Not Supported encoding {value}");
                    }

                } else {
                    Array data = pc.GetPlainData(out int offset, out int count);
                    ParquetPlainEncoder.Encode(data, offset, count, tse, ms, pc.HasDictionary ? null : column.Statistics);
                }

                ph.DataPageHeader!.Statistics = column.Statistics.ToThriftStatistics(tse);
                await CompressAndWriteAsync(ph, ms, r, cancellationToken);
            }

            return r;
        }

        private static void WriteLevels(Stream s, Span<int> levels, int count, int maxValue) {
            int bitWidth = maxValue.GetBitWidth();
            RleBitpackedHybridEncoder.EncodeWithLength(s, bitWidth, levels.Slice(0, count));
        }
    }
}