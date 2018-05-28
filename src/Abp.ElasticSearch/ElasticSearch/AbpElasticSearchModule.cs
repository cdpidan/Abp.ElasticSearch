using Abp.Auditing;
using Abp.ElasticSearch.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Dependency;

namespace Abp.ElasticSearch
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpElasticSearchModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IElasticSearchConfiguration, ElasticSearchConfiguration>();

            if (Configuration.Modules.ElasticSearch()?.UseAuditingLog == true)
            {
                IocManager.RegisterIfNot<IAuditingStore, ElasticSearchAuditingStore>();
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpElasticSearchModule).GetAssembly());
        }
    }
}