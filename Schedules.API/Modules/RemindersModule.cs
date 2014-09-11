using Nancy;
using Nancy.ModelBinding;
using Schedules.API.Models;
using Simpler;
using Schedules.API.Tasks.Reminders;

namespace Schedules.API.Modules
{
  public class RemindersModule : NancyModule
  {
    public RemindersModule ()
    {
      Post["/reminders/sms"] = _ => {
        var createSMSReminder = CreateAReminder("sms");
        return Response.AsJson(createSMSReminder.Out.Reminder, HttpStatusCode.Created);
      };

      Post["/reminders/email"] = _ => {
        var createEmailReminder = CreateAReminder("email");
        return Response.AsJson(createEmailReminder.Out.Reminder, HttpStatusCode.Created);
      };
    }

    private CreateReminder CreateAReminder(string reminderTypeName)
    {
      var createReminder = Task.New<CreateReminder>();
      createReminder.In.ReminderTypeName = reminderTypeName;
      createReminder.In.Reminder = this.Bind<Reminder>();
      createReminder.Execute();
      return createReminder;
    }
  }
}
