using System;
using Schedules.API.Models;
using Twilio;
using Schedules.API.Helpers;

namespace Schedules.API.Tasks.Sending
{
  public class SendSMS: SendReminderBase
  {
    private const string sidKey = "TWILIO_SID";
    private const string tokenKey = "TWILIO_TOKEN";
    private const string numberKey = "TWILIO_NUMBER";

    public override void Execute ()
    {
      //var sid = EnvironmentVariableHelper.GetEnvironmentVariable(sidKey);
      //var token = EnvironmentVariableHelper.GetEnvironmentVariable(tokenKey);
      //var number = EnvironmentVariableHelper.GetEnvironmentVariable(numberKey);


      string sid = "ACf6fc78c90acbedbca9765990760d84fb"; 
      string token = "e44e41839bf11f442be4d0a642f4c86c"; 
      string number = "+19709991126";

      var client = new TwilioRestClient (sid, token);

      foreach (var reminder in In.DueReminders) 
      {
        var contact = "+" + reminder.Contact;
        var result = client.SendSmsMessage(number, contact , reminder.Message);
        var error = result.RestException;
        if (error == null) {
          Out.Sent++;
          Console.WriteLine("Sent!");
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

