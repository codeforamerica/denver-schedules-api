using NUnit.Framework;
using Nancy.Testing;

namespace Schedules.API.Tests.Modules
{
  [TestFixture]
  public class StreetSweepingTests
  {
    const string longitude = "-104.952312";
    const string latitude = "39.7211511226435";
    const string url = "/schedules/streetsweeping";
    Browser browser;
    BrowserResponse response;

    [TestFixtureSetUp]
    public void SetUp()
    {
      browser = new Browser(new CustomBootstrapper());
    }

    public void SetUpOptions() {
      response = browser.Options(url, (with) => {
        with.HttpRequest();
        with.Query("longitude", longitude);
        with.Query("latitude", latitude);
        with.Query("accuracy", "0.631");
      });
    }

    public void SetUpGet() {
      response = browser.Get(url, (with) => {
        with.HttpRequest();
        with.Query("longitude", longitude);
        with.Query("latitude", latitude);
        with.Query("accuracy", "0.631");
      });
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

    [Test]
    public void ShouldThrowAnErrorWithoutLatAndLong()
    {
      SetUpGet();
      browser = new Browser(new CustomBootstrapper());
      response = browser.Get(url, (with) => with.HttpRequest());
      Assert.AreEqual(Nancy.HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}

