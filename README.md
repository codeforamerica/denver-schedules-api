denver-schedules-api
=====================

[![Build Status][build_png]][travis]
 
A light-weight api for city events &amp; schedules.

* [Staging][staging]

[build_png]: https://travis-ci.org/codeforamerica/denver-schedules-api.png?branch=master
[travis]: https://travis-ci.org/codeforamerica/denver-schedules-api
[staging]: http://staging-denver-now-api.herokuapp.com/schedules

## 3rd Party Dependencies
* Register for a [twilio account][twilio].

[twilio]: https://www.twilio.com/try-twilio

## Development Environment

This project is cross-platform. To avoid noisy commits from changes to line-endings, set autocrlf to true. 
```
git config core.autocrlf true
```

When adding files, you may see the following: **warning: LF will be replaced by CRLF in .travis.yml. The file will have its original line endings in your working directory.** It's throwing out your line endings in favor of the standard CRLF ones. It can be ignored.

### Windows
* Use [Visual Studio Express] [express]

### OSX
* Use [Xamarin Studio] [xamarin]

[express]: http://www.microsoft.com/en-us/download/details.aspx?id=34673
[xamarin]: http://xamarin.com/download

## Installation, Usage
* [Database Set Up][db]
* [Environment Variables][env]

[db]: https://github.com/codeforamerica/denver-schedules-api/wiki/Database-Setup
[env]: https://github.com/codeforamerica/denver-schedules-api/wiki/Environment-Variables

## Submitting an Issue
We use the GitHub issue tracker to track bugs and features. Before submitting a bug report or feature request, check to make sure it hasn't already been submitted. When submitting a bug report, please include a [Gist][] that includes a stack trace and any details that may be necessary to reproduce the bug.

[gist]: https://gist.github.com/

## Submitting a Pull Request
1. [Fork the repository.][fork]
2. [Create a topic branch.][branch]
3. [Submit a pull request.][pr]

[fork]: http://help.github.com/fork-a-repo/
[branch]: http://learn.github.com/p/branching.html
[pr]: http://help.github.com/send-pull-requests/

# License
See the [LICENSE][] for details.

[license]: https://github.com/codeforamerica/denver-schedules-api/blob/master/LICENSE
