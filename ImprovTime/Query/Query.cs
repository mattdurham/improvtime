using System;
using System.Collections.Generic;

namespace ImprovTime.Query
{
    public class Query
    {
        public Dictionary<string,string> Attributes = new Dictionary<string, string>();
        
        public DateTimeOffset Start { get; set; }
        
        public DateTimeOffset End { get; set; }
        
        public Aggregate Aggregate { get; set; }
        
        public string Metric { get; set; }
        
        public string Service { get; set; }
    }
}