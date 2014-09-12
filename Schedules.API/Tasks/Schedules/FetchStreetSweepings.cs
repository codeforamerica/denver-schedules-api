using System;
using Simpler;
using Schedules.API.Models;
using System.Collections.Generic;
using Schedules.API.Tasks;
using Dapper;
using Nancy;

namespace Schedules.API.Tasks.Schedules
{
  public class FetchStreetSweepings : InOutTask<FetchStreetSweepings.Input, FetchStreetSweepings.Output>
  {
    public CreateSchedulesFromStreetSweeping CreateSchedules { get; set; }

    public class Input
    {// Assuming validation happens elsewhere
      public Address Address { get; set; }
    }

    public class Output
    {
      public List<Schedule> StreetSweepings { get; set; }
    }

    public override void Execute()
    {
      Out.StreetSweepings = new List<Schedule>();
      using (var connection = Db.Connect ()) {
        try{
          var formatPoint = String.Format(point, In.Address.Longitude, In.Address.Latitude);
          var routes = connection.Query<StreetSweeping>(altSelect, new { Point = formatPoint});
          foreach(var route in routes) {
            CreateSchedules.In.StreetSweeping = route;
            CreateSchedules.Execute();
            Out.StreetSweepings.AddRange(CreateSchedules.Out.Schedules);
          }
        }
        catch(Exception ex){
          Console.WriteLine (ex.ToString ());
          throw;
        }
      }
    }

    // TODO: Make this more generic to pull account for different back ends
    // Tightly coupled to postgis
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
                                        hundblkdir as StreetDirection,
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
                                        ST_Intersects(ST_Buffer(ST_GeometryFromText(@Point, 4326), .0002), ss.geom)";
  }
}

