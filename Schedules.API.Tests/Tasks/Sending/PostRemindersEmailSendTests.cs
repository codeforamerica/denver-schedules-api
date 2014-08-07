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

    readonly DateTime julyOne = DateTime.Parse("2014-07-01");

    [TestFixtureSetUp]
    public void SetUp ()
    {
      postRemindersEmailSend = Task.New<PostRemindersEmailSend>();
    }

    [Test]
    public void ShouldSendEmailsForDueReminders()
    {
      var fakeFetchReminders = Fake.Task<FetchDueReminders>(
        fdr => fdr.Out.DueReminders = new[] {
          new Reminder { RemindOn = julyOne },
          new Reminder { RemindOn = julyOne }
        }
      );
      var fakeSendEmails = Fake.Task<SendEmails>(
        se => Assert.That(se.In.DueReminders.Length, Is.EqualTo(2))
      );
      postRemindersEmailSend.In.Send = new Send { RemindOn = julyOne };
      postRemindersEmailSend.Execute();
    }
  }
}
