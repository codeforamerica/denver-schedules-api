using System;
using System.Collections.Generic;
using Schedules.API.Helpers;
using Schedules.API.Tasks.Schedules;

namespace Schedules.API.Models
{
  /// <summary>
  /// Segment of a street sweeping route
  /// </summary>
  public class StreetSweeping
  {
    private static bool [] streetSweepingMonths = new bool [] {false, false, false, true, true, true, true, true, true, true, true, false};
    private const string noSchedules = "No schedules possible";

    public StreetSweeping ()
    {
      LeftSweep = string.Empty;
      RightSweep = string.Empty;
      LeftSweepDirection = string.Empty;
      RightSweepDirection = string.Empty;
      AddressQuadrant = string.Empty;
      StreetDirection = string.Empty;
    }

    /// <summary>
    /// Unique Identifier
    /// </summary>
    /// <value>The GI.</value>
    public int GID {
      get;
      set;
    }

    /// <summary>
    /// Unique Identifier for the street name
    /// </summary>
    /// <value>The street identifier.</value>
    public double StreetId {
      get;
      set;
    }

    /// <summary>
    /// Direction modifier
    /// Ex: N, S, E, W
    /// </summary>
    /// <value>The prefix.</value>
    public string Prefix {
      get;
      set;
    }

    /// <summary>
    /// Street Name
    /// </summary>
    /// <value>The name.</value>
    public string Name {
      get;
      set;
    }

    /// <summary>
    /// Type of Street
    /// Ex: Avenue, Street, etc
    /// </summary>
    /// <value>The type.</value>
    public string Type {
      get;
      set;
    }

    /// <summary>
    /// Full name of street: prefix + name + type
    /// </summary>
    /// <value>The full name.</value>
    public string FullName {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the name of the from street for the segment.
    /// </summary>
    /// <value>The name of the from.</value>
    public string FromName {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the name of the to street for the segment.
    /// </summary>
    /// <value>The name of the to.</value>
    public string ToName {
      get;
      set;
    }

    /// <summary>
    /// Length of the segment in miles
    /// </summary>
    /// <value>The length mi.</value>
    public double LenMi {
      get;
      set;
    }

    /// <summary>
    /// Jurisdiction the segment is in
    /// </summary>
    /// <value>The juris identifier.</value>
    public string JurisdictionId {
      get;
      set;
    }

    /// <summary>
    /// Who maintains the street
    /// </summary>
    /// <value>The maintnce.</value>
    public string Maintenance {
      get;
      set;
    }

    /// <summary>
    /// Address Quadrant
    /// Ex: N, S, E, W
    /// </summary>
    /// <value>The address qua.</value>
    public string AddressQuadrant {
      get;
      set;
    }

    /// <summary>
    /// The direction the street is running
    /// </summary>
    /// <value>The street direction.</value>
    public string StreetDirection { get; set; }

    /// <summary>
    /// Zip on left side
    /// </summary>
    /// <value>The zip left.</value>
    public string ZipLeft {
      get;
      set;
    }

    /// <summary>
    /// Zip on right side
    /// </summary>
    /// <value>The zipright.</value>
    public string ZipRight {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Schedules.API.Models.StreetSweeping"/> is oneway.
    /// </summary>
    /// <value><c>true</c> if oneway; otherwise, <c>false</c>.</value>
    public bool OneWay {
      get;
      set;
    }

    /// <summary>
    /// Route name the segment belongs to
    /// </summary>
    /// <value>The route.</value>
    public string Route {
      get;
      set;
    }

    /// <summary>
    /// No clue, but surely it's important!
    /// </summary>
    /// <value>The sssid.</value>
    public string SssId {
      get;
      set;
    }

    /// <summary>
    /// The magic of when sweeping happens on the left side of the road
    /// The two digit values represent the week of the month (first digit) 
    /// and the day of the week (second digit); that the street segment is swept on. 
    /// Sunday =1, Monday=2, Tuesday=3, Wednesday=4, Thursday=5, Friday=6, Saturday=7
    /// A ‘0’ indicates the sweep days are not posted.
    /// </summary>
    /// <value>The left sweep.</value>
    public string LeftSweep {
      get;
      set;
    }

    /// <summary>
    /// The magic of when sweeping happens on the right side of the road
    /// The two digit values represent the week of the month (first digit) 
    /// and the day of the week (second digit); that the street segment is swept on. 
    /// Sunday =1, Monday=2, Tuesday=3, Wednesday=4, Thursday=5, Friday=6, Saturday=7
    /// A ‘0’ indicates the sweep days are not posted.
    /// </summary>
    /// <value>The right sweep.</value>
    public string RightSweep {
      get;
      set;
    }

    /// <summary>
    /// The city of Denver’s street grid is a type of plan in which streets run at right angles to each other, forming a grid.
    /// The intersection of Broadway and Ellsworth is the 0,0 point for Denver. Broadway is the North-South line and the zero 
    /// point for the numbers of streets moving East and West.
    /// Ellsworth is the East-West line and the zero point for the numbering as they go North and South.
    /// TODO: Once the logic that sets this is proven, will add as column in db
    /// </summary>
    /// <value>The direction of the left side.</value>
    public string LeftSweepDirection { get; set; }

    /// <summary>
    /// The city of Denver’s street grid is a type of plan in which streets run at right angles to each other, forming a grid.
    /// The intersection of Broadway and Ellsworth is the 0,0 point for Denver. Broadway is the North-South line and the zero 
    /// point for the numbers of streets moving East and West.
    /// Ellsworth is the East-West line and the zero point for the numbering as they go North and South.
    /// TODO: Once the logic that sets this is proven, will add as column in db
    /// </summary>
    /// <value>The direction of the left side.</value>
    public string RightSweepDirection { get; set; }

    /// <summary>
    /// Gets or sets the error if no dates can be generated.
    /// </summary>
    /// <value>The error.</value>
    public string Error {
      get;
      set;
    }
    // TODO: NO! This should be a dumb object
    public List<Schedule> CreateSchedules() {
      CalculateSweepDirection();
      var schedules = new List<Schedule> ();
      var leftWeekAndDay = GetWeekAndDay (LeftSweep);
      var rightWeekAndDay = GetWeekAndDay (RightSweep);

      if (Error.Contains(noSchedules))
        return schedules;

      if (LeftSweep.Equals (RightSweep)) {
        schedules.Add (new Schedule () {
          Name = FullName,
          Category = Categories.StreetSweeping.ToString (),
          Description = LeftSweepDirection + " & " + RightSweepDirection,
          Upcoming = Scheduler.CalculateDatesForRestOfYear(leftWeekAndDay[0], leftWeekAndDay[1], streetSweepingMonths),
          Error = Error
        });
      } else {
        schedules.Add( new Schedule () { 
          Name = FullName, 
          Category = Categories.StreetSweeping.ToString (),
          Description = LeftSweepDirection,
          Upcoming = Scheduler.CalculateDatesForRestOfYear(leftWeekAndDay[0], leftWeekAndDay[1], streetSweepingMonths),
          Error = Error
        });

        schedules.Add (new Schedule () { 
          Name = FullName, 
          Category = Categories.StreetSweeping.ToString (),
          Description = RightSweepDirection,
          Upcoming = Scheduler.CalculateDatesForRestOfYear(rightWeekAndDay[0], rightWeekAndDay[1], streetSweepingMonths),
          Error = Error
        });
      }

      return schedules;
    }

    private int [] GetWeekAndDay(string sweep) {
      var weekAndDay = new int [2];
      var valueIsEmpty = String.IsNullOrEmpty (sweep);
      var secondValueIsX = sweep.Length > 1 && sweep.ToUpper()[1] == 'X'; // This condition should not occur in the data anymore.
      var streetNotMaintainedByDenver = sweep.ToUpper() == "NDM";
      var valueIsN = sweep.ToUpper() == "N";

    if (valueIsEmpty)
      Error = String.Format("{0} Reason: No data Available.", noSchedules);
    else if (streetNotMaintainedByDenver)
      Error = String.Format("{0} Reason: Street not maintained by the City and County of Denver.", noSchedules);
    else if (secondValueIsX)
        Error = String.Format ("{0} Reason: Week {1}, day unknown", noSchedules, sweep[0]);
    else if (valueIsN)
      Error = "Nightly"; // this really isn't an error
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
      var isNS = (AddressQuadrant.Equals("E") && StreetDirection.Equals("E"))
                 || ((AddressQuadrant.Equals("SE") || AddressQuadrant.Equals("NE"))
                    && (StreetDirection.Equals("E") || StreetDirection.Equals("W")));

      // Result is left: E, right: W if
      // Quad: SE, Dir: N or S
      // Quad: SW, Dir: N or S
      // Quad: S, Dir: E
      var isEW = (AddressQuadrant.Equals("S") && StreetDirection.Equals("E"))
                 || ((AddressQuadrant.Equals("SE") || AddressQuadrant.Equals("SW")) 
                     && (StreetDirection.Equals("N") || StreetDirection.Equals("S")));

      // Results is left: W, right: E if
      // Quad: NE, Dir: N or S
      // Quad: NW, Dir: N or S
      // Quad: N, Dir: E
      var isWE = (AddressQuadrant.Equals("N") && StreetDirection.Equals("E"))
                 || ((AddressQuadrant.Equals("NE") || AddressQuadrant.Equals("NW")) 
                     && (StreetDirection.Equals("N") || StreetDirection.Equals("S")));

      // Result is left: S, right: N if
      // Quad: SW, Dir: E or W
      // Quad: NW, Dir: E or W
      // Quad: W, Dir: N
      var isSN = (AddressQuadrant.Equals("W") && StreetDirection.Equals("N"))
                 || ((AddressQuadrant.Equals("SW") || AddressQuadrant.Equals("NW")) 
                     && (StreetDirection.Equals("E") || StreetDirection.Equals("W")));

      if (isNS) {
        LeftSweepDirection = "North";
        RightSweepDirection = "South";
      } else if (isEW) {
        LeftSweepDirection = "East";
        RightSweepDirection = "West";
      } else if (isWE) {
        LeftSweepDirection = "West";
        RightSweepDirection = "East";
      } else if (isSN) {
        LeftSweepDirection = "South";
        RightSweepDirection = "North";
      }
    }
  }
}

