using Nancy;
using Nancy.ModelBinding;
using Schedules.API.Models;
using System;

public class RemindersModule : NancyModule
{
  public RemindersModule ()
  {
    Post["/reminders/sms"] = _ => {
      Reminder reminder = this.Bind<Reminder>();
      reminder.ReminderType = new ReminderType {Name="sms"};
      return Response.AsJson(reminder, HttpStatusCode.Created);
    };
  }
}
