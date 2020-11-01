using System;
using System.Collections.Generic;

namespace ImprovTime.Query
{
    public class QueryResult
    {
        public Query Query { get; set; }

        public List<ResultRecord> Results { get; set; } =
            new List<ResultRecord>();
    }

    public class ResultRecord
    {
        public DateTimeOffset Start { get; set; }
        
        public double Value { get; set; }
    }
}