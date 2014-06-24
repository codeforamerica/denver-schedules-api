using System;
using Twilio;
using Schedules.API.Repositories;
using System.Configuration;

namespace Schedules.API.Helpers
{
  public class Results
  {
    public int Sent {
      get;
      set;
    }
  }

  public static class Notifier
  {
    public static Results DoIt() {
      var sid = System.Environment.GetEnvironmentVariable ("TWILIO-SID");
      var token = System.Environment.GetEnvironmentVariable ("TWILIO-TOKEN");
      var number = System.Environment.GetEnvironmentVariable ("TWILIO-NUMBER");
      var client = new TwilioRestClient (sid, token);
      var results = new Results ();

      foreach (var reminder in ReminderRepository.Get()) {
        var sent = client.SendSmsMessage (number, reminder.Cell, reminder.Message);
        if (sent.RestException != null)
          Console.WriteLine (sent.RestException);
        else
          results.Sent++;
      }

      return results;
    }
  }
}

