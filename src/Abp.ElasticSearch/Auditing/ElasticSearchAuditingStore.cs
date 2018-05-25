using System.Threading.Tasks;
using Abp.Dependency;
using Abp.ElasticSearch;

namespace Abp.Auditing
{
    public class ElasticSearchAuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly IElasticSeachClientProvider _elasticSeachClientProvider;

        public ElasticSearchAuditingStore(IElasticSeachClientProvider elasticSeachClientProvider)
        {
            _elasticSeachClientProvider = elasticSeachClientProvider;
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            await _elasticSeachClientProvider.ElasticClient.IndexAsync(auditInfo, x => x.Index("abp-audit-log"));
        }
    }
}