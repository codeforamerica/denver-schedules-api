using System;
using System.Collections.Generic;
using Schedules.API.Extensions;
using Schedules.API.Repositories;

namespace Schedules.API.Models
{
  /// <summary>
  /// Segment of a street sweeping route
  /// </summary>
  public class StreetSweeping
  {
    private static string leftString = "Left Side";
    private static string rightString = "Right Side";

    public StreetSweeping ()
    {
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

    public List<Schedule> CreateSchedules() {
      var schedules = new List<Schedule> ();
      if (LeftSweep.Equals (RightSweep)) {
        schedules.Add (new Schedule () {
          Name = Name,
          Category = SchedulesRepository.Categories.StreetSweeping.ToString (),
          Description = leftString + " & " + rightString,
          Upcoming = new List<DateTime> ()
        });
      } else {
        schedules.Add( new Schedule () { 
          Name = Name, 
          Category = SchedulesRepository.Categories.StreetSweeping.ToString (),
          Description = leftString,
          Upcoming = new List<DateTime> ()
        });

        schedules.Add (new Schedule () { 
          Name = Name, 
          Category = SchedulesRepository.Categories.StreetSweeping.ToString (),
          Description = rightString,
          Upcoming = new List<DateTime> ()
        });
      }
      return schedules;
    }
  }
}

