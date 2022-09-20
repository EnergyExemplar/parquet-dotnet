#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
/*
 * <auto-generated>
 * Autogenerated by Thrift Compiler (0.16.0)
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 * </auto-generated>
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
//using Thrift.Transport;
//using Thrift.Transport.Client;
//using Thrift.Transport.Server;
//using Thrift.Processor;


#nullable disable                // suppress C# 8.0 nullable contexts (we still support earlier versions)
#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE1006  // parts of the code use IDL spelling
#pragma warning disable IDE0083  // pattern matching "that is not SomeType" requires net5.0 but we still support earlier versions

namespace Parquet.Thrift
{

  public partial class RowGroup : TBase
  {
    private List<global::Parquet.Thrift.SortingColumn> _sorting_columns;
    private long _file_offset;
    private long _total_compressed_size;
    private short _ordinal;

    /// <summary>
    /// Metadata for each column chunk in this row group.
    /// This list must have the same order as the SchemaElement list in FileMetaData.
    /// 
    /// </summary>
    public List<global::Parquet.Thrift.ColumnChunk> Columns { get; set; }

    /// <summary>
    /// Total byte size of all the uncompressed column data in this row group *
    /// </summary>
    public long Total_byte_size { get; set; }

    /// <summary>
    /// Number of rows in this row group *
    /// </summary>
    public long Num_rows { get; set; }

    /// <summary>
    /// If set, specifies a sort ordering of the rows in this RowGroup.
    /// The sorting columns can be a subset of all the columns.
    /// </summary>
    public List<global::Parquet.Thrift.SortingColumn> Sorting_columns
    {
      get
      {
        return _sorting_columns;
      }
      set
      {
        __isset.sorting_columns = true;
        this._sorting_columns = value;
      }
    }

    /// <summary>
    /// Byte offset from beginning of file to first page (data or dictionary)
    /// in this row group *
    /// </summary>
    public long File_offset
    {
      get
      {
        return _file_offset;
      }
      set
      {
        __isset.file_offset = true;
        this._file_offset = value;
      }
    }

    /// <summary>
    /// Total byte size of all compressed (and potentially encrypted) column data
    /// in this row group *
    /// </summary>
    public long Total_compressed_size
    {
      get
      {
        return _total_compressed_size;
      }
      set
      {
        __isset.total_compressed_size = true;
        this._total_compressed_size = value;
      }
    }

    /// <summary>
    /// Row group ordinal in the file *
    /// </summary>
    public short Ordinal
    {
      get
      {
        return _ordinal;
      }
      set
      {
        __isset.ordinal = true;
        this._ordinal = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool sorting_columns;
      public bool file_offset;
      public bool total_compressed_size;
      public bool ordinal;
    }

    public RowGroup()
    {
    }

    public RowGroup(List<global::Parquet.Thrift.ColumnChunk> columns, long total_byte_size, long num_rows) : this()
    {
      this.Columns = columns;
      this.Total_byte_size = total_byte_size;
      this.Num_rows = num_rows;
    }

    public RowGroup DeepCopy()
    {
      var tmp203 = new RowGroup();
      if((Columns != null))
      {
        tmp203.Columns = this.Columns.DeepCopy();
      }
      tmp203.Total_byte_size = this.Total_byte_size;
      tmp203.Num_rows = this.Num_rows;
      if((Sorting_columns != null) && __isset.sorting_columns)
      {
        tmp203.Sorting_columns = this.Sorting_columns.DeepCopy();
      }
      tmp203.__isset.sorting_columns = this.__isset.sorting_columns;
      if(__isset.file_offset)
      {
        tmp203.File_offset = this.File_offset;
      }
      tmp203.__isset.file_offset = this.__isset.file_offset;
      if(__isset.total_compressed_size)
      {
        tmp203.Total_compressed_size = this.Total_compressed_size;
      }
      tmp203.__isset.total_compressed_size = this.__isset.total_compressed_size;
      if(__isset.ordinal)
      {
        tmp203.Ordinal = this.Ordinal;
      }
      tmp203.__isset.ordinal = this.__isset.ordinal;
      return tmp203;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_columns = false;
        bool isset_total_byte_size = false;
        bool isset_num_rows = false;
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.List)
              {
                {
                  TList _list204 = await iprot.ReadListBeginAsync(cancellationToken);
                  Columns = new List<global::Parquet.Thrift.ColumnChunk>(_list204.Count);
                  for(int _i205 = 0; _i205 < _list204.Count; ++_i205)
                  {
                    global::Parquet.Thrift.ColumnChunk _elem206;
                    _elem206 = new global::Parquet.Thrift.ColumnChunk();
                    await _elem206.ReadAsync(iprot, cancellationToken);
                    Columns.Add(_elem206);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
                isset_columns = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.I64)
              {
                Total_byte_size = await iprot.ReadI64Async(cancellationToken);
                isset_total_byte_size = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.I64)
              {
                Num_rows = await iprot.ReadI64Async(cancellationToken);
                isset_num_rows = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 4:
              if (field.Type == TType.List)
              {
                {
                  TList _list207 = await iprot.ReadListBeginAsync(cancellationToken);
                  Sorting_columns = new List<global::Parquet.Thrift.SortingColumn>(_list207.Count);
                  for(int _i208 = 0; _i208 < _list207.Count; ++_i208)
                  {
                    global::Parquet.Thrift.SortingColumn _elem209;
                    _elem209 = new global::Parquet.Thrift.SortingColumn();
                    await _elem209.ReadAsync(iprot, cancellationToken);
                    Sorting_columns.Add(_elem209);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 5:
              if (field.Type == TType.I64)
              {
                File_offset = await iprot.ReadI64Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 6:
              if (field.Type == TType.I64)
              {
                Total_compressed_size = await iprot.ReadI64Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 7:
              if (field.Type == TType.I16)
              {
                Ordinal = await iprot.ReadI16Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
        if (!isset_columns)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_total_byte_size)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_num_rows)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var tmp210 = new TStruct("RowGroup");
        await oprot.WriteStructBeginAsync(tmp210, cancellationToken);
        var tmp211 = new TField();
        if((Columns != null))
        {
          tmp211.Name = "columns";
          tmp211.Type = TType.List;
          tmp211.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
          {
            await oprot.WriteListBeginAsync(new TList(TType.Struct, Columns.Count), cancellationToken);
            foreach (global::Parquet.Thrift.ColumnChunk _iter212 in Columns)
            {
              await _iter212.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteListEndAsync(cancellationToken);
          }
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        tmp211.Name = "total_byte_size";
        tmp211.Type = TType.I64;
        tmp211.ID = 2;
        await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
        await oprot.WriteI64Async(Total_byte_size, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        tmp211.Name = "num_rows";
        tmp211.Type = TType.I64;
        tmp211.ID = 3;
        await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
        await oprot.WriteI64Async(Num_rows, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        if((Sorting_columns != null) && __isset.sorting_columns)
        {
          tmp211.Name = "sorting_columns";
          tmp211.Type = TType.List;
          tmp211.ID = 4;
          await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
          {
            await oprot.WriteListBeginAsync(new TList(TType.Struct, Sorting_columns.Count), cancellationToken);
            foreach (global::Parquet.Thrift.SortingColumn _iter213 in Sorting_columns)
            {
              await _iter213.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteListEndAsync(cancellationToken);
          }
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if(__isset.file_offset)
        {
          tmp211.Name = "file_offset";
          tmp211.Type = TType.I64;
          tmp211.ID = 5;
          await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
          await oprot.WriteI64Async(File_offset, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if(__isset.total_compressed_size)
        {
          tmp211.Name = "total_compressed_size";
          tmp211.Type = TType.I64;
          tmp211.ID = 6;
          await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
          await oprot.WriteI64Async(Total_compressed_size, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if(__isset.ordinal)
        {
          tmp211.Name = "ordinal";
          tmp211.Type = TType.I16;
          tmp211.ID = 7;
          await oprot.WriteFieldBeginAsync(tmp211, cancellationToken);
          await oprot.WriteI16Async(Ordinal, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      if (!(that is RowGroup other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return TCollections.Equals(Columns, other.Columns)
        && global::System.Object.Equals(Total_byte_size, other.Total_byte_size)
        && global::System.Object.Equals(Num_rows, other.Num_rows)
        && ((__isset.sorting_columns == other.__isset.sorting_columns) && ((!__isset.sorting_columns) || (TCollections.Equals(Sorting_columns, other.Sorting_columns))))
        && ((__isset.file_offset == other.__isset.file_offset) && ((!__isset.file_offset) || (global::System.Object.Equals(File_offset, other.File_offset))))
        && ((__isset.total_compressed_size == other.__isset.total_compressed_size) && ((!__isset.total_compressed_size) || (global::System.Object.Equals(Total_compressed_size, other.Total_compressed_size))))
        && ((__isset.ordinal == other.__isset.ordinal) && ((!__isset.ordinal) || (global::System.Object.Equals(Ordinal, other.Ordinal))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Columns != null))
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Columns);
        }
        hashcode = (hashcode * 397) + Total_byte_size.GetHashCode();
        hashcode = (hashcode * 397) + Num_rows.GetHashCode();
        if((Sorting_columns != null) && __isset.sorting_columns)
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Sorting_columns);
        }
        if(__isset.file_offset)
        {
          hashcode = (hashcode * 397) + File_offset.GetHashCode();
        }
        if(__isset.total_compressed_size)
        {
          hashcode = (hashcode * 397) + Total_compressed_size.GetHashCode();
        }
        if(__isset.ordinal)
        {
          hashcode = (hashcode * 397) + Ordinal.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp214 = new StringBuilder("RowGroup(");
      if((Columns != null))
      {
        tmp214.Append(", Columns: ");
        Columns.ToString(tmp214);
      }
      tmp214.Append(", Total_byte_size: ");
      Total_byte_size.ToString(tmp214);
      tmp214.Append(", Num_rows: ");
      Num_rows.ToString(tmp214);
      if((Sorting_columns != null) && __isset.sorting_columns)
      {
        tmp214.Append(", Sorting_columns: ");
        Sorting_columns.ToString(tmp214);
      }
      if(__isset.file_offset)
      {
        tmp214.Append(", File_offset: ");
        File_offset.ToString(tmp214);
      }
      if(__isset.total_compressed_size)
      {
        tmp214.Append(", Total_compressed_size: ");
        Total_compressed_size.ToString(tmp214);
      }
      if(__isset.ordinal)
      {
        tmp214.Append(", Ordinal: ");
        Ordinal.ToString(tmp214);
      }
      tmp214.Append(')');
      return tmp214.ToString();
    }
  }

}
