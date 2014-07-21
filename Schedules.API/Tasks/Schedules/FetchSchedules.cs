using System;
using Simpler;
using System.Collections.Generic;
using Schedules.API.Models;
using Schedules.API.Tasks.Schedules;

namespace Schedules.API
{
  public class FetchSchedules : InOutTask<FetchSchedules.Input, FetchSchedules.Output>
  {
    public FetchHolidays FetchHolidays { get; set; }
    public FetchStreetSweepings FetchStreetSweepings { get; set; }

    public class Input
    {
      public Categories Category { get; set; }

      public Address Address { get; set; }
    }

    public class Output
    {
      public List<Schedule> Schedules { get; set; }
    }

    public override void Execute(){
      Out.Schedules = new List<Schedule>();
      switch (In.Category) {
        case Categories.StreetSweeping:
          FetchStreetSweepings.In.Address = In.Address;
          FetchStreetSweepings.Execute();
          Out.Schedules = FetchStreetSweepings.Out.StreetSweepings;
          break;
        case Categories.Holidays:
        default:
          FetchHolidays.Execute();
          Out.Schedules = FetchHolidays.Out.Holidays;
          break;
      }
    }
  }
}

