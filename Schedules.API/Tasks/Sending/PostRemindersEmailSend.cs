using Simpler;
using Nancy;
using Schedules.API.Models;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tasks.Sending
{
  public class PostRemindersEmailSend : InOutTask<PostRemindersEmailSend.Input, PostRemindersEmailSend.Output>
  {
    public class Input
    {
      public Send Send { get; set; }
    }

    public class Output
    {
      public Send Send { get; set; }
    }

    public FetchDueReminders FetchDueReminders { get; set; }
    public SendEmails SendEmails { get; set; }

    public override void Execute ()
    {
      FetchDueReminders.In.RemindOn = In.Send.RemindOn;
      FetchDueReminders.Execute();

      SendEmails.In.DueReminders = FetchDueReminders.Out.DueReminders;
      SendEmails.Execute();

      Out.Send = new Send {
        Sent = SendEmails.Out.Sent,
        Errors = SendEmails.Out.Errors
      };
    }
  }
}
