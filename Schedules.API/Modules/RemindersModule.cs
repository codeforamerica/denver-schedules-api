using Nancy;
using Nancy.ModelBinding;
using Schedules.API.Models;
using System;

public class RemindersModule : NancyModule
{
    public RemindersModule()
    {
        Post ["/reminders"] = _ => {
            Reminder reminder = this.Bind<Reminder>();
            return Response.AsJson(reminder, HttpStatusCode.Created);
        };
    }
}
