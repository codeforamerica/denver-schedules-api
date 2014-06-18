using System;
using System.Collections.Generic;

namespace Schedules.API.Models
{
  public class Schedule
  {
    public Schedule ()
    {
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the category of the schedule.
    /// </summary>
    /// <value>The category.</value>
    public String Category {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>The description.</value>
    public String Description {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the upcoming dates for this schedule item.
    /// TODO: Instead of creating this list, it should follow icalendar conventions
    /// and be calculated client side: https://tools.ietf.org/html/rfc5545
    /// </summary>
    /// <value>The upcoming.</value>
    public List<DateTime> Upcoming {
      get;
      set;
    }

    public class ScheduleComparer : IEqualityComparer<Schedule>
    {
      public bool Equals(Schedule x, Schedule y)
      {
        return (x.Name == y.Name && x.Category == y.Category && x.Description == y.Description);
      }

      public int GetHashCode(Schedule obj)
      {
        return obj.Name.GetHashCode();
      }
    }
  }
}

