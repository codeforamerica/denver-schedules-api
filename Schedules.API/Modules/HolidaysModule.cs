using Nancy;
using Schedules.API.Tasks.Schedules;
using Simpler;

namespace Schedules.API.Modules
{
  public class HolidaysModule : NancyModule
  {
    public HolidaysModule()
    {
      Get ["/schedules/holidays"] = _ => {
        FetchHolidays fetchHolidays = Task.New<FetchHolidays>();
        fetchHolidays.Execute();
        return Response.AsJson(fetchHolidays.Out.Holidays);
      };
    }
  }
}