using System;
using Simpler;
using Schedules.API.Models;
using System.Collections.Generic;
using Schedules.API.Helpers;

namespace Schedules.API.Tasks.Schedules
{
  public class CreateSchedulesFromStreetSweeping : InOutTask<CreateSchedulesFromStreetSweeping.Input, CreateSchedulesFromStreetSweeping.Output>
  {
    private static bool [] streetSweepingMonths = new bool [] {false, false, false, true, true, true, true, true, true, true, true, false};
    private const string noSchedules = "No schedules possible";

    public class Input 
    {
      public StreetSweeping StreetSweeping { get; set; }
    }

    public class Output
    {
      public List<Schedule> Schedules { get; set; }
    }

    public override void Execute()
    {
      CalculateSweepDirection();
      Out.Schedules = new List<Schedule> ();
      var leftWeekAndDay = GetWeekAndDay (In.StreetSweeping.LeftSweep);
      var rightWeekAndDay = GetWeekAndDay (In.StreetSweeping.RightSweep);

      if (!In.StreetSweeping.Error.Contains(noSchedules)) {
        if (In.StreetSweeping.LeftSweep.Equals(In.StreetSweeping.RightSweep)) {
          Out.Schedules.Add(new Schedule () {
            Name = In.StreetSweeping.FullName,
            Category = Categories.StreetSweeping.ToString(),
            Description = In.StreetSweeping.LeftSweepDirection + " & " + In.StreetSweeping.RightSweepDirection,
            Upcoming = Scheduler.CalculateDatesForRestOfYear(leftWeekAndDay[0], leftWeekAndDay[1], streetSweepingMonths),
            Error = In.StreetSweeping.Error
          });
        } else {
          Out.Schedules.Add(new Schedule () { 
            Name = In.StreetSweeping.FullName, 
            Category = Categories.StreetSweeping.ToString(),
            Description = In.StreetSweeping.LeftSweepDirection,
            Upcoming = Scheduler.CalculateDatesForRestOfYear(leftWeekAndDay[0], leftWeekAndDay[1], streetSweepingMonths),
            Error = In.StreetSweeping.Error
          });

          Out.Schedules.Add(new Schedule () { 
            Name = In.StreetSweeping.FullName, 
            Category = Categories.StreetSweeping.ToString(),
            Description = In.StreetSweeping.RightSweepDirection,
            Upcoming = Scheduler.CalculateDatesForRestOfYear(rightWeekAndDay[0], rightWeekAndDay[1], streetSweepingMonths),
            Error = In.StreetSweeping.Error
          });
        }
      }
    }

    private int [] GetWeekAndDay(string sweep) {
      var weekAndDay = new int [2];
      var valueIsEmpty = String.IsNullOrEmpty (sweep);
      var secondValueIsX = sweep.Length > 1 && sweep.ToUpper()[1] == 'X'; // This condition should not occur in the data anymore.
      var streetNotMaintainedByDenver = sweep.ToUpper() == "NDM";
      var valueIsN = sweep.ToUpper() == "N";

      if (valueIsEmpty)
        In.StreetSweeping.Error = String.Format("{0} Reason: No data Available.", noSchedules);
      else if (streetNotMaintainedByDenver)
        In.StreetSweeping.Error = String.Format("{0} Reason: Street not maintained by the City and County of Denver.", noSchedules);
      else if (secondValueIsX)
        In.StreetSweeping.Error = String.Format ("{0} Reason: Week {1}, day unknown", noSchedules, sweep[0]);
      else if (valueIsN)
        In.StreetSweeping.Error = "Nightly"; // this really isn't an error
      else {
        weekAndDay[0] = int.Parse (sweep [0].ToString ());
        weekAndDay[1] = int.Parse (sweep [1].ToString ()) - 1; // Day of Week index is 0 - 6
      }

      return weekAndDay;
    }

    private void CalculateSweepDirection()
    {
      // Result is left: N, right: S if
      // Quad: SE, Dir: E or W
      // Quad: NE, Dir: E or W
      // Quad: E, Dir: E
      var isEW = (In.StreetSweeping.AddressQuadrant.Equals("E") && In.StreetSweeping.StreetDirection.Equals("E"))
        || ((In.StreetSweeping.AddressQuadrant.Equals("SE") || In.StreetSweeping.AddressQuadrant.Equals("NE"))
            && (In.StreetSweeping.StreetDirection.Equals("E") || In.StreetSweeping.StreetDirection.Equals("W")));

      // Result is left: E, right: W if
      // Quad: SE, Dir: N or S
      // Quad: SW, Dir: N or S
      // Quad: S, Dir: E
      var isSN = (In.StreetSweeping.AddressQuadrant.Equals("S") && In.StreetSweeping.StreetDirection.Equals("E"))
        || ((In.StreetSweeping.AddressQuadrant.Equals("SE") || In.StreetSweeping.AddressQuadrant.Equals("SW")) 
            && (In.StreetSweeping.StreetDirection.Equals("N") || In.StreetSweeping.StreetDirection.Equals("S")));

      // Results is left: W, right: E if
      // Quad: NE, Dir: N or S
      // Quad: NW, Dir: N or S
      // Quad: N, Dir: E
      var isNS = (In.StreetSweeping.AddressQuadrant.Equals("N") && In.StreetSweeping.StreetDirection.Equals("E"))
        || ((In.StreetSweeping.AddressQuadrant.Equals("NE") || In.StreetSweeping.AddressQuadrant.Equals("NW")) 
            && (In.StreetSweeping.StreetDirection.Equals("N") || In.StreetSweeping.StreetDirection.Equals("S")));

      // Result is left: S, right: N if
      // Quad: SW, Dir: E or W
      // Quad: NW, Dir: E or W
      // Quad: W, Dir: N
      var isWE = (In.StreetSweeping.AddressQuadrant.Equals("W") && In.StreetSweeping.StreetDirection.Equals("N"))
        || ((In.StreetSweeping.AddressQuadrant.Equals("SW") || In.StreetSweeping.AddressQuadrant.Equals("NW")) 
            && (In.StreetSweeping.StreetDirection.Equals("E") || In.StreetSweeping.StreetDirection.Equals("W")));

      if (isEW) {
        In.StreetSweeping.LeftSweepDirection = "East";
        In.StreetSweeping.RightSweepDirection = "West";
      } else if (isSN) {
        In.StreetSweeping.LeftSweepDirection = "South";
        In.StreetSweeping.RightSweepDirection = "North";
      } else if (isNS) {
        In.StreetSweeping.LeftSweepDirection = "North";
        In.StreetSweeping.RightSweepDirection = "South";
      } else if (isWE) {
        In.StreetSweeping.LeftSweepDirection = "West";
        In.StreetSweeping.RightSweepDirection = "East";
      }
    }
  }
}

