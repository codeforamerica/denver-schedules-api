using NUnit.Framework;
using Schedules.API.Tasks.Sending;
using Simpler;
using System;
using Schedules.API.Models;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tests.Tasks.Sending
{
  [TestFixture]
  public class PostReminderEmailSendTests
  {
    PostRemindersEmailSend postRemindersEmailSend;

    readonly DateTime tomorrow = DateTime.Now.AddDays(1);

    [TestFixtureSetUp]
    public void SetUp ()
    {
      postRemindersEmailSend = Task.New<PostRemindersEmailSend>();
    }

    [Test]
    public void ShouldSendEmailsForDueReminders()
    {
      var count = 0;
      postRemindersEmailSend.FetchDueReminders = Fake.Task<FetchDueReminders>(
        fdr => fdr.Out.DueReminders = new[] {
          new Reminder { RemindOn = tomorrow },
          new Reminder { RemindOn = tomorrow }
        }
      );
      postRemindersEmailSend.SendEmails = Fake.Task<SendEmails>(
        se => count = se.In.DueReminders.Length
      );
      postRemindersEmailSend.In.Send = new Send { RemindOn = tomorrow };
      postRemindersEmailSend.Execute();
      Assert.That(count, Is.EqualTo(2));
    }
  }
}
