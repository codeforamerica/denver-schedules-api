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
    string latitudeParameter = "latitude";
    string longitudeParameter = "longitude";

    public SchedulesModule()
    {
      Get ["/schedules/{category}"] = _ => {
        try{
          string c = _.category.ToString();
          var category = c.ToEnum<Categories>();
          FetchSchedules fetchSchedules = Task.New<FetchSchedules>();
          fetchSchedules.In.Category = category;

          if(category == Categories.StreetSweeping){
            // TODO: Where's the best place to do error checking
            ValidateStreetSweepingParameters(category, fetchSchedules);

            fetchSchedules.In.Address = new Address {
              Longitude = Request.Query[longitudeParameter],
              Latitude = Request.Query[latitudeParameter]
            };
          }
          fetchSchedules.Execute();

          return Response.AsJson (fetchSchedules.Out.Schedules);
        }
        catch(ArgumentNullException ex){
          return Response.AsJson(ex, HttpStatusCode.BadRequest);
        }
      };
    }

    void ValidateStreetSweepingParameters (Categories category, FetchSchedules fetchSchedules)
    {
      // Make sure we have a lat and long or throw an error
      var hasLat = Request.Query.ContainsKey(latitudeParameter);
      var hasLong = Request.Query.ContainsKey(longitudeParameter);

      if(!hasLat)
        throw new ArgumentNullException(latitudeParameter);
      else if (!hasLong)
        throw new ArgumentNullException(longitudeParameter);
    }
  }
}