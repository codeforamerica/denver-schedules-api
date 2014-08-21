using System;
using Nancy;
using Schedules.API.Extensions;
using Schedules.API.Models;

namespace Schedules.API.Modules
{
  public class StatusModule : NancyModule
  {
    public StatusModule ()
    {
      // Engine Light App Monitoring - http://engine-light.codeforamerica.org/
      Get ["/status"] = _ => {
        return Response.AsJson (new State());
      };
    }
  }
}
