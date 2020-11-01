using System;
using System.Collections.Generic;

namespace ImprovTime.Query
{
    public class Query
    {
        public List<QueryableAttributes> Attributes { get; set; } = new List<QueryableAttributes>();
        
        public DateTimeOffset Start { get; set; }
        
        public DateTimeOffset End { get; set; }
        
        public Aggregate Aggregate { get; set; }
        
        public string Metric { get; set; }
        
        public string Service { get; set; }
    }

    public class QueryableAttributes
    {
        public string Name { get; set; }
        
        public string Value { get; set; }
    }
}