using NUnit.Framework;
using Nancy.Testing;
using System;

namespace Schedules.API.Tests.Modules
{
  [TestFixture]
  public class HolidaysTest
  {
    const string url = "/schedules/holidays";
    Browser browser;
    BrowserResponse response;

    [TestFixtureSetUp]
    public void SetUp()
    {
      browser = new Browser(new CustomBootstrapper());
    }

    public void SetUpOptions() {
      response = browser.Options(url, with => with.HttpRequest());
    }

    public void SetUpGet() {
      response = browser.Get(url, with => with.HttpRequest());
    }

    [Test]
    public void OptionsShouldAllowAllOrigins()
    {
      SetUpOptions();
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test]
    public void OptionsShouldAllowGet()
    {
      SetUpOptions();
      Assert.That(response.Headers["Access-Control-Allow-Methods"], Contains.Substring("GET"));
    }

    [Test]
    public void OptionsShouldAllowContentType()
    {
      SetUpOptions();
      Assert.That(response.Headers["Access-Control-Allow-Headers"], Contains.Substring("Content-Type"));
    }

    [Test]
    public void ListIndexShouldAllowAllOrigins()
    {
      SetUpGet();
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test]
    public void ListIndexShouldReturnOk()
    {
      SetUpGet();
      Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public void ListIndexShouldReturnJson()
    {
      SetUpGet();
      Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
    }
  }
}
