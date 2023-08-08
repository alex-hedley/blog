---
title: DependencyCheck .NET
tags:
    - dependencycheck
    - owasp
    - dotnet
    - github
author: alex-hedley
description: How to use DependencyCheck with .NET.
published: 2023-08-08
image: /posts/images/dependency-check/dependencycheck.svg
imageattribution: https://jeremylong.github.io/DependencyCheck/index.html
---

<!-- DependencyCheck .NET -->

In this article I will run through setting up the awesome [DependencyCheck](https://jeremylong.github.io/DependencyCheck/index.html) from [Jeremy Long](https://github.com/jeremylong) for a .NET application.

> Dependency-check can currently be used to scan software to identify the use of known vulnerable components.

I've used this on some Kotlin/Java projects and wanted to see how easy it was to setup for .NET.

There are a number of plugins listed for Gradle etc but for .NET I'm going to use the [CLI](https://jeremylong.github.io/DependencyCheck/dependency-check-cli/index.html).

You can run this locally with `brew install dependency-check` then for ***nix** systems run `dependency-check.sh --project "My App Name" --scan "/java/application/lib"`

Since I'm wanting to also try this out in CI/CD it's straight to GitHub to create a repo.

- [dotnet-dependencycheck](https://github.com/alex-hedley/dotnet-dependencycheck)

https://github.com/jeremylong/DependencyCheck

I created a simple .NET Console Application then looked at what was needed to run the tooling locally.

`dependency-check --project "DependencyCheckExample" --scan "src/DependencyCheckExample/bin/Debug/net7.0/*.dll"`

> ![Note] Update the net7.0 version to whichever you are working with.

Depending where you are running the tool from determines the output location of the report. See the [Command Line Agruments](https://jeremylong.github.io/DependencyCheck/dependency-check-cli/arguments.html) for more details.

| Short | Argument Name | Parameter | Description | Requirement |
|-|-|-|-|-|
| -o | --out | <path> | The folder to write reports to. This defaults to the current directory. If the format is not set to ALL one could specify a specific file name. | Optional |

Make sure you are in the working directory of your project before running, unless you want it adding to your _usr_ folder.

I also updated it to output to a subfolder of _reports_.

`dependency-check --project "DependencyCheckExample" --scan "DependencyCheckExample/bin/Debug/net7.0/*.dll" --out "reports"`

This produces a `dependency-check-report.html` file, self contained with the results.

![Sample Report](images/dependency-check/dependency-check-report.png "Sample Report")

Not very interesting as there are no dependencies, but at least the flow is working.

Next is how to replicate this in [GitHub Actions](https://github.com/features/actions)?

Could I run brew install and run `dependency-check.sh`, maybe but I'm sure someone has already created a GitHub Action for this.

- [Dependency-Check Action](https://github.com/dependency-check/Dependency-Check_Action)

```yml
      - name: Depcheck
        uses: dependency-check/Dependency-Check_Action@main
        id: Depcheck
        with:
          project: 'test'
          path: '.'
          format: 'HTML'
          out: 'reports' # this is the default, no need to specify unless you wish to override it
          args: >
            --failOnCVSS 7
            --enableRetired
```

This has a lot of options to override so check the docs if you want to tweak anything.

There's an ADO one too ([azuredevops](https://github.com/dependency-check/azuredevops)).

I created a Workflow ([depcheck.yml](https://github.com/alex-hedley/dotnet-dependencycheck/blob/main/.github/workflows/depcheck.yml)) to get the _src_, _restore_ any dependencies, _build_, (_test_) and then run the Dependency-Check action.

We now have an artifact so might as well upload that and since it's a website we can use GitHub Pages to host the output too.

I manually created an `index.html` on the `gh-pages` branch to point to the file, you could do with this how you wish.

I've used [JamesIves/github-pages-deploy-action](https://github.com/JamesIves/github-pages-deploy-action) but you could swap to [peaceiris/actions-gh-pages](https://github.com/peaceiris/actions-gh-pages) or even the GH one through the new Beta options which includes `actions/configure-pages`, `actions/upload-pages-artifact` and `actions/deploy-pages`.

Next up is adding some vulnerable code to the sample Console App to see what DC finds...
