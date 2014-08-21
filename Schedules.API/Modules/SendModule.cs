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
        var sendEmailReminders = Task.New<SendEmails>();
        var send = SendReminders("email", sendEmailReminders);
        return Response.AsJson(
          send,
          HttpStatusCode.Created
        );
      };

      Post["/reminders/sms/send"] = parameters => {
        var sendSMSReminders = Task.New<SendSMS>();
        var send = SendReminders("sms", sendSMSReminders);
        return Response.AsJson(
          send,
          HttpStatusCode.Created
        );
      };
    }

    private Send SendReminders(string type, SendReminderBase sendReminders)
    {
      this.RequiresAuthentication();
      this.RequiresClaims(new[] { "admin" });

      var postRemindersSend = Task.New<PostRemindersSend>();
      postRemindersSend.In.ReminderTypeName = type;
      postRemindersSend.In.Send = this.Bind<Send>();
      postRemindersSend.In.SendReminders = sendReminders;
      postRemindersSend.Execute();

      return postRemindersSend.Out.Send;
    }
  }
}
