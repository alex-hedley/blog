---
title: GitHub Actions Branches
tags:
    - programming
    - github
author: alex-hedley
description: 
published: 2021-12-24
---

How to get a list of branches from a current repo.

The [actions/github-script](https://github.com/actions/github-script) is a good place to start as it has access to the Repo info.
To see what you can get run the script and output the complete context:

```yml
      - name: View context attributes
        uses: actions/github-script@v5
        with:
          script: console.log(context)
```

Be careful though, as when this is ran manually it gets different values to when ran on a schedule.

Looking at the *Repository* info the `branches_url` might be useful:

```json
Context {
  payload: {
    repository: {
      branches_url: 'https://api.github.com/repos/alex-hedley/[REPONAME]/branches{/branch}',
    }
  }
}
```

- `context.payload.repository.branches_url`

We don't need the `{{/branch}}` though. So lets remove that.

```js
const branches_url = context.payload.repository.branches_url
const branches_url_new = branches_url.replace("{/branch}", "")
```

With this *url* we can now get the data using the [github.request()](https://github.com/actions/github-script#download-data-from-a-url) method.
Map just the names of the branches to an Array.
And log the data.

```js
const result = await github.request(branches_url_new)
const names = result.data.map(branch => branch.name)
console.log(names)
```

```json
{
    ...
    data: [
        { name: 'dev', commit: [Object], protected: false },
        { name: 'main', commit: [Object], protected: false }
    ]
}
```

```json
[ 'dev', 'main' ]
```

Depending on the type of event you could

```yml
on:
  push:
    branches:    
      - '*'         # matches every branch that doesn't contain a '/'
      - '*/*'       # matches every branch containing a single '/'
      - '**'        # matches every branch
      - '!master'   # excludes master
```

But this isn't available for `workflow_dispatch:` as of yet.

```yml
name: Get Branches

on:
  workflow_dispatch:
    
jobs:
  print-context:
    runs-on: ubuntu-latest
    outputs:
      branches: ${{ steps.branch-output.outputs.branches }}
    steps:
      - name: Branches
        id: branches-list
        uses: actions/github-script@v5
        with:
          script: |
            const branches_url = context.payload.repository.branches_url
            const branches_url_new = branches_url.replace("{/branch}", "")
            const result = await github.request(branches_url_new)
            const names = result.data.map(branch => branch.name)
            
            const index = names.indexOf('main');
            if (index > -1) {
              names.splice(index, 1);
            }
            console.log(names)

            return names
      
      - name: Branches
        id: branch-output
        run: |
          BRANCHES='${{ steps.branches-list.outputs.result }}'
          echo ::set-output name=branches::$BRANCHES
      
  my_matrix:
    runs-on: ubuntu-latest
    needs:
      - print-context
    strategy:
      matrix:
        branches: ${{ fromJSON(needs.print-context.outputs.branches) }}
    steps:
      - name: Branches
        run: |
          echo ${{ matrix.branches }}
```

## Links

- [Dynamic Matrices in GitHub Actions](https://thekevinwang.com/2021/09/19/github-actions-dynamic-matrix/) by [Kevin Wang](https://thekevinwang.com/me/) - September 19, 2021
