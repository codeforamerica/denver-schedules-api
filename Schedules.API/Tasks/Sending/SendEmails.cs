using Schedules.API.Models;
using Mandrill;
using System.Collections.Generic;
using System;
using Schedules.API.Helpers;

namespace Schedules.API.Tasks.Sending
{
  public class SendEmails : SendReminderBase
  {
    private const string mandrillAPIKey = "MANDRILL_API_KEY";
    private const string fromEmailKey = "MANDRILL_FROM_EMAIL";

    public override void Execute ()
    {
      var apiKey = EnvironmentVariableHelper.GetEnvironmentVariable(mandrillAPIKey);
      var fromEmail = EnvironmentVariableHelper.GetEnvironmentVariable(fromEmailKey);

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
