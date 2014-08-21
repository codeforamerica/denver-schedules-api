using NUnit.Framework;
using Nancy.Testing;
using Nancy;
using Schedules.API.Models;
using System;
using System.Collections.Generic;

namespace Schedules.API.Tests.Modules.Sending
{
  [TestFixture]
  public class RemindersSendTests
  {
    Browser browser;

    public static IEnumerable<TestCaseData> SendTypes 
    {
      get {
        yield return new TestCaseData("/reminders/email/send");
        yield return new TestCaseData("/reminders/sms/send");
      }
    }

    [TestFixtureSetUp]
    public void SetUp ()
    {
      browser = new Browser (new CustomBootstrapper());
    }

    [Test, TestCaseSource("SendTypes")]
    public void OptionsShouldAllowAllOrigins(string url)
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test, TestCaseSource("SendTypes")]
    public void OptionsShouldAllowPost(string url)
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Methods"], Contains.Substring("POST"));
    }

    [Test, TestCaseSource("SendTypes")]
    public void OptionsShouldAllowContentType(string url)
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Headers"], Contains.Substring("Content-Type"));
    }

    [Test, TestCaseSource("SendTypes")]
    public void ShouldReturnUnauthorizedIfHeaderIsMissingToken(string url)
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test, TestCaseSource("SendTypes"), Category("Reminder")]
    public void PostShouldAllowAllOrigins(string url)
    {
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test, TestCaseSource("SendTypes"), Category("Reminder")]
    public void PostShouldReturnCreated(string url)
    {
      var token = AuthenticationHelper.Authenticate(browser);
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.Header("Authorization", "Token " + token);
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, TestCaseSource("SendTypes"), Category("Reminder")]
    public void PostShouldReturnCreatedDataAsJson(string url)
    {
      var token = AuthenticationHelper.Authenticate(browser);
      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.Header("Authorization", "Token " + token);
        with.JsonBody<Send>(new Send { RemindOn = DateTime.Now });
      });
      Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
      Assert.That(response.Context.JsonBody<Send>(), Is.InstanceOf<Send>());
    }
  }
}

