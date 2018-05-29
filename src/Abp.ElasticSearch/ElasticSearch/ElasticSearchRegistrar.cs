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

            if (esConfiguration.UseAuditingLog && string.IsNullOrWhiteSpace(esConfiguration.AuditingLogIndexName))
                throw new ArgumentNullException(nameof(esConfiguration.AuditingLogIndexName));

            services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfiguration>(service => esConfiguration);

            return services;
        }
    }
}