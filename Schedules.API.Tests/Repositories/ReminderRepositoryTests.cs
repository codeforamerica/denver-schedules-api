using System;
using NUnit.Framework;
using Schedules.API.Models;
using Schedules.API.Helpers;
using System.Collections.Generic;

namespace Schedules.API.Tests.Repositories
{
  [TestFixture]
  public class ReminderRepositoryTests
  {// https://www.twilio.com/docs/api/rest/test-credentials

    [Test]
    public void SendsTextReminder() {
      Reminder reminder = new Reminder {
        Cell="15005550006", 
        Message="Please do a thing"
      };
      var reminders = new System.Collections.Generic.List<Reminder> () { reminder };
      var sent = Notifier.DoIt (reminders);
      Assert.That (sent.Errors, Is.Empty);
    }

    [Test]
    public void LogsErrorWhenTwilioCanNotRouteToNumber() {
      // Invalid cell
      Reminder reminder = new Reminder {
        Cell="15005550002", 
        Message="Please do a thing"
      };
     
      var reminders = new System.Collections.Generic.List<Reminder> () { reminder };
      var sent = Notifier.DoIt (reminders);
      Assert.That (sent.Errors.Count, Is.EqualTo (1));
    }

    [Test]
    public void LogsErrorWhenAccountCanNotMakeInternationalCalls() {
      Reminder reminder = new Reminder {
        Cell="15005550003", 
        Message="Please do a thing"
      };

      var reminders = new System.Collections.Generic.List<Reminder> () { reminder };
      var sent = Notifier.DoIt (reminders);
      Assert.That (sent.Errors.Count, Is.EqualTo (1));
    }

    [Test]
    public void LogsErrorWhenBlacklistedNumber() {
      Reminder reminder = new Reminder {
        Cell="15005550004", 
        Message="Please do a thing"
      };

      var reminders = new System.Collections.Generic.List<Reminder> () { reminder };
      var sent = Notifier.DoIt (reminders);
      Assert.That (sent.Errors.Count, Is.EqualTo (1));
    }
  }
}

