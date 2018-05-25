using System.Threading.Tasks;
using Abp.Dependency;
using Abp.ElasticSearch;

namespace Abp.Auditing
{
    public class ElasticSearchAuditStore : IAuditingStore, ITransientDependency
    {
        private readonly IElasticSeachClientProvider _elasticSeachClientProvider;

        public ElasticSearchAuditStore(IElasticSeachClientProvider elasticSeachClientProvider)
        {
            _elasticSeachClientProvider = elasticSeachClientProvider;
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            await _elasticSeachClientProvider.ElasticClient.IndexAsync(auditInfo, x => x.Index("abp-audit-log"));
        }
    }
}