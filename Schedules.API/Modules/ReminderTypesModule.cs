using Nancy;
using Schedules.API.Models;
using System;
using System.Collections.Generic;
using Simpler;
using Schedules.API.Tasks.ReminderTypes;

namespace Schedules.API.Modules
{
  public class ReminderTypesModule : NancyModule
  {
    public ReminderTypesModule ()
    {
      Get ["/reminderTypes"] = _ => {
        var fetchReminderTypes = Task.New<FetchReminderTypes>();
        fetchReminderTypes.Execute();
        var reminderTypes = fetchReminderTypes.Out.ReminderTypes;
        return Response.AsJson (reminderTypes);
      };
    }
  }
}

