using NUnit.Framework;
using Simpler;
using Schedules.API.Models;
using System;
using System.IO;
using Schedules.API.Tasks.Sending;

namespace Schedules.API.Tests.Tasks.Sending
{
  [TestFixture]
  public class SendEmailsTests
  {
//    [Test]
//    public void ShouldSendEmailsForDueReminders ()
//    {
//      var reminders = new[] {
//        new Reminder {
//          Email = "denver@example.com",
//          Message = "Consider yourself reminded."
//        }
//      };
//
//      var sendEmails = Task.New<SendEmails>();
//      sendEmails.In.DueReminders = reminders;
//      sendEmails.Execute();
//
//      Assert.That(sendEmails.Out.EmailsSent, Is.EqualTo(reminders.Length));
//      Assert.That(sendEmails.Out.Errors, Is.EqualTo(0));
//    }
//
//    [Test]
//    public void ShouldLogErrorsForRejectedEmails ()
//    {
//      var mandrillRejectEmail = "reject@test.mandrillapp.com";
//      var reminders = new[] {
//        new Reminder {
//          Email = mandrillRejectEmail,
//          Message = "Consider yourself reminded."
//        }
//      };
//
//      var sendEmails = Task.New<SendEmails>();
//      sendEmails.In.DueReminders = reminders;
//      var logOutput = CaptureOutput(sendEmails.Execute);
//
//      Assert.That(sendEmails.Out.EmailsSent, Is.EqualTo(0));
//      Assert.That(sendEmails.Out.Errors, Is.EqualTo(reminders.Length));
//      Assert.That(logOutput, Contains.Substring(String.Format("Email {0} failed", mandrillRejectEmail)));
//    }

    static string CaptureOutput (Action execute)
    {
      using (var sw = new StringWriter ()) {
        var previousOut = Console.Out;
        Console.SetOut(sw);
        execute();
        Console.SetOut(previousOut);
        return sw.ToString();
      }
    }
  }
}
