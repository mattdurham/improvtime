// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Record.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Record.proto</summary>
public static partial class RecordReflection {

  #region Descriptor
  /// <summary>File descriptor for Record.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static RecordReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgxSZWNvcmQucHJvdG8isAEKBlJlY29yZBIMCgR0aW1lGAEgASgEEg8KB3Nl",
          "cnZpY2UYAiABKAkSEwoLbWV0cmljdmFsdWUYAyABKAESEgoKbWV0cmljbmFt",
          "ZRgEIAEoCRIrCgphdHRyaWJ1dGVzGAUgAygLMhcuUmVjb3JkLkF0dHJpYnV0",
          "ZXNFbnRyeRoxCg9BdHRyaWJ1dGVzRW50cnkSCwoDa2V5GAEgASgJEg0KBXZh",
          "bHVlGAIgASgJOgI4ASJoCghMb2dFbnRyeRIQCghrZXlfbmFtZRgBIAEoCRIR",
          "CglrZXlfdmFsdWUYAiABKAkSFAoMbWV0cmljX3ZhbHVlGAMgASgBEhEKCXJl",
          "Y29yZF9pZBgEIAEoDRIOCgZvZmZzZXQYBSABKA1iBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Record), global::Record.Parser, new[]{ "Time", "Service", "Metricvalue", "Metricname", "Attributes" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, }),
          new pbr::GeneratedClrTypeInfo(typeof(global::LogEntry), global::LogEntry.Parser, new[]{ "KeyName", "KeyValue", "MetricValue", "RecordId", "Offset" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class Record : pb::IMessage<Record>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Record> _parser = new pb::MessageParser<Record>(() => new Record());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Record> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RecordReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Record() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Record(Record other) : this() {
    time_ = other.time_;
    service_ = other.service_;
    metricvalue_ = other.metricvalue_;
    metricname_ = other.metricname_;
    attributes_ = other.attributes_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Record Clone() {
    return new Record(this);
  }

  /// <summary>Field number for the "time" field.</summary>
  public const int TimeFieldNumber = 1;
  private ulong time_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ulong Time {
    get { return time_; }
    set {
      time_ = value;
    }
  }

  /// <summary>Field number for the "service" field.</summary>
  public const int ServiceFieldNumber = 2;
  private string service_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Service {
    get { return service_; }
    set {
      service_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "metricvalue" field.</summary>
  public const int MetricvalueFieldNumber = 3;
  private double metricvalue_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public double Metricvalue {
    get { return metricvalue_; }
    set {
      metricvalue_ = value;
    }
  }

  /// <summary>Field number for the "metricname" field.</summary>
  public const int MetricnameFieldNumber = 4;
  private string metricname_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Metricname {
    get { return metricname_; }
    set {
      metricname_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "attributes" field.</summary>
  public const int AttributesFieldNumber = 5;
  private static readonly pbc::MapField<string, string>.Codec _map_attributes_codec
      = new pbc::MapField<string, string>.Codec(pb::FieldCodec.ForString(10, ""), pb::FieldCodec.ForString(18, ""), 42);
  private readonly pbc::MapField<string, string> attributes_ = new pbc::MapField<string, string>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::MapField<string, string> Attributes {
    get { return attributes_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as Record);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(Record other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Time != other.Time) return false;
    if (Service != other.Service) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Metricvalue, other.Metricvalue)) return false;
    if (Metricname != other.Metricname) return false;
    if (!Attributes.Equals(other.Attributes)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Time != 0UL) hash ^= Time.GetHashCode();
    if (Service.Length != 0) hash ^= Service.GetHashCode();
    if (Metricvalue != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Metricvalue);
    if (Metricname.Length != 0) hash ^= Metricname.GetHashCode();
    hash ^= Attributes.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Time != 0UL) {
      output.WriteRawTag(8);
      output.WriteUInt64(Time);
    }
    if (Service.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Service);
    }
    if (Metricvalue != 0D) {
      output.WriteRawTag(25);
      output.WriteDouble(Metricvalue);
    }
    if (Metricname.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Metricname);
    }
    attributes_.WriteTo(output, _map_attributes_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Time != 0UL) {
      output.WriteRawTag(8);
      output.WriteUInt64(Time);
    }
    if (Service.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Service);
    }
    if (Metricvalue != 0D) {
      output.WriteRawTag(25);
      output.WriteDouble(Metricvalue);
    }
    if (Metricname.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(Metricname);
    }
    attributes_.WriteTo(ref output, _map_attributes_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Time != 0UL) {
      size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Time);
    }
    if (Service.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Service);
    }
    if (Metricvalue != 0D) {
      size += 1 + 8;
    }
    if (Metricname.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Metricname);
    }
    size += attributes_.CalculateSize(_map_attributes_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(Record other) {
    if (other == null) {
      return;
    }
    if (other.Time != 0UL) {
      Time = other.Time;
    }
    if (other.Service.Length != 0) {
      Service = other.Service;
    }
    if (other.Metricvalue != 0D) {
      Metricvalue = other.Metricvalue;
    }
    if (other.Metricname.Length != 0) {
      Metricname = other.Metricname;
    }
    attributes_.Add(other.attributes_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Time = input.ReadUInt64();
          break;
        }
        case 18: {
          Service = input.ReadString();
          break;
        }
        case 25: {
          Metricvalue = input.ReadDouble();
          break;
        }
        case 34: {
          Metricname = input.ReadString();
          break;
        }
        case 42: {
          attributes_.AddEntriesFrom(input, _map_attributes_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Time = input.ReadUInt64();
          break;
        }
        case 18: {
          Service = input.ReadString();
          break;
        }
        case 25: {
          Metricvalue = input.ReadDouble();
          break;
        }
        case 34: {
          Metricname = input.ReadString();
          break;
        }
        case 42: {
          attributes_.AddEntriesFrom(ref input, _map_attributes_codec);
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class LogEntry : pb::IMessage<LogEntry>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<LogEntry> _parser = new pb::MessageParser<LogEntry>(() => new LogEntry());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<LogEntry> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RecordReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public LogEntry() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public LogEntry(LogEntry other) : this() {
    keyName_ = other.keyName_;
    keyValue_ = other.keyValue_;
    metricValue_ = other.metricValue_;
    recordId_ = other.recordId_;
    offset_ = other.offset_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public LogEntry Clone() {
    return new LogEntry(this);
  }

  /// <summary>Field number for the "key_name" field.</summary>
  public const int KeyNameFieldNumber = 1;
  private string keyName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string KeyName {
    get { return keyName_; }
    set {
      keyName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "key_value" field.</summary>
  public const int KeyValueFieldNumber = 2;
  private string keyValue_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string KeyValue {
    get { return keyValue_; }
    set {
      keyValue_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "metric_value" field.</summary>
  public const int MetricValueFieldNumber = 3;
  private double metricValue_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public double MetricValue {
    get { return metricValue_; }
    set {
      metricValue_ = value;
    }
  }

  /// <summary>Field number for the "record_id" field.</summary>
  public const int RecordIdFieldNumber = 4;
  private uint recordId_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint RecordId {
    get { return recordId_; }
    set {
      recordId_ = value;
    }
  }

  /// <summary>Field number for the "offset" field.</summary>
  public const int OffsetFieldNumber = 5;
  private uint offset_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint Offset {
    get { return offset_; }
    set {
      offset_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as LogEntry);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(LogEntry other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (KeyName != other.KeyName) return false;
    if (KeyValue != other.KeyValue) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(MetricValue, other.MetricValue)) return false;
    if (RecordId != other.RecordId) return false;
    if (Offset != other.Offset) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (KeyName.Length != 0) hash ^= KeyName.GetHashCode();
    if (KeyValue.Length != 0) hash ^= KeyValue.GetHashCode();
    if (MetricValue != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(MetricValue);
    if (RecordId != 0) hash ^= RecordId.GetHashCode();
    if (Offset != 0) hash ^= Offset.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (KeyName.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(KeyName);
    }
    if (KeyValue.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(KeyValue);
    }
    if (MetricValue != 0D) {
      output.WriteRawTag(25);
      output.WriteDouble(MetricValue);
    }
    if (RecordId != 0) {
      output.WriteRawTag(32);
      output.WriteUInt32(RecordId);
    }
    if (Offset != 0) {
      output.WriteRawTag(40);
      output.WriteUInt32(Offset);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (KeyName.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(KeyName);
    }
    if (KeyValue.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(KeyValue);
    }
    if (MetricValue != 0D) {
      output.WriteRawTag(25);
      output.WriteDouble(MetricValue);
    }
    if (RecordId != 0) {
      output.WriteRawTag(32);
      output.WriteUInt32(RecordId);
    }
    if (Offset != 0) {
      output.WriteRawTag(40);
      output.WriteUInt32(Offset);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (KeyName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(KeyName);
    }
    if (KeyValue.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(KeyValue);
    }
    if (MetricValue != 0D) {
      size += 1 + 8;
    }
    if (RecordId != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(RecordId);
    }
    if (Offset != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Offset);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(LogEntry other) {
    if (other == null) {
      return;
    }
    if (other.KeyName.Length != 0) {
      KeyName = other.KeyName;
    }
    if (other.KeyValue.Length != 0) {
      KeyValue = other.KeyValue;
    }
    if (other.MetricValue != 0D) {
      MetricValue = other.MetricValue;
    }
    if (other.RecordId != 0) {
      RecordId = other.RecordId;
    }
    if (other.Offset != 0) {
      Offset = other.Offset;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          KeyName = input.ReadString();
          break;
        }
        case 18: {
          KeyValue = input.ReadString();
          break;
        }
        case 25: {
          MetricValue = input.ReadDouble();
          break;
        }
        case 32: {
          RecordId = input.ReadUInt32();
          break;
        }
        case 40: {
          Offset = input.ReadUInt32();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          KeyName = input.ReadString();
          break;
        }
        case 18: {
          KeyValue = input.ReadString();
          break;
        }
        case 25: {
          MetricValue = input.ReadDouble();
          break;
        }
        case 32: {
          RecordId = input.ReadUInt32();
          break;
        }
        case 40: {
          Offset = input.ReadUInt32();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code