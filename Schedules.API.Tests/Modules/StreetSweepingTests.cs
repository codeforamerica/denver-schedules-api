using NUnit.Framework;
using Nancy.Testing;

namespace Schedules.API.Tests.Modules
{
  [TestFixture]
  public class StreetSweepingTests
  {
    string url = "/schedules/streetsweeping";
    Browser browser;
    BrowserResponse response;

    [TestFixtureSetUp]
    public void SetUp()
    {
      browser = new Browser(new CustomBootstrapper());
      response = browser.Get(url, (with) => {
        with.HttpRequest();
        with.Query("longitude", "-104.95474457244");
        with.Query("latitude", "39.7659901751922");
        with.Query("accuracy", "0.631");
      });
    }

    [Test]
    public void ListIndexShouldAllowAllOrigins()
    {
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test]
    public void ListIndexShouldReturnOk()
    {
      Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public void ListIndexShouldReturnJson()
    {
      Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
    }

    [Test]
    public void ShouldThrowAnErrorWithoutLatAndLong()
    {
      browser = new Browser(new CustomBootstrapper());
      response = browser.Get(url, (with) => with.HttpRequest());
      Assert.AreEqual(Nancy.HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}

