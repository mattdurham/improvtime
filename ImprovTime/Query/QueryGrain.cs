using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace ImprovTime.Query
{
    [StatelessWorker]
    public class QueryGrain : Grain, IQueryGrain
    {
        private IGrainFactory _factory;
        public QueryGrain(IGrainFactory factory)
        {
            _factory = factory;
        }
        public async Task<QueryResult> Query(Query q)
        {
            // Need to determine the range of nodes to query
            var normalizedStart = new DateTimeOffset(q.Start.Year,q.Start.Month,q.Start.Day,q.Start.Hour,q.Start.Minute,0,new TimeSpan());
            var tasks = new List<Task<RecordQueryResult>>();
            while (normalizedStart <= q.End)
            {
                var worker  = _factory.GetGrain<IQueryMapGrain>(0);
                await worker.QueryIndividual(new RecordQuery()
                {
                    Aggregate = q.Aggregate,
                    Attributes = q.Attributes,
                    End = q.End,
                    Start = q.Start,
                    EntryMinute = normalizedStart,
                    MetricName = q.Metric,
                    ServiceName = q.Service
                });
                normalizedStart = normalizedStart.AddMinutes(1);
            }

            // Do the cast to get rid of the compiler warning
            Task.WaitAll(tasks.Cast<Task>().ToArray());
            var result = new QueryResult()
            {
                Query = q
            };
            
            foreach (var t in tasks)
            {
                result.Results.Add(t.Result.Source.EntryMinute, t.Result.Result);
            }

            return result;
        }
    }
}