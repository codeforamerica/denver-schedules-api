using System;
using Schedules.API.Models;
using System.Collections.Generic;
using Nancy;
using Dapper;
using System.Linq;

namespace Schedules.API.Repositories
{
  public class SchedulesRepository:RepositoryBase
  {
    private const string longitude = "longitude";
    private const string latitude = "latitude";
    private string point = @"POINT({0} {1})";
    private string altSelect = @"SELECT gid,
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

    public enum Categories
    {
      Holidays,
      StreetSweeping
    }

    public SchedulesRepository ()
    {
    }

    public List<Schedule> Get (SchedulesRepository.Categories category, Nancy.DynamicDictionary dictionary)
    {
      var schedules = new List<Schedule> ();
      switch (category) {
        case Categories.StreetSweeping:
          if (dictionary.ContainsKey (longitude) && dictionary.ContainsKey (latitude)) {
            var address = new Address { Longitude = dictionary [longitude], Latitude = dictionary [latitude] };
            schedules = GetSchedules (address);
          }
          break;
        case Categories.Holidays:
        default:
          schedules = GetHolidays ();
          break;
      }
      return schedules;
    }

    public List<Schedule> GetHolidays() {
      return new List<Schedule> (){
        new Schedule(){
          Name = "New Years Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 1, 1)}
        },
        new Schedule(){
          Name = "Martin Luther King Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 1, 20)}
        },
        new Schedule(){
          Name = "President's Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 2, 27)}
        },
        new Schedule(){
          Name = "Cesar Chavez Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 3, 31)}
        },
        new Schedule(){
          Name = "Memorial Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 5, 26)}
        },
        new Schedule(){
          Name = "Independence Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 7, 4)}
        },
        new Schedule(){
          Name = "Labor Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 11, 1)}
        },
        new Schedule(){
          Name = "Thanksgiving Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 11, 27)}
        },
        new Schedule(){
          Name = "Christmas Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 12, 25)}
        }
      };
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

