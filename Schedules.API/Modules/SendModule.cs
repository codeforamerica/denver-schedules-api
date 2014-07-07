using Nancy;
using Schedules.API.Tasks.Sending;
using Simpler;

public class SendModule : NancyModule
{
  public SendModule ()
  {
    Post["/reminders/email/send"] = parameters => {
      var postRemindersEmailSend = Task.New<PostRemindersEmailSend>();
      postRemindersEmailSend.In.Module = this;
      postRemindersEmailSend.In.Parameters = parameters;
      postRemindersEmailSend.Execute();
      return postRemindersEmailSend.Out.Response;
    };
  }
}
