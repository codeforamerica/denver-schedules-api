using System;
using Simpler;
using Schedules.API.Models;
using Twilio;
using System.Collections.Generic;

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
      var messages = new List<String> ();
      foreach (var reminder in In.DueReminders) 
      {
        var contact = "+" + reminder.Contact;
        var message = reminder.Message + " Reply STOP to unsubscribe.";
        var result = client.SendSmsMessage(number, contact, message);
        var error = result.RestException;
        if (error == null) {
          Out.Sent++;
          messages.Add(message);
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

      Out.Messages = messages.ToArray();
    }
  }
}

