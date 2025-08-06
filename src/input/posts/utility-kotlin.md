---
title: Utility Kotlin
# lead: 
description: Building a Utility App in Kotlin Multiplatform
tags:
    - utility
    - kotlin
author: alex-hedley
published: 2025-08-06
# image: /posts/images/##.png
# imageattribution: 
---

<!-- Utility Kotlin -->

For the past 10+ years I've been building a [Utility](https://github.com/AlexHedley/Utility) app to speed up day to day tasks. It started with a [VB.NET](https://github.com/AlexHedley/Utility-VB) version for encoding/decoding HTML to help with log files in [Symantec Workflow](https://techdocs.broadcom.com/us/en/symantec-security-software/endpoint-security-and-management/it-management-suite/ITMS/Related-Solutions/Workflow-Solution/Introducing-Workflow/how-workflow-works-v81109357-d855e17980.html). It grew with other functionality include SQL generation etc. From that I used the opportunity to build a [macOS](https://github.com/AlexHedley/Utility-Mac) app to try out my Objective-C ability on a desktop app, instead of the iOS apps I'd been building. To make it more accessible I created a [Web](https://alexhedley.com/Utility-Web/) version then swapped to [WPF](https://github.com/AlexHedley/Utility-WPF) and finally a [Blazor](https://alexhedley.com/Utility-Blazor/) one a few years after that.
Having switched back to Java and now Kotlin dev, in the past couple of years, it seemed a good opportunity to try my hand at [Kotlin Multiplatform](https://kotlinlang.org/docs/multiplatform.html). Having seen the power of [WASM](https://webassembly.org/) with [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) it was a no brainer. Hopefully I can actually find the time to build multiple apps but for now I just wanted a web version with the first utility being a [Duration](https://docs.oracle.com/javase/8/docs/api/java/time/Duration.html) parser.

![Utility Kotlin](images/Utility-Kotlin.png "Utility Kotlin")

It's a bit of a mess, all being in one file [App.kt](https://github.com/alex-hedley/Utility-Kotlin/blob/main/src/composeApp/src/wasmJsMain/kotlin/com/alexhedley/App.kt), but I wanted a POC working quickly, I'll refactor it later once I've read up more on the best way to architecture these. It builds and deploys to [GitHub Pages](https://pages.github.com) using the following WF: [ci.yml](https://github.com/alex-hedley/Utility-Kotlin/blob/main/.github/workflows/ci.yml) and all runs natively in your browser. Check out the canvas element in the generated page source.

Let me know what you think, and are there any other features you'd liked adding?

I'm looking to backport other ideas from the previous apps to give me an opportunity to try different UI elements.

## 🌍 Site

- https://alex-hedley.github.io/Utility-Kotlin/

## Source

- https://github.com/alex-hedley/Utility-Kotlin
