using System;
using System.Collections.Generic;

namespace ImprovTime.Query
{
    public class RecordQuery
    {
        public Dictionary<string,string> Attributes = new Dictionary<string, string>();
        
        public DateTimeOffset Start { get; set; }
        
        public DateTimeOffset End { get; set; }
        
        public DateTimeOffset EntryMinute { get; set; }
        
        public string ServiceName { get; set; }
        
        public string MetricName { get; set; }
        
        public Aggregate Aggregate { get; set; }
    }

    public enum Aggregate
    {
        Sum,
        Count,
    }
}