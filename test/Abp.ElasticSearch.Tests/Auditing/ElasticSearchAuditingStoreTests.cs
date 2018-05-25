using Abp.ElasticSearch.Configuration;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Abp.ElasticSearch.Tests.Auditing
{
    public class ElasticSearchAuditingStoreTests : AbpElasticSearchTestBase
    {
        private readonly IElasticSeachClientProvider _elasticSeachClientProvider;

        public ElasticSearchAuditingStoreTests()
        {
            _elasticSeachClientProvider = Resolve<IElasticSeachClientProvider>();
        }

        [Fact]
        public void Test()
        {
            var client = _elasticSeachClientProvider.ElasticClient;

//            var  createIndexResponse =client.CreateIndex("111");
//            
//            createIndexResponse.IsValid.ShouldBeTrue();

            var indexResponse = client.IndexDocument(new TestThing("peter rabbit"));
            indexResponse.IsValid.ShouldBeTrue();

            var result = client.Search<TestThing>();
            result.ShouldNotBeNull();

//            result..Success.Should().BeTrue();
        }

        public class TestThing
        {
            public string Stuff { get; }

            public TestThing(string stuff)
            {
                Stuff = stuff;
            }
        }
    }
}