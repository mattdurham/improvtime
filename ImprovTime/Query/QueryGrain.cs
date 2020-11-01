using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprovTime.Query
{
    /// <summary>
    /// Handles the organization of queries.
    /// </summary>
    public class QueryGrain 
    {
        
        public Task<QueryResult> Query(Query q)
        {
            // Need to determine the range of nodes to query
            var normalizedStart = new DateTimeOffset(q.Start.Year,q.Start.Month,q.Start.Day,q.Start.Hour,q.Start.Minute,0,new TimeSpan());
            
            var starts = new List<DateTimeOffset>();
            // Slice the range into minute slices, this does mean our granularity is really 1 minute which is not ideal
            while (normalizedStart <= q.End)
            {
                starts.Add(normalizedStart);
                normalizedStart = normalizedStart.AddMinutes(1);
            }
            var result = new QueryResult()
            {
                Query = q
            };

            // Since they come in any order and we cache by the attributes we want to reorder them to always be the same
            var sortedAttributes = (from x in q.Attributes orderby x.Name select x).ToList(); 
            // Originally had async but found slamming the async threadpool hard caused some problems, ie if querying over a month
            //   43k asyncs or so
            Parallel.ForEach(starts, x =>
            {
                var worker = new QueryMapGrain();
                var r = worker.QueryIndividual(new RecordQuery()
                {
                    Aggregate = q.Aggregate,
                    Attributes = sortedAttributes,
                    End = q.End,
                    Start = q.Start,
                    EntryMinute = x,
                    MetricName = q.Metric,
                    ServiceName = q.Service
                }).Result;
                if (r.Result == 0)
                {
                    return;
                }
                result.Results.Add(new ResultRecord()
                {
                    Start = r.Source.EntryMinute, 
                    Value = r.Result
                        
                });
            });
            return Task.FromResult(result);
        }
    }
}
