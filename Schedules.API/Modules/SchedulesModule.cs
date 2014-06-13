using Nancy;
using Schedules.API.Repositories;

public class SchedulesModule : NancyModule
{
    public SchedulesModule()
    {
      Get ["/schedules"] = _ => {
        var repository = new SchedulesRepository();
        return Response.AsJson (repository.GetHolidays());
      };
    }
}
