using System.Threading.Tasks;
using ImprovTime.Query;
using Orleans;

namespace ImprovTime
{
    public interface IRecordGrain : IGrainWithStringKey
    {
        Task AddRecord(Record record);

        Task<RecordQueryResult> RunQuery(RecordQuery query);
    }
}