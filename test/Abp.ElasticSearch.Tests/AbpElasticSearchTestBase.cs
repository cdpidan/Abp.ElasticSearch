using System;
using System.Linq;
using System.Text;
using Abp.ElasticSearch.Tests.ElasticSearch;
using Abp.TestBase;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;

namespace Abp.ElasticSearch.Tests
{
    public class AbpElasticSearchTestBase : AbpIntegratedTestBase<AbpElasticSearchTestModule>
    {
        protected const string DefaultIndexName = "abp-es";

        protected IElasticClient GetElasticClient(object responseData = null)
        {
            IConnection connection = null;
            if (responseData != null)
            {
                var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData));
                connection = new InMemoryConnection(responseBytes);
            }
            else
            {
                connection = new InMemoryConnection();
            }

            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(connectionPool, connection).DefaultIndex(DefaultIndexName);
            var client = new ElasticClient(settings);

            return client;
        }

        protected object CreateSearchResonse<T>(Func<int, T> createFunc, int totalCount, string typeName = null)
            where T : class, new()
        {
            typeName = GetTypeName<T>(typeName);

            var response = new
            {
                took = 1,
                timed_out = false,
                _shards = new
                {
                    total = 1,
                    successful = 1,
                    failed = 0
                },
                hits = new
                {
                    total = 25,
                    max_score = 1.0,
                    hits = Enumerable.Range(1, totalCount).Select(i => new
                    {
                        _index = DefaultIndexName,
                        _type = typeName,
                        _id = $"Project {i}",
                        _score = 1.0,
                        _source = createFunc(i)
                    }).ToArray()
                }
            };

            return response;
        }

        protected object CreateDocumentCreateResponse<T>(string typeName = null) where T : class, new()
        {
            var response = new
            {
                _index = DefaultIndexName,
                _type = GetTypeName<T>(typeName),
                _id = 1,
                _version = 1,
                result = "created",
                _shards = new
                {
                    total = 2,
                    successful = 1,
                    failed = 0
                },
                _seq_no = 0,
                _primary_term = 1
            };

            return response;
        }

        protected string GetTypeName<T>(string typeName) where T : class, new()
        {
            typeName = typeName ?? typeof(T).Name.ToLower();
            if (typeName == "object")
                throw new ArgumentNullException(nameof(typeName));
            return typeName.ToLower();
        }
    }
}