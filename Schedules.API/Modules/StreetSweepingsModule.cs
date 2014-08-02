using System;
using Nancy;
using Simpler;
using Schedules.API.Models;


namespace Schedules.API
{
  public class StreetSweepingsModule: NancyModule
  {
    const string latitudeParameter = "latitude";
    const string longitudeParameter = "longitude";

    public StreetSweepingsModule()
    {
      Get ["/schedules/streetsweeping"] = _ => {
        try{
          ValidateStreetSweepingParameters();
          FetchStreetSweepings fetchStreetSweepings = Task.New<FetchStreetSweepings>();
          fetchStreetSweepings.In.Address = new Address {
            Longitude = Request.Query[longitudeParameter],
            Latitude = Request.Query[latitudeParameter]
          };
          fetchStreetSweepings.Execute();

          return Response.AsJson (fetchStreetSweepings.Out.StreetSweepings);
        }
        catch(ArgumentNullException ex){
          return Response.AsJson(ex, HttpStatusCode.BadRequest);
        }
      };
    }

    void ValidateStreetSweepingParameters ()
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

