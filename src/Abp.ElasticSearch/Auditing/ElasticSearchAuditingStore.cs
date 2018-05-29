using System.Threading.Tasks;
using Abp.Dependency;
using Abp.ElasticSearch;
using Abp.ElasticSearch.Configuration;

namespace Abp.Auditing
{
    public class ElasticSearchAuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly IElasticSeachClientProvider _elasticSeachClientProvider;
        private readonly IElasticSearchConfiguration _elasticSearchConfiguration;

        public ElasticSearchAuditingStore(IElasticSeachClientProvider elasticSeachClientProvider,
            IElasticSearchConfiguration elasticSearchConfiguration)
        {
            _elasticSeachClientProvider = elasticSeachClientProvider;
            _elasticSearchConfiguration = elasticSearchConfiguration;
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            await _elasticSeachClientProvider.ElasticClient.IndexAsync(auditInfo,
                x => x.Index(_elasticSearchConfiguration.AuditingLogIndexName));
        }
    }
}