using NUnit.Framework;
using Nancy.Testing;
using Schedules.API.Models;
using System;

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
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Category("Email")]
    public void PostShouldReturnCreatedDataAsJson ()
    {
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
      Assert.That(response.Context.JsonBody<Send>(), Is.InstanceOf<Send>());
    }
  }
}
