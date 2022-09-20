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

  public partial class OffsetIndex : TBase
  {

    /// <summary>
    /// PageLocations, ordered by increasing PageLocation.offset. It is required
    /// that page_locations[i].first_row_index &lt; page_locations[i+1].first_row_index.
    /// </summary>
    public List<global::Parquet.Thrift.PageLocation> Page_locations { get; set; }

    public OffsetIndex()
    {
    }

    public OffsetIndex(List<global::Parquet.Thrift.PageLocation> page_locations) : this()
    {
      this.Page_locations = page_locations;
    }

    public OffsetIndex DeepCopy()
    {
      var tmp230 = new OffsetIndex();
      if((Page_locations != null))
      {
        tmp230.Page_locations = this.Page_locations.DeepCopy();
      }
      return tmp230;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_page_locations = false;
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
                  TList _list231 = await iprot.ReadListBeginAsync(cancellationToken);
                  Page_locations = new List<global::Parquet.Thrift.PageLocation>(_list231.Count);
                  for(int _i232 = 0; _i232 < _list231.Count; ++_i232)
                  {
                    global::Parquet.Thrift.PageLocation _elem233;
                    _elem233 = new global::Parquet.Thrift.PageLocation();
                    await _elem233.ReadAsync(iprot, cancellationToken);
                    Page_locations.Add(_elem233);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
                isset_page_locations = true;
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
        if (!isset_page_locations)
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
        var tmp234 = new TStruct("OffsetIndex");
        await oprot.WriteStructBeginAsync(tmp234, cancellationToken);
        var tmp235 = new TField();
        if((Page_locations != null))
        {
          tmp235.Name = "page_locations";
          tmp235.Type = TType.List;
          tmp235.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp235, cancellationToken);
          {
            await oprot.WriteListBeginAsync(new TList(TType.Struct, Page_locations.Count), cancellationToken);
            foreach (global::Parquet.Thrift.PageLocation _iter236 in Page_locations)
            {
              await _iter236.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteListEndAsync(cancellationToken);
          }
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
      if (!(that is OffsetIndex other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return TCollections.Equals(Page_locations, other.Page_locations);
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Page_locations != null))
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Page_locations);
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp237 = new StringBuilder("OffsetIndex(");
      if((Page_locations != null))
      {
        tmp237.Append(", Page_locations: ");
        Page_locations.ToString(tmp237);
      }
      tmp237.Append(')');
      return tmp237.ToString();
    }
  }

}
