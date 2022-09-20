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

  /// <summary>
  /// Timestamp logical type annotation
  /// 
  /// Allowed for physical types: INT64
  /// </summary>
  public partial class TimestampType : TBase
  {

    public bool IsAdjustedToUTC { get; set; }

    public global::Parquet.Thrift.TimeUnit Unit { get; set; }

    public TimestampType()
    {
    }

    public TimestampType(bool isAdjustedToUTC, global::Parquet.Thrift.TimeUnit unit) : this()
    {
      this.IsAdjustedToUTC = isAdjustedToUTC;
      this.Unit = unit;
    }

    public TimestampType DeepCopy()
    {
      var tmp55 = new TimestampType();
      tmp55.IsAdjustedToUTC = this.IsAdjustedToUTC;
      if((Unit != null))
      {
        tmp55.Unit = (global::Parquet.Thrift.TimeUnit)this.Unit.DeepCopy();
      }
      return tmp55;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_isAdjustedToUTC = false;
        bool isset_unit = false;
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
              if (field.Type == TType.Bool)
              {
                IsAdjustedToUTC = await iprot.ReadBoolAsync(cancellationToken);
                isset_isAdjustedToUTC = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.Struct)
              {
                Unit = new global::Parquet.Thrift.TimeUnit();
                await Unit.ReadAsync(iprot, cancellationToken);
                isset_unit = true;
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
        if (!isset_isAdjustedToUTC)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_unit)
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
        var tmp56 = new TStruct("TimestampType");
        await oprot.WriteStructBeginAsync(tmp56, cancellationToken);
        var tmp57 = new TField();
        tmp57.Name = "isAdjustedToUTC";
        tmp57.Type = TType.Bool;
        tmp57.ID = 1;
        await oprot.WriteFieldBeginAsync(tmp57, cancellationToken);
        await oprot.WriteBoolAsync(IsAdjustedToUTC, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        if((Unit != null))
        {
          tmp57.Name = "unit";
          tmp57.Type = TType.Struct;
          tmp57.ID = 2;
          await oprot.WriteFieldBeginAsync(tmp57, cancellationToken);
          await Unit.WriteAsync(oprot, cancellationToken);
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
      if (!(that is TimestampType other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return global::System.Object.Equals(IsAdjustedToUTC, other.IsAdjustedToUTC)
        && global::System.Object.Equals(Unit, other.Unit);
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        hashcode = (hashcode * 397) + IsAdjustedToUTC.GetHashCode();
        if((Unit != null))
        {
          hashcode = (hashcode * 397) + Unit.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp58 = new StringBuilder("TimestampType(");
      tmp58.Append(", IsAdjustedToUTC: ");
      IsAdjustedToUTC.ToString(tmp58);
      if((Unit != null))
      {
        tmp58.Append(", Unit: ");
        Unit.ToString(tmp58);
      }
      tmp58.Append(')');
      return tmp58.ToString();
    }
  }

}
