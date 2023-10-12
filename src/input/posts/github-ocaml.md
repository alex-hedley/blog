---
title: 🐪 OCaml on GitHub
tags:
    - ocaml
    - github
author: alex-hedley
description: Using OCaml in GitHub with Actions/Workflows etc.
published: 2023-10-12
# image: /posts/images/
# imageattribution: 
---

<!-- # 🐪 OCaml on GitHub -->

In this article I'll show you how to setup and configure GitHub Actions to work with an OCaml Library.

Rob Anderson [@jamsidedown](https://github.com/jamsidedown/) ([Site](https://robanderson.dev/)) decided he's going to be doing ⭐️ [AoC](https://adventofcode.com) this year in [OCaml](https://ocaml.org)! They wrote a new library [ocaml-cll](https://github.com/jamsidedown/ocaml-cll) an OCaml circular linked list library, in preperation for this as it's been needed in other languages for previous questions.

To help out we looked at how to build the package using [GitHub Actions](https://github.com/features/actions).

See the example here: ocaml-example [main.yml](https://github.com/alex-hedley/ocaml-example/blob/main/.github/workflows/main.yml)

Or the production one: ocaml-cll [main.yml](https://github.com/jamsidedown/ocaml-cll/blob/main/.github/workflows/main.yml)

Firstly as with most builds you setup your env with the language you need:

- [setup-ocaml](https://github.com/ocaml/setup-ocaml)

Another useful action is [actions-ocaml](https://github.com/RedPRL/actions-ocaml).

```yml
- name: 🐪 Set-up OCaml
  uses: ocaml/setup-ocaml@v2
  with:
    ocaml-compiler: "5.0"
    dune-cache: true
```

Once configured you need to install any dependencies from your `.opam`/`dune-project` file.

```yml
- name: ⬇️ Install opam deps
  run: opam install . --deps-only --with-test
```

It's worth installing any global packages you need like `odoc` etc.

```yml
- name: ⬇️ Install opam global deps
  run: opam install odoc ocamlformat dune-release
```

Next **build** your project:

```yml
- name: 🔨 Build
  run: opam exec -- dune build
```

And **test**:

```yml
- name: 🧪 Test
  run: opam exec -- dune runtest
```

Or with **coverage**:

```yml
- name: 🧪 Test and Coverage
  run: "opam exec -- dune runtest --instrument-with bisect_ppx --force"
- name: 📄 Coverage Report"
  run: "opam exec -- bisect-ppx-report summary"
```

You can then build the **docs**:

```yml
- name: 📄 Build Docs
  run: opam exec -- dune build @doc
```

Then decide if you want to upload that as an artifact and deploy it to either [GitHub Pages](https://pages.github.com) or another service.

Once you have a package you can add it to the opam-repository, example PRs here:

- https://github.com/ocaml/opam-repository/pull/24626
- https://github.com/ocaml/opam-repository/pull/24719
- https://github.com/ocaml/opam-repository/pull/24777

---

Complete:

```yml
jobs:
  build:
    runs-on: ubuntu-latest

    defaults:
      run:
        working-directory: src/helloworld

    steps:
      - name: 🛎️ Checkout
        uses: actions/checkout@v4

      - name: 🐪 Set-up OCaml
        uses: ocaml/setup-ocaml@v2
        with:
          ocaml-compiler: "5.0"
          dune-cache: true

      - name: ⬇️ Install opam deps
        run: opam install . --deps-only --with-test

      - name: ⬇️ Install opam global deps
        run: opam install odoc ocamlformat dune-release

      - name: 🔨 Build
        run: opam exec -- dune build

      - name: 🧪 Test
        run: opam exec -- dune runtest

      - name: 📄 Build Docs
        run: opam exec -- dune build @doc
      
      - name: Upload API Docs artifact
        uses: actions/upload-artifact@v3.1.3
        with:
          name: docs
          path: ./src/helloworld/_build/default/_doc/_html

      - name: Deploy API Docs
        uses: peaceiris/actions-gh-pages@v3
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          user_name: 'github-actions[bot]'
          user_email: 'github-actions[bot]@users.noreply.github.com'
          publish_dir: ./src/helloworld/_build/default/_doc/_html
          destination_dir: docs
          enable_jekyll: true
```

## ocaml-cll

- Documentation https://robanderson.dev/ocaml-cll/cll/
- Package https://opam.ocaml.org/packages/cll/

## Links

- https://github.com/alex-hedley/ocaml-example
- https://github.com/jamsidedown/ocaml-cll
