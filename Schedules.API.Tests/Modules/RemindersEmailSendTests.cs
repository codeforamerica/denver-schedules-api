using NUnit.Framework;
using Nancy.Testing;
using Schedules.API.Models;

namespace Schedules.API.Tests.Modules
{
  [TestFixture()]
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

    [Test()]
    public void PostShouldAllowAllOrigins ()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test()]
    public void PostShouldReturnCreated ()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
    }

    [Test()]
    public void PostShouldReturnCreatedDataAsJson ()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
      Assert.That(response.Context.JsonBody<Send>(), Is.InstanceOf<Send>());
    }
  }
}
