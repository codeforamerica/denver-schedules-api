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
    CreateReminder createEmailReminder;

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

      createEmailReminder = Task.New<CreateReminder>();
      createEmailReminder.In.ReminderTypeName = "email";
      createEmailReminder.In.Reminder = new Reminder {
        Contact = "example@email.com",
        Message = "hello peoples",
        RemindOn = DateTime.Now,
        Verified = false,
        Address = "1234 address ave",
        CreatedAt = DateTime.Now
      };

      createEmailReminder.Execute();
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

    [Test]
    public void EmailReminderShouldNotBeNull()
    {
      Assert.That(createEmailReminder.Out.Reminder, Is.Not.Null);
    }

    [Test]
    public void EmailReminderShouldHaveAnId()
    {
      Assert.That(createEmailReminder.Out.Reminder.Id, Is.Not.EqualTo(0));
    }
  }
}

