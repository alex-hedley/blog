# Setup

`dotnet new console --name blog`

`cd blog`

`dotnet add package Statiq.Web --version 1.0.0-beta.54`

`dotnet add package Devlead.Statiq --version 0.18.0`

Update [Program.cs](../src/Program.cs)

```csharp
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Web;

return await Bootstrapper
  .Factory
  .CreateWeb(args)
  .AddTabGroupShortCode()
  .AddIncludeCodeShortCode()
  .RunAsync();
```

`mkdir input`

`cd input`

`mkdir posts`

`cd posts`

`touch index.md`

```md
Title: My First Statiq page
---
# Hello World!

Hello from my first Statiq page.

Testing
```

`touch paged-archive.cshtml`

Copy from [Paged Archive](https://www.statiq.dev/guide/examples/archives/paged-archive)

`dotnet run`

`dotnet run -- preview`

- http://localhost:5080

## Theme

`cd src`

`git submodule add https://github.com/statiqdev/CleanBlog.git theme`
