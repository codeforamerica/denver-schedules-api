using Simpler;
using Schedules.API.Models;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Tasks.Sending
{
  public class PostRemindersSend: InOutTask<PostRemindersSend.Input, PostRemindersSend.Output>
  {
    public class Input
    {
      public string ReminderTypeName { get; set; }
      public Send Send { get; set; }
      public SendReminderBase SendReminders { get; set; }
    }

    public class Output
    {
      public Send Send { get; set; }
    }

    public FetchDueReminders FetchDueReminders { get; set; }

    public override void Execute ()
    {
      FetchDueReminders.In.RemindOn = In.Send.RemindOn;
      FetchDueReminders.In.ReminderTypeName = In.ReminderTypeName;
      FetchDueReminders.Execute();

      In.SendReminders.In.DueReminders = FetchDueReminders.Out.DueReminders;
      In.SendReminders.Execute();

      Out.Send = new Send {
        RemindOn = In.Send.RemindOn,
        Sent = In.SendReminders.Out.Sent,
        Errors = In.SendReminders.Out.Errors
      };
    }
  }
}

