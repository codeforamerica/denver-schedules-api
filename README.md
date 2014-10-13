Denver Street Sweeping API
=====================

[![Build Status][build_png]][travis]

*Current Status*: As of October 2014 Denver Street Sweeping is available on [http://streetsweep.co/](streetsweep.co) as a pilot project for the City and County of Denver.

* [What is Denver Street Sweeping](#what-is-denver-street-sweeping)
* [Technical details and developer documentation](#technical-details)
* [License](#license)

##What Is Denver Street Sweeping?

Denver Street Sweeping is a web application developed by the 2014 Code for America Fellows in Denver. The app uses Street Sweeping data from the City and County of Denver. It allows residents of Denver to lookup an address and find the corresponding street sweeping schedule for that address. The resident can then sign up for schedule reminders by Email or SMS. The web app is responsive.

[Read more about Denver Street Sweeping here](/docs/about.md)


##Technical Details

The Denver Street Sweeping API reads and parses the street sweeping data from the City and County of Denver.

**Languages**: C#, Ruby (build tools, not necessary but helpful)

### Platform pieces
* [Heroku](http://heroku.com) is used for hosting the app.
* [Travis](https://travis-ci.org/) CI is used for continuous integration service. 

### Libraries
* [Nancy](http://nancyfx.org/) is the web framework for the API.
* [Dapper](https://github.com/StackExchange/dapper-dot-net) is a mini-object operational mapper for the API.
* [Simpler](https://github.com/gregoryjscott/Simpler) is a command pattern library for the API.

### Service Dependencies
* [Twilio](https://www.twilio.com/try-twilio) is used to send SMS reminders.
* [Mandrill](https://mandrill.com/signup/) is used to send Email reminders.

### Code

[https://github.com/codeforamerica/denver-schedules-api](https://github.com/codeforamerica/denver-schedules-api)

A commit to the appropriate branch on github triggers a build, tests, and deployment.

* [Continuous Integration][travis] (travis)
* [Staging][staging] (branch: master)
* [Production][prod] (branch: production)

[prod]: http://production-denver-now-api.herokuapp.com/
[build_png]: https://travis-ci.org/codeforamerica/denver-schedules-api.png?branch=master
[travis]: https://travis-ci.org/codeforamerica/denver-schedules-api
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

##License
See the [LICENSE][] for details.

[license]: https://github.com/codeforamerica/denver-schedules-api/blob/master/LICENSE
