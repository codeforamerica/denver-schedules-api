using System;
using NUnit.Framework;
using Schedules.API.Models;
using Simpler;
using Schedules.API.Tasks;

namespace Schedules.API.Tests
{
  [TestFixture]
  public class CreateRemindersTests
  {
    CreateReminder createSMSReminder;

    [TestFixtureSetUp]
    public void SetUp()
    {
      createSMSReminder = Task.New<CreateReminder>();
      createSMSReminder.In.ReminderTypeName = "sms";
      createSMSReminder.In.Reminder = new Reminder {
        Contact = "5555555555",
        Message = "hello peoples",
        RemindOn = DateTime.Now,
        Verified = false,
        Address = "1234 address ave",
        CreatedAt = DateTime.Now
      };

      createSMSReminder.Execute();
    }

    [Test]
    public void SMSReminderShouldNotBeNull()
    {
      Assert.That(createSMSReminder.Out.Reminder, Is.Not.Null);
    }

    [Test]
    public void SMSReminderShouldHaveAnId()
    {
      Assert.That(createSMSReminder.Out.Reminder.Id, Is.Not.EqualTo(0));
    }
  }
}

