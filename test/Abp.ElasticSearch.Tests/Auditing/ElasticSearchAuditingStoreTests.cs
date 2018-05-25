using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.ElasticSearch.Tests.ElasticSearch;
using Nest;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Abp.ElasticSearch.Tests.Auditing
{
    public class ElasticSearchAuditingStoreTests : AbpElasticSearchTestBase
    {
        private readonly ElasticSearchClientTestProvider _elasticSeachClientProvider;

        public ElasticSearchAuditingStoreTests()
        {
            _elasticSeachClientProvider = Resolve<ElasticSearchClientTestProvider>();
        }

        [Fact]
        public async Task SaveAsync_Test()
        {
            _elasticSeachClientProvider.ElasticClient = Substitute.For<IElasticClient>();

            var store = new ElasticSearchAuditingStore(_elasticSeachClientProvider);

            var auditInfo = new AuditInfo
            {
                ClientName = "AbpTest",
                ClientIpAddress = "127.0.0.1",
                MethodName = "Test",
                ServiceName = "Hello"
            };

            await store.SaveAsync(auditInfo);

            await _elasticSeachClientProvider.ElasticClient.Received()
                .IndexAsync(
                    Arg.Is<AuditInfo>(x =>
                        x.ClientName == auditInfo.ClientName
                        && x.ClientIpAddress == auditInfo.ClientIpAddress
                        && x.MethodName == auditInfo.MethodName
                        && x.ServiceName == auditInfo.ServiceName
                        && x.ServiceName == auditInfo.ServiceName
                    ),
                    Arg.Any<Func<IndexDescriptor<AuditInfo>, IIndexRequest<AuditInfo>>>());
        }

        [Fact]
        public void Test2()
        {
            var response = CreateSearchResonse(index => new AuditInfo {ClientName = $"Client {index}"}, 25);
            var client = GetElasticClient(response);

            var searchResponse = client.Search<AuditInfo>(s => s.MatchAll());

            searchResponse.IsValid.ShouldBeTrue();
            searchResponse.Documents.Count.ShouldBe(25);
        }
    }
}