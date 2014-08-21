using System;
using NUnit.Framework;
using Simpler;
using Schedules.API.Tasks.Reminders;
using System.Linq;
using Schedules.API.Tasks;
using Dapper;

namespace Schedules.API.Tests.Tasks.Reminders
{
  [TestFixture]
  public class FetchDueRemindersTests
  {
    FetchDueReminders fetchDueReminders;

    readonly DateTime julyOne = DateTime.Parse("2014-07-01");

    [TestFixtureSetUp]
    public void SetUp ()
    {
      InsertSmsReminder(julyOne.AddDays(-1));
      InsertSmsReminder(julyOne);
      InsertSmsReminder(julyOne.AddDays(1));

      InsertEmailReminder(julyOne.AddDays(-1));
      InsertEmailReminder(julyOne);
      InsertEmailReminder(julyOne.AddDays(1));

      fetchDueReminders = Task.New<FetchDueReminders>();
      fetchDueReminders.In.RemindOn = julyOne;
    }

    [Test]
    public void ShouldReturnSmsReminders()
    {
      fetchDueReminders.In.ReminderTypeName = "sms";
      fetchDueReminders.Execute();
      Assert.That(fetchDueReminders.Out.DueReminders.Length, Is.GreaterThan(0));
      Assert.That(fetchDueReminders.Out.DueReminders.All(r => r.ReminderType.Name == "sms"));
    }

    [Test]
    public void ShouldReturnEmailReminders()
    {
      fetchDueReminders.In.ReminderTypeName = "email";
      fetchDueReminders.Execute();
      Assert.That(fetchDueReminders.Out.DueReminders.Length, Is.GreaterThan(0));
      Assert.That(fetchDueReminders.Out.DueReminders.All(r => r.ReminderType.Name == "email"));
    }

    [Test]
    public void ShouldReturnSmsRemindersWithGivenRemindOnDate()
    {
      fetchDueReminders.In.ReminderTypeName = "sms";
      fetchDueReminders.Execute();
      Assert.That(fetchDueReminders.Out.DueReminders.All(r => r.RemindOn == julyOne));
    }

    [Test]
    public void ShouldReturnEmailRemindersWithGivenRemindOnDate()
    {
      fetchDueReminders.In.ReminderTypeName = "email";
      fetchDueReminders.Execute();
      Assert.That(fetchDueReminders.Out.DueReminders.All(r => r.RemindOn == julyOne));
    }

    static void InsertSmsReminder(DateTime remindOn)
    {
      InsertReminder(remindOn, 1);
    }

    static void InsertEmailReminder(DateTime remindOn)
    {
      InsertReminder(remindOn, 2);
    }

    static void InsertReminder(DateTime remindOn, int reminderTypeId)
    {
      var sql = @"
        insert into reminders(reminder_type_id, contact, message, remind_on, verified, address)
        values (@ReminderTypeId, 'contact', 'message', @RemindOn, false, 'address');
        ";

      using (var db = Db.Connect()) {
        db.Execute(sql, new { ReminderTypeId = reminderTypeId, RemindOn = remindOn });
      }
    }
  }
}

