using Abp.Configuration.Startup;
using Abp.ElasticSearch.Configuration;
using Abp.Modules;

namespace Abp.ElasticSearch.Tests.ElasticSearch
{
    [DependsOn(typeof(AbpElasticSearchModule))]
    public class AbpElasticSearchTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.ElasticSearch().ConnectionString = "http://localhost:9200";
            Configuration.Modules.ElasticSearch().AuthUserName = "elastic";
            Configuration.Modules.ElasticSearch().AuthPassWord = "elasticpw";
            Configuration.Modules.ElasticSearch().UseAuditingLog = false;

            Configuration.ReplaceService<IElasticSeachClientProvider, ElasticSearchClientTestProvider>();
        }
    }
}