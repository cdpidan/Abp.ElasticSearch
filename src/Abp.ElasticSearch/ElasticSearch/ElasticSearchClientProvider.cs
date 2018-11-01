using System;
using System.Linq;
using Abp.Dependency;
using Abp.ElasticSearch.Configuration;
using Elasticsearch.Net;
using Nest;

namespace Abp.ElasticSearch
{
    public class ElasticSearchClientProvider : IElasticSearchClientProvider, ISingletonDependency
    {
        private readonly IElasticSearchConfiguration _elasticSearchConfigration;
        private readonly Lazy<IElasticClient> _client;

        public ElasticSearchClientProvider(IElasticSearchConfiguration elasticSearchConfigration)
        {
            _elasticSearchConfigration = elasticSearchConfigration;
            _client = new Lazy<IElasticClient>(CreatElasticClient);
        }
        
        private IElasticClient CreatElasticClient()
        {
            var str = _elasticSearchConfigration.ConnectionString;
            var strs = str.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var nodes = strs.Select(s => new Uri(s)).ToList();
            var connectionPool = new StaticConnectionPool(nodes);

            var settings = new ConnectionSettings(connectionPool)
                .BasicAuthentication(_elasticSearchConfigration.AuthUserName, _elasticSearchConfigration.AuthPassWord);

            return new ElasticClient(settings);
        }

        public IElasticClient ElasticClient => _client.Value;
    }
}