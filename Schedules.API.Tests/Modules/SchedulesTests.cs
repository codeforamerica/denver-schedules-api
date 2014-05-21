using NUnit.Framework;
using Nancy.Testing;

namespace Schedules.API.Tests
{
    [TestFixture ()]
    public class SchedulesTest
    {
        Browser browser;

        const string requestHeaders = "Access-Control-Request-Headers";
        const string allowOrigin = "Access-Control-Allow-Origin";
        const string allowMethods = "Access-Control-Allow-Methods";
        const string allowHeaders = "Access-Control-Allow-Headers";

        [SetUp]
        public void SetUp()
        {
            browser = new Browser(with =>
                {
                    with.Module<SchedulesModule>();
                    with.EnableAutoRegistration();
                });
        }

        [Test ()]
        public void OptionsShouldAllowAllOrigins()
        {
            var response = browser.Options("/schedules", with => with.HttpRequest());
            Assert.That(response.Headers[allowOrigin], Is.EqualTo("*"));
        }

        [Test ()]
        public void OptionsShouldAllowPost()
        {
            var response = browser.Options("/schedules", with => with.HttpRequest());
            Assert.That(response.Headers[allowMethods], Is.EqualTo("*").Or.Contains("POST"));
        }

        [Test ()]
        public void OptionsShouldAllowRequestedHeaders()
        {
            var response = browser.Options("/schedules", with => {
                with.HttpRequest();
                with.Header(requestHeaders, "MyCustomHeader");
            });
            Assert.That(response.Headers[allowHeaders], Contains.Substring("MyCustomHeader"));
        }

        [Test ()]
        public void ListIndexShouldAllowAllOrigins()
        {
            var response = browser.Get("/schedules", with => with.HttpRequest());
            Assert.That(response.Headers[allowOrigin], Is.EqualTo("*"));
        }

        [Test ()]
        public void ListIndexShouldReturnOk()
        {
            var response = browser.Get("/schedules", with => with.HttpRequest());
            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
        }

        [Test ()]
        public void ListIndexShouldReturnJson()
        {
            var response = browser.Get("/schedules", with => with.HttpRequest());
            Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
        }
    }
}
