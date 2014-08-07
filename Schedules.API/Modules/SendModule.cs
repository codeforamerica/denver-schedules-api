using Nancy;
using Schedules.API.Tasks.Sending;
using Simpler;
using Nancy.ModelBinding;
using Schedules.API.Models;

public class SendModule : NancyModule
{
  public SendModule ()
  {
    Post["/reminders/email/send"] = parameters => {
      var postRemindersEmailSend = Task.New<PostRemindersEmailSend>();
      postRemindersEmailSend.In.Send = this.Bind<Send>();
      postRemindersEmailSend.Execute();

      return Response.AsJson(
        postRemindersEmailSend.Out.Send,
        HttpStatusCode.Created
      );
    };
  }
}
