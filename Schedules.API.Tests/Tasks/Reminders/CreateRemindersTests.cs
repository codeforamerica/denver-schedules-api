using System;
using NUnit.Framework;
using Schedules.API.Models;
using Simpler;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tests.Tasks.Reminders
{
  [TestFixture]
  public class CreateRemindersTests:CreateRemindersTestsBase
  {
    [TestFixtureTearDown]
    public void TearDown()
    {// TODO: Find a mock framework
      TearDownEmail();
      TearDownSMS();
    }

    [Test]
    public void SMSReminderShouldNotBeNull()
    {
      SetUpSms();
      Assert.That(CreateReminder.Out.Reminder, Is.Not.Null);
    }

    [Test]
    public void SMSReminderShouldHaveAnId()
    {
      SetUpSms();
      Assert.That(CreateReminder.Out.Reminder.Id, Is.Not.EqualTo(0));
    }

    [Test]
    public void SMSReminderShouldBeOfTypeSMS()
    {
      SetUpSms();
      Assert.That(CreateReminder.Out.Reminder.ReminderType.Name, Is.EqualTo("sms"));
    }

    [Test]
    public void SMSReminderShouldHaveARemindOnDate()
    {
      SetUpSms();
      Assert.That(CreateReminder.Out.Reminder.RemindOn, Is.EqualTo(JulyOne));
    }

        [Test]
    public void EmailReminderShouldNotBeNull()
    {
      SetUpEmail();
      Assert.That(CreateReminder.Out.Reminder, Is.Not.Null);
    }

    [Test]
    public void EmailReminderShouldHaveAnId()
    {
      SetUpEmail();
      Assert.That(CreateReminder.Out.Reminder.Id, Is.Not.EqualTo(0));
    }

    [Test]
    public void EmailReminderShouldBeOfTypeEmail()
    {
      SetUpEmail();
      Assert.That(CreateReminder.Out.Reminder.ReminderType.Name, Is.EqualTo("email"));
    }

    [Test]
    public void EmailReminderShouldHaveARemindOnDate()
    {
      SetUpEmail();
      Assert.That(CreateReminder.Out.Reminder.RemindOn, Is.EqualTo(JulyOne));
    }
  }
}

