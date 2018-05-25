using Nest;

namespace Abp.ElasticSearch
{
    public interface IElasticSeachClientProvider
    {
        IElasticClient ElasticClient { get; }
    }
}