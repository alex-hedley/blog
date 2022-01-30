---
title: Deploy Tech Docs
tags:
    - programming
    - github
author: alex-hedley
description: 
published: 2023-01-30
---

In this article I'll show how to deploy the GOV.UK [Tech Docs Template](https://github.com/alphagov/tech-docs-template) to [GitHub Pages](https://pages.github.com/).

> The Tech Docs Template is a [Middleman template](https://middlemanapp.com/advanced/project_templates/) that you can use to build technical documentation using a GOV.UK style.

To learn more about the template you can view the [Documentation Site](https://tdt-documentation.london.cloudapps.digital/) ([Code](https://github.com/alphagov/tdt-documentation))

There's a section in the docs for [GitHub Pages](https://tdt-documentation.london.cloudapps.digital/publish_project/deploy/#github-pages) which mentions how MoJ achieve this, see [publish.yml](https://github.com/ministryofjustice/cloud-platform-user-guide/blob/main/.github/workflows/publish.yml) from their **Cloud Platform User Guide**, there is also another example [publish.yml](https://github.com/ministryofjustice/hmpps-tech-docs/blob/main/.github/workflows/publish.yml) from **hmpps-tech-docs** which uses some scripts ([bin](https://github.com/ministryofjustice/hmpps-tech-docs/tree/main/bin)).

As an alternative I created my own [GitHub Action Workflow](https://docs.github.com/en/actions/using-workflows).

## Config

Firstly you need to add some config to your existing project. As GitHub Pages deploys to the repo name links won't work, i.e. `username.github.io/projectname`.

Update the `config.rb` file with the below settings.

```ruby
# https://github.com/edgecase/middleman-gh-pages#project-page-path-issues

# Project Page Path Issues
# Since project pages deploy to a subdirectory, assets and page paths are relative to the organization or user that owns the repo. If you're treating the project pages as a standalone site, you can tell Middleman to generate relative paths for assets and links with these settings in the build configuration in config.rb
# NOTE This only affects sites being accessed at the username.github.io/projectname URL, not when accessed at a custom CNAME.

activate :relative_assets
set :relative_links, true
```

Thanks to the [Middleman Github Pages](https://github.com/edgecase/middleman-gh-pages#project-page-path-issues) project for this.

You can also add a `CNAME` file with your url `https://[USERNAME].github.io/[REPO_NAME]`. This will be copied by the workflow later.

## Workflow

The WF only runs when there has been a change in the `source/` folder. You may wish to update this to your own needs. I've also added a `workflow_dispatch` trigger so you can run this manually.

Next add some **write** permissions for `contents` and `pages`.

Now for the main `job` which is built up of a number of `steps`.

First you'll want to **[checkout](https://github.com/actions/checkout)** (`actions/checkout@v2`) your code.

Next as this is a [Ruby](https://www.ruby-lang.org/en/) project **[Setup](https://github.com/ruby/setup-ruby)** (`ruby/setup-ruby@v1`) and choose the correct version.

Next install your bundle. This can just be a simple `run` script. You can also check that `middleman` is available.

Next build your website using `bundle exec middleman build`.

Once this is built you can optionally copy over a `CNAME`.

Next deploy your project to GitHub Pages using another action [peaceiris/actions-gh-pages@v3.9.2](https://github.com/peaceiris/actions-gh-pages).
If you are outputting your build to a different folder make sure to update this `publish_dir: build`.

Finally in your Repo settings configure GHP.

- [Quickstart for GitHub Pages](https://docs.github.com/en/pages/quickstart)

Yaml file for reference:

```yml
name: Deploy

on:
  workflow_dispatch:
  push:
    branches: [main]
    paths:
      - "source/**"

# Sets permissions of the GITHUB_TOKEN to allow deployment to GitHub Pages
permissions:
  contents: write
  pages: write
  id-token: write

jobs:
  build_and_deploy:
    name: Build & Deploy
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v2
        
      - name: Set up 💎 Ruby 3.2.0
        uses: ruby/setup-ruby@v1
        with:
          ruby-version: 3.2.0
#           bundler-cache: true
        
      - name: Install Dependencies
        run: |
          echo 'Installing bundles...'
          gem install bundler
          bundle install
          bundle list | grep "middleman ("
        
      - name: Build
        run: bundle exec middleman build
      
      - name: Copy CNAME
        run: |
          echo "Copying cname across too"
          cp -a CNAME build
      
      - name: Deploy
        uses: peaceiris/actions-gh-pages@v3.9.2
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: build
```

## Alternatives

- [Middleman Github Pages](https://github.com/edgecase/middleman-gh-pages)
- [middleman-gh-pages-action](https://github.com/yurikoval/middleman-gh-pages-action)
  - [Marketplace](https://github.com/marketplace/actions/middleman-github-pages-action)

I found an [issue](https://github.com/yurikoval/middleman-gh-pages-action/issues/3) with the GHA which was referencing `master` not `main`.
