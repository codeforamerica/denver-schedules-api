using NUnit.Framework;
using System;
using Nancy.Testing;
using Schedules.API.Models;

namespace Schedules.API.Tests
{
    [TestFixture ()]
    public class RemindersTest
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
                    with.Module<RemindersModule>();
                    with.EnableAutoRegistration();
                });
        }

        [Test ()]
        public void OptionsShouldAllowAllOrigins()
        {
            var response = browser.Options("/reminders", with => with.HttpRequest());
            Assert.That(response.Headers[allowOrigin], Is.EqualTo("*"));
        }

        [Test ()]
        public void OptionsShouldAllowPost()
        {
            var response = browser.Options("/reminders", with => with.HttpRequest());
            Assert.That(response.Headers[allowMethods], Is.EqualTo("*").Or.Contains("POST"));
        }

        [Test ()]
        public void OptionsShouldAllowRequestedHeaders()
        {
            var response = browser.Options("/reminders", with => {
                with.HttpRequest();
                with.Header(requestHeaders, "MyCustomHeader");
            });
            Assert.That(response.Headers[allowHeaders], Contains.Substring("MyCustomHeader"));
        }

        [Test ()]
        public void PostShouldAllowAllOrigins()
        {
            var response = browser.Post("/reminders", with => with.HttpRequest());
            Assert.That(response.Headers[allowOrigin], Is.EqualTo("*"));
        }

        [Test ()]
        public void PostShouldReturnOK()
        {
            var response = browser.Post("/reminders", with => with.HttpRequest());
            Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
        }

        [Test ()]
        public void PostShouldReturnJson()
        {
            var response = browser.Post("/reminders", with => with.HttpRequest());
            Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
        }

        [Test ()]
        public void PostShouldCreateAReminder()
        {
            var reminder = new Reminder {
                Cell = "2514320909",
                CellVerified = true,
                Email = "test@gmail.com",
                EmailVerified = true,
                Message = "hello peoples",
                RemindOn = DateTime.Now
            };

            var response = browser.Post("/reminders", with => {
                with.HttpRequest();
                with.FormValue("cell", reminder.Cell);
                with.FormValue("cellVerified", reminder.CellVerified.ToString());
                with.FormValue("email", reminder.Email);
                with.FormValue("emailVerified", reminder.EmailVerified.ToString());
                with.FormValue("message", reminder.Message);
                with.FormValue("remindOn", reminder.RemindOn.ToString());
            });

            Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
            Assert.That (response.Context.JsonBody<Reminder> (), Is.EqualTo (reminder));
        }
    }
}
