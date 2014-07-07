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
      public NancyModule Module { get; set; }
      public dynamic Parameters { get; set; }
    }

    public class Output
    {
      public dynamic Response { get; set; }
    }

    public FetchDueReminders FetchDueReminders { get; set; }
    public SendEmails SendEmails { get; set; }

    public override void Execute ()
    {
      FetchDueReminders.Execute();

      SendEmails.In.DueReminders = FetchDueReminders.Out.DueReminders;
      SendEmails.Execute();

      Out.Response = In.Module.Response.AsJson(
        new Send { Sent = SendEmails.Out.Sent },
        HttpStatusCode.Created
      );
    }
  }
}
