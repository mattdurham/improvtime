using System;
using System.Collections.Generic;

namespace ImprovTime.Query
{
    public class QueryResult
    {
        public Query Query { get; set; }
        
        public Dictionary<DateTimeOffset, double> Results { get; set; } = new Dictionary<DateTimeOffset, double>();
    }
}