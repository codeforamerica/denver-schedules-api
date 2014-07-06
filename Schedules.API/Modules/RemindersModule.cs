using Nancy;
using Nancy.ModelBinding;
using Schedules.API.Models;
using System;
using Schedules.API.Helpers;

public class RemindersModule : NancyModule
{
  public RemindersModule()
  {
    Post ["/reminders"] = _ => {
        Reminder reminder = this.Bind<Reminder>();
        return Response.AsJson(reminder, HttpStatusCode.Created);
    };

    Post ["/reminders/notify"] = _ => {
      return Response.AsJson(Notifier.TextDoIt (), HttpStatusCode.Created);
    };
  }
}
