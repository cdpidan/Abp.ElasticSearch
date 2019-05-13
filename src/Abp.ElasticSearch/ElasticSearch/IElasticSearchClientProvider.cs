using Nest;

namespace Abp.ElasticSearch
{
    public interface IElasticSearchClientProvider
    {
        IElasticClient ElasticClient { get; }
    }
}