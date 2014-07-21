using System;
using Schedules.API.Models;
using System.Collections.Generic;
using Nancy;
using Dapper;
using System.Linq;
using Simpler;
using Schedules.API.Tasks.Schedules;

namespace Schedules.API.Repositories
{
  public class SchedulesRepository:RepositoryBase
  {
    private const string longitude = "longitude";
    private const string latitude = "latitude";
    private const string point = @"POINT({0} {1})";
    private const string altSelect = @"SELECT gid,
                                        streetid,
                                        prefix,
                                        name,
                                        type,
                                        fullname,
                                        fromname,
                                        toname,
                                        len_mi as LenMi,
                                        jurisid as JurisdictionId,
                                        maintnce as Maintenance,
                                        addressqua as AddressQuadrant,
                                        zipleft,
                                        zipright,
                                        oneway,
                                        route,
                                        sssid,
                                        leftsweep,
                                        rtsweep as RightSweep
                                 FROM
                                        streetsweeping ss
                                 WHERE
                                        ST_Intersects(ST_Buffer(ST_GeometryFromText(@Point, 4326), .001), ss.geom)";

    public SchedulesRepository ()
    {
    }

    public List<Schedule> Get (Categories category, DynamicDictionary dictionary)
    {
      var schedules = new List<Schedule> ();
      switch (category) {
        case Categories.StreetSweeping:
          if (dictionary.ContainsKey (longitude) && dictionary.ContainsKey (latitude)) {
            var address = new Address { Longitude = dictionary [longitude], Latitude = dictionary [latitude] };
            var fetchStreetSweeping = Task.New<FetchStreetSweepings>();
            fetchStreetSweeping.In.Address = address;
            fetchStreetSweeping.Execute();
            schedules = fetchStreetSweeping.Out.StreetSweepings;
          }
          break;
        default:
          var fetchHolidays = Task.New<FetchHolidays>();
          fetchHolidays.Execute();
          schedules = fetchHolidays.Out.Holidays;
          break;
      }
      return schedules;
    }

    public List<Schedule> GetSchedules (Address address)
    {
      List<Schedule> schedules = new List<Schedule> ();
      try{
        // TODO: Make this more generic to pull account for different back ends
        // Tightly coupled to postgis
        var formatPoint = String.Format(point, address.Longitude, address.Latitude);
        var routes = connection.Query<StreetSweeping>(altSelect, new { Point = formatPoint});
        foreach(var route in routes) {
          schedules.AddRange(route.CreateSchedules());
        }
      }
      catch(Exception ex){
        Console.WriteLine (ex.ToString ());
      }
      finally {
        connection.Close ();
      }

      return schedules.Distinct (new Schedule.ScheduleComparer()).ToList ();
    }
  }
}

