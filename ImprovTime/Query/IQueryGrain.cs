using System.Threading.Tasks;
using Orleans;

namespace ImprovTime.Query
{
    public interface IQueryGrain : IGrainWithIntegerKey
    {
        Task<QueryResult> Query(Query q);
    }
}