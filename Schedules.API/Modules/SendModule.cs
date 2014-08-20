using Nancy;
using Schedules.API.Tasks.Sending;
using Simpler;
using Nancy.ModelBinding;
using Schedules.API.Models;
using Nancy.Security;

namespace Schedules.API.Modules
{
  public class SendModule : NancyModule
  {
    public SendModule ()
    {
      Post["/reminders/email/send"] = parameters => {
        this.RequiresAuthentication();
        this.RequiresClaims(new[] { "admin" });

        var postRemindersEmailSend = Task.New<PostRemindersSend>();
        postRemindersEmailSend.In.ReminderTypeName = "email";
        postRemindersEmailSend.In.Send = this.Bind<Send>();
        postRemindersEmailSend.Execute();

        return Response.AsJson(
          postRemindersEmailSend.Out.Send,
          HttpStatusCode.Created
        );
      };

      Post["/reminders/sms/send"] = parameters => {
        this.RequiresAuthentication();
        this.RequiresClaims(new[] { "admin" });

        var postRemindersSMSSend = Task.New<PostRemindersSend>();
        postRemindersSMSSend.In.ReminderTypeName = "sms";
        postRemindersSMSSend.In.Send = this.Bind<Send>();
        postRemindersSMSSend.Execute();

        return Response.AsJson(
          postRemindersSMSSend.Out.Send,
          HttpStatusCode.Created
        );
      };
    }
  }
}
