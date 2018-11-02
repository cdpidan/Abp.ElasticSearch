using System.Threading.Tasks;
using Abp.Dependency;
using Abp.ElasticSearch;
using Abp.ElasticSearch.Configuration;

namespace Abp.Auditing
{
    public class ElasticSearchAuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly IElasticSearchClientProvider _elasticSearchClientProvider;
        private readonly IElasticSearchConfiguration _elasticSearchConfiguration;

        public ElasticSearchAuditingStore(IElasticSearchClientProvider elasticSearchClientProvider,
            IElasticSearchConfiguration elasticSearchConfiguration)
        {
            _elasticSearchClientProvider = elasticSearchClientProvider;
            _elasticSearchConfiguration = elasticSearchConfiguration;
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            await _elasticSearchClientProvider.ElasticClient.IndexAsync(auditInfo,
                x => x.Index(_elasticSearchConfiguration.AuditingLogIndexName));
        }
    }
}