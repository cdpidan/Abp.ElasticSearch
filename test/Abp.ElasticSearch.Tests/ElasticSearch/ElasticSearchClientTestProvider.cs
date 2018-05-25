using System;
using Elasticsearch.Net;
using Nest;

namespace Abp.ElasticSearch.Tests.ElasticSearch
{
    public class ElasticSearchClientTestProvider : IElasticSeachClientProvider
    {
        public IElasticClient ElasticClient => CreateElasticClient();

        private IElasticClient CreateElasticClient()
        {
            var connection = new InMemoryConnection();
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(connectionPool, connection)
                .DefaultIndex("abp-es-test");
            return new ElasticClient(settings);
        }
    }
}