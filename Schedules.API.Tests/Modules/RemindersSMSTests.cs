using NUnit.Framework;
using System;
using Nancy.Testing;
using Schedules.API.Models;
using Schedules.API.Tasks;
using Simpler;

namespace Schedules.API.Tests.Modules
{
  [TestFixture]
  public class RemindersSMSTest
  {
    const string url = "/reminders/sms";
    Browser browser;

    [TestFixtureSetUp]
    public void SetUp ()
    {
      browser = new Browser (new CustomBootstrapper ());
    }

    [Test]
    public void OptionsShouldAllowAllOrigins ()
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test]
    public void OptionsShouldAllowPost ()
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Methods"], Contains.Substring("POST"));
    }

    [Test]
    public void OptionsShouldAllowContentType ()
    {
      var response = browser.Options(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Headers"], Contains.Substring("Content-Type"));
    }

    [Test]
    public void PostShouldAllowAllOrigins ()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test()]
    public void PostShouldReturnOK ()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
    }

    [Test]
    public void PostShouldReturnJson ()
    {
      var response = browser.Post(url, with => with.HttpRequest());
      Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
    }

    [Test]
    public void PostShouldCreateSMSReminder ()
    {
      FetchReminderType fetchReminderType = Task.New<FetchReminderType>();
      fetchReminderType.In.ReminderTypeName = "sms";
      fetchReminderType.Execute();

      var reminder = new Reminder {
        ReminderType = fetchReminderType.Out.ReminderType,
        Contact = "5555555555",
        Message = "hello peoples",
        RemindOn = DateTime.Now,
        Verified = false,
        Address = "1234 address ave",
        CreatedAt = DateTime.Now
      };

      var response = browser.Post(url, with => {
        with.HttpRequest();
        with.FormValue("contact", reminder.Contact);
        with.FormValue("message", reminder.Message);
        with.FormValue("remindOn", reminder.RemindOn.ToString());
        with.FormValue("verified", reminder.Verified.ToString());
        with.FormValue("address", reminder.Address);
        with.FormValue("createdAt", reminder.CreatedAt.ToString());
      });

      Assert.AreEqual(Nancy.HttpStatusCode.Created, response.StatusCode);
      Assert.That(response.Context.JsonBody<Reminder>(), Is.EqualTo(reminder));
    }
  }
}
