using System;
using Abp.ElasticSearch.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.ElasticSearch
{
    public static class ElasticSearchRegistrar
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services,
            Action<IElasticSearchConfiguration> options)
        {
            var esConfiguration = new ElasticSearchConfiguration();
            options(esConfiguration);
            services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfiguration>(service => esConfiguration);

            return services;
        }
    }
}