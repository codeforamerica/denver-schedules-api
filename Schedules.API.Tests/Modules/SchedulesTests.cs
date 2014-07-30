using NUnit.Framework;
using Nancy.Testing;
using System;

namespace Schedules.API.Tests.Modules
{
  [TestFixture ()]
  public class SchedulesTest
  {
    Browser browser;

    [TestFixtureSetUp]
    public void SetUp()
    {
        browser = new Browser(new CustomBootstrapper());
    }

    [Test]
    public void OptionsShouldAllowAllOrigins()
    {
        var response = browser.Options("/schedules", with => with.HttpRequest());
        Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test]
    public void OptionsShouldAllowGet()
    {
        var response = browser.Options("/schedules", with => with.HttpRequest());
        Assert.That(response.Headers["Access-Control-Allow-Methods"], Contains.Substring("GET"));
    }

    [Test]
    public void OptionsShouldAllowContentType()
    {
        var response = browser.Options("/schedules", with => with.HttpRequest());
        Assert.That(response.Headers["Access-Control-Allow-Headers"], Contains.Substring("Content-Type"));
    }
  }
}
