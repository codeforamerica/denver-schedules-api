using Nancy;
using Schedules.API.Tasks.Sending;
using Simpler;

public class SendModule : NancyModule
{
  public SendModule ()
  {
    Post["/reminders/email/send"] = parameters => {
      var postRemindersEmailSend = Task.New<PostRemindersEmailSend>();
      postRemindersEmailSend.Execute();

      return Response.AsJson(
        postRemindersEmailSend.Out.Send,
        HttpStatusCode.Created
      );
    };
  }
}
