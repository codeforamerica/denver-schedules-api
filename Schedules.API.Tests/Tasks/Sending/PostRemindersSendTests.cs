using NUnit.Framework;
using Simpler;
using Schedules.API.Tasks.Sending;
using System;
using Schedules.API.Models;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tests.Tasks.Sending
{
  [TestFixture]
  public class PostRemindersSendTests
  {
    PostRemindersSend postRemindersSend;

    readonly DateTime tomorrow = DateTime.Now.AddDays(1);

    [TestFixtureSetUp]
    public void SetUp ()
    {
      postRemindersSend = Task.New<PostRemindersSend>();
    }

    [Test]
    public void ShouldSendSMSForDueReminders()
    {
      ShouldSendNotifcationForDueReminders<SendSMS>();
    }

    [Test]
    public void ShouldSendEmailsForDueReminders()
    {
      ShouldSendNotifcationForDueReminders<SendEmails>();
    }

    private void ShouldSendNotifcationForDueReminders<T>() where T:SendReminderBase
    {
      var count = 0;
      postRemindersSend.FetchDueReminders = Fake.Task<FetchDueReminders>(
        fdr => fdr.Out.DueReminders = new[] {
          new Reminder { RemindOn = tomorrow },
          new Reminder { RemindOn = tomorrow }
        }
      );
      postRemindersSend.In.SendReminders = Fake.Task<T>(
        se => count = se.In.DueReminders.Length
      );
      postRemindersSend.In.Send = new Send { RemindOn = tomorrow };
      postRemindersSend.Execute();
      Assert.That(count, Is.EqualTo(2));
    }
  }
}

