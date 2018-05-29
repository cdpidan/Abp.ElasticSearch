[TOC]

# Abp.ElasticSearch
以模块的方式对ElasticSearch进行集成

## Nuget

[![NuGet version](https://badge.fury.io/nu/Abp.ElasticSearch.Core.svg)](https://badge.fury.io/nu/Abp.ElasticSearch.Core)

## Get Started

### Install package
`dotnet add package Abp.ElasticSearch.Core`

### AppSettings
```
{
    "ElasticSearchConfiguration": {
        "ConnectionString": "http://localhost:9200",
        "AuthUserName": "elastic",
        "AuthPassWord": "elastic",
        "UseAuditingLog": true,
        "AuditingLogIndexName": "abp-audit-log"
    }
}
```
AuditingLogIndexName默认值：abp-audit-log

### Configuration
**Startup.cs -> ConfigureServices**
```
services.AddElasticSearch(options =>
{
    _appConfiguration.GetSection("ElasticSearchConfiguration").Bind(options);
});
```

### Module DependsOn
添加依赖：AbpElasticSearchModule
```
[DependsOn(typeof(AbpElasticSearchModule))]
public class XXXCoreModel
{
}
```

## License

[MIT](LICENSE).