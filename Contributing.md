### Code

Denver Street Sweeping is composed of an [API backend][backend] and a [client front end][client]. The API reads the data. The front end takes data from the API, formats it, and serves up the website. 

[This is the backend repo][backend]. A commit to the appropriate branch on github triggers a build, tests, and deployment.

* [Continuous Integration][travis] (travis)
* [Staging][staging] (branch: master)
* [Production][prod] (branch: production)

[backend]: https://github.com/codeforamerica/denver-schedules-api
[prod]: http://production-denver-now-api.herokuapp.com/
[staging]: http://staging-denver-now-api.herokuapp.com/schedules

### Installation, Usage
* [Development Environment][dev]
* [Database Set Up][db]
* [Environment Variables][env]

[dev]: https://github.com/codeforamerica/denver-schedules-api/wiki/Enviroment-Setup
[db]: https://github.com/codeforamerica/denver-schedules-api/wiki/Database-Setup
[env]: https://github.com/codeforamerica/denver-schedules-api/wiki/Environment-Variables

### Submitting an Issue
We use the GitHub issue tracker to track bugs and features. Before submitting a bug report or feature request, check to make sure it hasn't already been submitted. When submitting a bug report, please include a [Gist][] that includes a stack trace and any details that may be necessary to reproduce the bug.

[gist]: https://gist.github.com/

### Submitting a Pull Request
1. [Fork the repository.][fork]
2. [Create a topic branch.][branch]
3. [Submit a pull request.][pr]

[fork]: http://help.github.com/fork-a-repo/
[branch]: http://learn.github.com/p/branching.html
[pr]: http://help.github.com/send-pull-requests/