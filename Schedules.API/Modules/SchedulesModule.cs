using Nancy;
using Schedules.API;
using Schedules.API.Extensions;
using Schedules.API.Models;
using Schedules.API.Tasks.Schedules;
using Simpler;
using System;

namespace Schedules.API.Modules
{
  public class SchedulesModule : NancyModule
  {
    public SchedulesModule()
    {
      Get ["/schedules/holidays"] = _ => {
        try{
          var category = Categories.Holidays;
          FetchSchedules fetchSchedules = Task.New<FetchSchedules>();
          fetchSchedules.Execute();

          return Response.AsJson (fetchSchedules.Out.Schedules);
        }
        catch(ArgumentNullException ex){
          return Response.AsJson(ex, HttpStatusCode.BadRequest);
        }
      };
    }
  }
}