using System;
using Simpler;
using Twilio;
using Schedules.API.Helpers;

namespace Schedules.API.Tasks.Sending
{
  public class SendSMSConfirmation : InOutTask<SendSMSConfirmation.Input, SendSMSConfirmation.Output>
  {
    private const string sidKey = "TWILIO_SID";
    private const string tokenKey = "TWILIO_TOKEN";
    private const string numberKey = "TWILIO_NUMBER";
    private const string message = "You are now subscribed to street sweeping reminders on {0} near {1}. Text STOP to unsubscribe."; // TODO: Get this from the config

    public class Input
    {
      public String Contact { get; set; }
      public String StreetName { get; set; }
      public String Address { get; set; }
    }

    public class Output
    {
      public int Sent { get; set; }
      public int Errors { get; set; }
    }

    public override void Execute()
    {
      var sid = EnvironmentVariableHelper.GetEnvironmentVariable(sidKey);
      var token = EnvironmentVariableHelper.GetEnvironmentVariable(tokenKey);
      var number = EnvironmentVariableHelper.GetEnvironmentVariable(numberKey);

      var client = new TwilioRestClient (sid, token);

      var result = client.SendSmsMessage(number, In.Contact, String.Format(message, In.StreetName, In.Address));
      var error = result.RestException;
      if (error == null) {
        Out.Sent++;
      } else {
        Out.Errors++;
        Console.WriteLine(
          "SMS to {0} failed with a status of {1} and reason of {2}.",
          In.Contact,
          error.Status,
          error.Code + ": " + error.Message
        );
      }
    }
  }
}

