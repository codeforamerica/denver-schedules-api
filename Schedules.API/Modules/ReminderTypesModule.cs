using Nancy;
using Schedules.API.Models;
using System;
using System.Collections.Generic;

namespace Schedules.API.Modules
{
  public class ReminderTypesModule : NancyModule
  {
    public ReminderTypesModule ()
    {
      Get ["/reminderTypes"] = _ => {
        var reminderTypes = new List<string>();
        return Response.AsJson (reminderTypes);
      };
    }
  }
}

