using Abp.ElasticSearch.Configuration;
using Shouldly;
using Xunit;

namespace Abp.ElasticSearch.Tests.ElasticSearch.Configuration
{
    public class ElasticSearchConfigrationTests : AbpElasticSearchTestBase
    {
        [Fact]
        public void Test_ResolveElasticSearchConfiguration()
        {
            var elasticSearchConfigration = Resolve<IElasticSearchConfigration>();

            elasticSearchConfigration.ShouldNotBeNull();
            elasticSearchConfigration.ConnectionString.ShouldBe("http://localhost:9200");
            elasticSearchConfigration.AuthUserName.ShouldBe("elastic");
            elasticSearchConfigration.AuthPassWord.ShouldBe("elasticpw");
        }

        [Fact]
        public void Test_ChangeConfiguration()
        {
            var elasticSearchConfigration = Resolve<IElasticSearchConfigration>();

            elasticSearchConfigration.AuthPassWord = "helloworld";

            var newElasticSearchConfigration = Resolve<IElasticSearchConfigration>();
            newElasticSearchConfigration.AuthPassWord.ShouldBe(elasticSearchConfigration.AuthPassWord);
        }
    }
}