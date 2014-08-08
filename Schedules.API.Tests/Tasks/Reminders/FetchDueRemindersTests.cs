using System;
using NUnit.Framework;
using Simpler;
using Schedules.API.Tasks.Reminders;
using System.Linq;
using Schedules.API.Tasks;
using Dapper;

namespace Schedules.API.Tests
{
  [TestFixture]
  public class FetchDueRemindersTests
  {
    FetchDueReminders fetchDueReminders;

    readonly DateTime julyOne = DateTime.Parse("2014-07-01");

    [TestFixtureSetUp]
    public void SetUp ()
    {
      fetchDueReminders = Task.New<FetchDueReminders>();
    }

    [Test]
    public void ShouldReturnRemindersWithGivenRemindOnDate()
    {
      InsertReminder(julyOne.AddDays(-1));
      InsertReminder(julyOne);
      InsertReminder(julyOne.AddDays(1));

      fetchDueReminders.In.RemindOn = julyOne;
      fetchDueReminders.Execute();

      Assert.That(fetchDueReminders.Out.DueReminders.Length, Is.GreaterThan(0));
      Assert.That(fetchDueReminders.Out.DueReminders.All(r => r.RemindOn == julyOne));
    }

    void InsertReminder(DateTime remindOn)
    {
      var sql = @"
        insert into reminders(reminder_type_id, contact, message, remind_on, verified, address)
        values (1, 'contact', 'message', @RemindOn, false, 'address');
        ";

      using (var db = Db.Connect()) {
        db.Execute(sql, new { RemindOn = remindOn });
      }
    }
  }
}

