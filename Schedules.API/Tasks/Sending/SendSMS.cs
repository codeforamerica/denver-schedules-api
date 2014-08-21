using System;
using Simpler;
using Schedules.API.Models;
using Twilio;

namespace Schedules.API.Tasks.Sending
{
  public class SendSMS: SendReminderBase
  {
    public override void Execute ()
    {
      var sid = Environment.GetEnvironmentVariable ("TWILIO_SID");
      var token = Environment.GetEnvironmentVariable ("TWILIO_TOKEN");
      var number = Environment.GetEnvironmentVariable ("TWILIO_NUMBER");

      var client = new TwilioRestClient (sid, token);

      foreach (var reminder in In.DueReminders) 
      {
        var contact = "+" + reminder.Contact;
        var result = client.SendSmsMessage(number, contact, reminder.Message);
        var error = result.RestException;
        if (error == null) {
          Out.Sent++;
        } else {
          Out.Errors++;
          Console.WriteLine(
            "SMS to {0} failed with a status of {1} and reason of {2}.",
            reminder.Contact,
            error.Status,
            error.Code + ": " + error.Message
          );
        }
      }
    }
  }
}

