using System;
using NUnit.Framework;
using Simpler;
using Schedules.API.Models;
using Schedules.API.Tasks.Sending;

namespace Schedules.API.Tests.Tasks.Sending
{
  [TestFixture]
  public class SendSMSTests
  {// https://www.twilio.com/docs/api/rest/test-credentials

    [Test, Category("SMS")]
    public void ShouldSendSMSForDueReminders()
    {
      var reminders = new [] {
        new Reminder {
          Contact="15005550006",
          Message="Please do a thing"
        }
      };

      var sendSMS = Task.New<SendSMS>();
      sendSMS.In.DueReminders = reminders;
      sendSMS.Execute();

      Assert.That(sendSMS.Out.Sent, Is.EqualTo(reminders.Length));
      Assert.That(sendSMS.Out.Errors, Is.EqualTo(0));
    }

    [Test, Category("SMS")]
    public void ShouldLogErrorWhenInternationalNumber()
    {
      var internationalNumber = "15005550003";
      ShouldLogErrorWhenFails(internationalNumber);
    }

    [Test, Category("SMS")]
    public void ShouldLogErrorWhenBlacklistedNumber()
    {
      var blackListedNumber = "15005550004";
      ShouldLogErrorWhenFails(blackListedNumber);
    }

    [Test, Category("SMS")]
    public void ShouldLogErrorWhenUnableToRoute()
    {
      var invalidNumber = "15005550002";
      ShouldLogErrorWhenFails(invalidNumber);
    }

    private void ShouldLogErrorWhenFails(string numberThatFails)
    {
      var reminders = new [] {
        new Reminder {
          Contact= numberThatFails,
          Message="Please do a thing"
        }
      };

      var sendSMS = Task.New<SendSMS>();
      sendSMS.In.DueReminders = reminders;
      var logOutput = ConsoleHelper.CaptureOutput(sendSMS.Execute);

      Assert.That(sendSMS.Out.Sent, Is.EqualTo(0));
      Assert.That(sendSMS.Out.Errors, Is.EqualTo(reminders.Length));
      Assert.That(logOutput, Contains.Substring(String.Format("SMS to {0} failed", numberThatFails)));
    }
  }
}

