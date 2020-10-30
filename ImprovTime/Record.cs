using System;
using System.Collections.Generic;

namespace ImprovTime
{
    public struct Record
    {
        public DateTimeOffset Time { get; set; }
        
        public string Service { get; set; }
        
        public Dictionary<string, string> Attributes { get; set; }
        
        public double Value { get; set; }
        
        public string RecordName { get; set; }
    }
}