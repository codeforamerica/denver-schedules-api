using NUnit.Framework;
using Nancy.Testing;
using Schedules.API.Models;
using System;
using Nancy;

namespace Schedules.API.Tests.Modules.Sending
{
  [TestFixture]
  public class RemindersEmailSendTests
  {
    Browser browser;
    const string url = "/reminders/email/send";

    [TestFixtureSetUp]
    public void SetUp ()
    {
      browser = new Browser (new CustomBootstrapper());
    }

    [Test()]
    public void OptionsShouldAllowAllOrigins ()
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test()]
    public void OptionsShouldAllowPost ()
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Methods"], Contains.Substring("POST"));
    }

    [Test()]
    public void OptionsShouldAllowContentType ()
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Headers"], Contains.Substring("Content-Type"));
    }

    [Test]
    public void ShouldReturnUnauthorizedIfHeaderIsMissingToken()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test, Category("Email")]
    public void PostShouldAllowAllOrigins ()
    {
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test, Category("Email")]
    public void PostShouldReturnCreated ()
    {
      var token = Authenticate();
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.Header("Authorization", "Token " + token);
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Category("Email")]
    public void PostShouldReturnCreatedDataAsJson ()
    {
      var token = Authenticate();
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.Header("Authorization", "Token " + token);
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
      Assert.That(response.Context.JsonBody<Send>(), Is.InstanceOf<Send>());
    }

    string Authenticate()
    {
      var username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
      var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

      var response = browser.Post("/authenticate", with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.JsonBody<User>(new User { Username = username, Password = password });
      });
      return response.Context.JsonBody<Authenticate>().Token;
    }
  }
}
