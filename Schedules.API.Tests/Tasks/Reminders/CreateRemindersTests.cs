using System;
using NUnit.Framework;
using Schedules.API.Models;
using Simpler;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tests.Tasks.Reminders
{
  [TestFixture]
  public class CreateRemindersTests
  {
    CreateReminder createSMSReminder;
    CreateReminder createEmailReminder;

    readonly DateTime julyOne = DateTime.Parse("07-01-2014");


    [Test]
    public void SMSReminderShouldNotBeNull()
    {
      SetUpSms();
      Assert.That(createSMSReminder.Out.Reminder, Is.Not.Null);
    }

    [Test]
    public void SMSReminderShouldHaveAnId()
    {
      SetUpSms();
      Assert.That(createSMSReminder.Out.Reminder.Id, Is.Not.EqualTo(0));
    }

    [Test]
    public void SMSReminderShouldBeOfTypeSMS()
    {
      SetUpSms();
      Assert.That(createSMSReminder.Out.Reminder.ReminderType.Name, Is.EqualTo("sms"));
    }

    [Test]
    public void SMSReminderShouldHaveARemindOnDate()
    {
      SetUpSms();
      Assert.That(createSMSReminder.Out.Reminder.RemindOn, Is.EqualTo(julyOne));
    }

        [Test]
    public void EmailReminderShouldNotBeNull()
    {
      SetUpEmail();
      Assert.That(createEmailReminder.Out.Reminder, Is.Not.Null);
    }

    [Test]
    public void EmailReminderShouldHaveAnId()
    {
      SetUpEmail();
      Assert.That(createEmailReminder.Out.Reminder.Id, Is.Not.EqualTo(0));
    }

    [Test]
    public void EmailReminderShouldBeOfTypeEmail()
    {
      SetUpEmail();
      Assert.That(createEmailReminder.Out.Reminder.ReminderType.Name, Is.EqualTo("email"));
    }

    [Test]
    public void EmailReminderShouldHaveARemindOnDate()
    {
      SetUpEmail();
      Assert.That(createEmailReminder.Out.Reminder.RemindOn, Is.EqualTo(julyOne));
    }

    void SetUpSms()
    {
      createSMSReminder = Task.New<CreateReminder>();
      createSMSReminder.In.ReminderTypeName = "sms";
      createSMSReminder.In.Reminder = new Reminder {
        Contact = "5555555555",
        Message = "hello peoples",
        RemindOn = julyOne,
        Verified = false,
        Address = "1234 address ave",
        CreatedAt = DateTime.Now
      };
      createSMSReminder.Execute();
    }

    void SetUpEmail()
    {
      createEmailReminder = Task.New<CreateReminder>();
      createEmailReminder.In.ReminderTypeName = "email";
      createEmailReminder.In.Reminder = new Reminder {
        Contact = "example@email.com",
        Message = "hello peoples",
        RemindOn = julyOne,
        Verified = false,
        Address = "1234 address ave",
        CreatedAt = DateTime.Now
      };
      createEmailReminder.Execute();
    }
  }
}

