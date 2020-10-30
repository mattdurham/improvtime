using ProtoBuf;

namespace ImprovTime
{
    [ProtoContract]
    public struct LogEntry
    {
        [ProtoMember(1)]
        public string KeyName { get; set; }
        
        [ProtoMember(2)]
        public string KeyValue { get; set; }
        
        [ProtoMember(3)]
        public double MetricValue { get; set; }
        
        [ProtoMember(4)]
        public uint RecordID { get; set; }
        
        [ProtoMember(5)]
        public uint Offset { get; set; }
    }
}