using Nancy;
using Nancy.ModelBinding;
using Schedules.API.Models;
using Simpler;
using Schedules.API.Tasks;

public class RemindersModule : NancyModule
{
  public RemindersModule ()
  {
    Post["/reminders/sms"] = _ => {
      var createSMSReminder = Task.New<CreateReminder>();
      createSMSReminder.In.ReminderTypeName = "sms";
      createSMSReminder.In.Reminder = this.Bind<Reminder>();
      createSMSReminder.Execute();

      return Response.AsJson(createSMSReminder.Out.Reminder, HttpStatusCode.Created);
    };
  }
}
