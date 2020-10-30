using System.Threading.Tasks;
using Orleans;

namespace ImprovTime.Query
{
    public interface IQueryMapGrain : IGrainWithIntegerKey
    {
        Task<RecordQueryResult> QueryIndividual(RecordQuery query);
    }
}