# FBLAComputerGameSimulation
Table of contents
=================

<!--ts-->
   * [Table of contents](#table-of-contents)
   * [Installation](#installation)
   * [Usage](#usage)
      * [STDIN](#stdin)
      * [Local files](#local-files)
      * [Remote files](#remote-files)
      * [Multiple files](#multiple-files)
      * [Combo](#combo)
      * [Auto insert and update TOC](#auto-insert-and-update-toc)
      * [GitHub token](#github-token)
      * [TOC generation with Github Actions](#toc-generation-with-github-actions)
   * [Tests](#tests)
   * [Dependency](#dependency)
   * [Docker](#docker)
     * [Local](#local)
     * [Public](#public)
<!--te-->
Installation
============

Linux (manual installation)
```bash
$ wget https://raw.githubusercontent.com/ekalinin/github-markdown-toc/master/gh-md-toc
$ chmod a+x gh-md-toc
```

MacOS (manual installation)
```bash
$ curl https://raw.githubusercontent.com/ekalinin/github-markdown-toc/master/gh-md-toc -o gh-md-toc
$ chmod a+x gh-md-toc
```

Linux or MacOS (using [Basher](https://github.com/basherpm/basher))
```bash
$ basher install ekalinin/github-markdown-toc
# `gh-md-toc` will automatically be available in the PATH
```

Usage
=====
