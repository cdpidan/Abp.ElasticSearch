using Abp.ElasticSearch.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Abp.ElasticSearch
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpElasticSearchModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IElasticSearchConfigration, ElasticSearchConfiguration>();
        }
        
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpElasticSearchModule).GetAssembly());
        }
    }
}