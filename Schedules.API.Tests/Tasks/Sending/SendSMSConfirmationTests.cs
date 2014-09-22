using System;
using NUnit.Framework;
using Simpler;
using Schedules.API.Tasks.Sending;

namespace Schedules.API.Tests.Tasks.Sending
{
  [TestFixture, Category("Reminder")]
  public class SendSMSConfirmationTests
  {
    private SendSMSConfirmation sendConfirmation;

    [Test]
    public void ShouldSendConfirmation()
    {
      sendConfirmation = Task.New<SendSMSConfirmation>();
      sendConfirmation.In.Contact = "15005550006";
      sendConfirmation.Execute();

      Assert.That(sendConfirmation.Out.Sent, Is.EqualTo(1));
      Assert.That(sendConfirmation.Out.Errors, Is.EqualTo(0));
    }
  }
}

