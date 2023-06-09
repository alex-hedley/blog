---
title: Mermaid
tags:
    - mermaid
    - js
    - statiq
author: alexhedley
description: How to add mermaid diagrams in statiq
published: 2023-05-16
---

Add a file `_scripts.cshtml`

<?# IncludeCode "./../_scripts.cshtml" /?>

```mermaid
flowchart LR
    subgraph azureprod [Azure fa:fa-cloud]
        AppProduction(App Service fa:fa-globe) --> api-production[APIM fa:fa-sitemap<br>api-production]
        AppProduction --> vnetprd[vnet-prd]
    end
    subgraph onpremprod [On-premises fa:fa-home]
        vnetprd ----> SQLProd[SQL Prod fa:fa-server ] --> DBProd[(DBProd)]
    end
```

## [Devlead]

- [scripts](https://github.com/devlead/Devlead.Statiq/blob/main/src/Devlead.Statiq.TestWeb/input/_scripts.cshtml)
- [Tabs.md](https://github.com/devlead/Devlead.Statiq/blob/main/src/Devlead.Statiq.TestWeb/input/posts/Tabs.md)

## Other

- [Using Mermaid diagrams with Statiq.](https://www.dpvreony.com/articles/mermaid-with-statiq/)
  - [repo](https://github.com/dpvreony/article-statiq-mermaid)
- [Mermaid Diagrams in Statiq](https://blog.beckshome.com/2022/09/mermaid-in-statiq)
  - [code](https://github.com/thbst16/dotnet-statiq-beckshome-blog/blob/main/input/posts/mermaid-in-statiq.md)

- [StatiqMermaid Repo](https://github.com/ociaw/StatiqMermaid)
  - [nuget](https://www.nuget.org/packages/Ociaw.StatiqMermaid/0.1.0-beta.2)
