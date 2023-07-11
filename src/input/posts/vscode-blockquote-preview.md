---
title: VSCode - Blockquote - Preview
tags:
    - vscode
    - markdown
author: alex-hedley
description: Change the background colour in preview.
published: 2023-07-11
# image: /posts/images/##.png
# imageattribution: https://code.visualstudio.com
---

<!-- VSCode - Blockquote -->

I use the [learn-preview](https://marketplace.visualstudio.com/items?itemName=docsmsft.docs-preview) VSCode extension to show markdown in a side by side fashion.

Not sure when this stopped working since the colours changed but now it's unreadable:

![BlockQuote](images/vscode/blockquote_1.png "BlockQuote")

Highlighting the text helps but this shouldn't be necessary.

![BlockQuote](images/vscode/blockquote_2.png "BlockQuote")

Update the `settings.json` with the following.

(You can pick whichever colour you prefer.)

```json
"workbench.colorCustomizations": {
    "textBlockQuote.background":"#95e0d6"
}
```

> This is a BlockQuote

As an image:

![BlockQuote](images/vscode/blockquote_3.png "BlockQuote")

## Info

Extension:

```bash
Name: learn-preview
Id: docsmsft.docs-preview
Description: Learn Markdown Preview Extension
Version: 1.0.2
Publisher: Microsoft
VS Marketplace Link: https://marketplace.visualstudio.com/items?itemName=docsmsft.docs-preview
```
