using System;
using Elasticsearch.Net;
using Nest;
using NSubstitute;

namespace Abp.ElasticSearch.Tests.ElasticSearch
{
    public class ElasticSearchClientTestProvider : IElasticSeachClientProvider
    {
        private IElasticClient _client;

        public IElasticClient ElasticClient
        {
            get
            {
                if (_client == null)
                    _client = CreateElasticClient();

                return _client;
            }
            set { _client = value; }
        }

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