using NUnit.Framework;
using Nancy.Testing;
using Schedules.API.Models;
using System;

namespace Schedules.API.Tests.Modules
{
  [TestFixture]
  public class AuthenticateTests
  {
    Browser browser;
    const string url = "/authenticate";

    [TestFixtureSetUp]
    public void SetUp()
    {
      browser = new Browser(new CustomBootstrapper());
    }

    [Test]
    public void PostWithoutUserDataShouldReturnUnauthorized()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.That(response.StatusCode, Is.EqualTo(Nancy.HttpStatusCode.Unauthorized));
    }

    [Test]
    public void PostWithUnauthorizedUserDataShouldReturnUnauthorized()
    {
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.JsonBody<User>(new User { Username = "unknown", Password = "person" });
      });
      Assert.That(response.StatusCode, Is.EqualTo(Nancy.HttpStatusCode.Unauthorized));
    }

    [Test]
    public void PostAdminUserShouldReturnToken()
    {
      var username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
      var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.JsonBody<User>(new User { Username = username, Password = password });
      });

      Assert.That(response.StatusCode, Is.EqualTo(Nancy.HttpStatusCode.OK));
      Assert.That(response.Context.JsonBody<Authenticate>().Token, Is.Not.Empty);
    }
  }
}
