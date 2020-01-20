---
title: [Beta] 0.2.0 Release Notes
author: Daniel Fitz
date: 20th January 2020
---

A new release of Sanchez.Markdown has been published to Nuget and GitHub, it brings with it some much needed core features of Markdown a couple of extensions...

# Document Metadata
Document metadata allows you to begin a Markdown file with a pair of `---`, any content inside the symbols will automatically be parsed in a key-value pair setting and be returned as apart of the document metadata.

# Comments
The ability to comment out an entire line in Markdown has also been added, this is done by starting the line with `%` and anything else on that line will not be parsed and instead a plain comment symbol will be returned. Different renders can choose to implement the final details for if to ignore it (like the provided implementations do) or provide it to the end user in a unique styling.

# Code Block
Support for both inline and block code blocks have been added and are used just like normal Markdown code blocks are defined.

> **NOTE:** there is currently no support for defining a language to be used with the code block

# Special Functions
Special functions is a new experimental feature which will allow users to add a high degree of customization into their Markdown. A special function is created by starting a line with `!func`, where `func` is the special function which will be reverse looked up by a renderer and render special content on page. Special functions also supports the ability to add arguments afterwards. An example usage of this might be a custom author field to show on the page, `!author Daniel Fitz` which might render out to have a profile picture and hyperlinked (these features would be implemented through the arguments passed into a renderer).