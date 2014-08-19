using Simpler;
using Schedules.API.Models;
using Mandrill;
using System.Collections.Generic;
using System;

namespace Schedules.API.Tasks.Sending
{
  public class SendEmails : InOutTask<SendEmails.Input, SendEmails.Output>
  {
    public class Input
    {
      public Reminder[] DueReminders { get; set; }
    }

    public class Output
    {
      public int Sent { get; set; }
      public int Errors { get; set; }
    }

    public override void Execute ()
    {
      var apiKey = Environment.GetEnvironmentVariable("MANDRILL_API_KEY");
      if (String.IsNullOrEmpty(apiKey)) throw new Exception ("MANDRILL_API_KEY environment variable not found.");

      var fromEmail = Environment.GetEnvironmentVariable("MANDRILL_FROM_EMAIL");
      if (String.IsNullOrEmpty(fromEmail)) throw new Exception ("MANDRILL_FROM_EMAIL environment variable not found.");

      var api = new MandrillApi (apiKey);

      Console.WriteLine("Sending {0} emails.", In.DueReminders.Length);
      foreach (var reminder in In.DueReminders) {
        var results = api.SendMessage(
          new EmailMessage {
            to = new List<EmailAddress> { new EmailAddress { email = reminder.Contact } },
            from_email = fromEmail,
            subject = "Reminder!",
            text = reminder.Message
        });

        // This assumes that emails are sent one at time to each address, therefore just look
        // at the first result.
        EmailResult result = results[0];
        if (result.Status == EmailResultStatus.Sent) {
          Out.Sent++;
        } else {
          Out.Errors++;
          Console.WriteLine(
            "Email {0} failed with a status of {1} and reason of {2}.",
            result.Email,
            result.Status,
            result.RejectReason
          );
        }
      }
    }
  }
}
