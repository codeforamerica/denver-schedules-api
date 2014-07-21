using Nancy;
using Schedules.API.Repositories;
using Schedules.API.Extensions;
using Schedules.API.Models;
using Schedules.API.Tasks.Schedules;
using System.Collections.Generic;

public class SchedulesModule : NancyModule
{
  public SchedulesModule()
  {
    Get ["/schedules/{category}"] = _ => {
      string c = _.category.ToString();
      var category = c.ToEnum<Categories>();
      var repository = new SchedulesRepository();
      List<Schedule> schedules = repository.Get(category, Request.Query);

      return Response.AsJson (schedules);
    };
  }
}
