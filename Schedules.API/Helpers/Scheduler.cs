using System;
using FluentDateTime;
using System.Collections.Generic;

namespace Schedules.API.Helpers
{
  /// <summary>
  /// TODO: Test!!!!!!!
  /// </summary>
  public static class Scheduler
  {
    /// <summary>
    /// Calculates dates for rest of year.
    /// TODO: Accomodate holidays
    /// </summary>
    /// <returns>The dates for rest of year.</returns>
    /// <param name="week">Week.</param>
    /// <param name="day">Day.</param>
    /// <param name="months">Months to calculate dates for.</param>
    public static List<DateTime> CalculateDatesForRestOfYear (int week, int day, bool [] months) {
      var dates = new List<DateTime> ();
      var today = DateTime.UtcNow;
      if (week != 0 && day != 0) {
        for (int i = today.Month; i <= months.Length && months [i - 1]; i++) {
          var date = UTCDateFromYearMonthWeekDay (today.Year, i, week, day);
          if (date >= today)
            dates.Add (date);
        }
      }

      return dates;
    }

    private static DateTime UTCDateFromYearMonthWeekDay(int year, int month, int week, int day){
      var date = new DateTime (year, month, 1).ToUniversalTime ();
      var dayOfWeekWanted = (DayOfWeek)Enum.ToObject (typeof(DayOfWeek), day);
      var beginningDay = (int) date.DayOfWeek;
      if (beginningDay < day)
        date = date.Next (dayOfWeekWanted);
      else if (beginningDay > day) {
        var previous = date.Previous (dayOfWeekWanted);
        if (previous.Month == month)
          date = previous;
        else
          date = date.Next (dayOfWeekWanted);
      }
      var daysToAdd = (week - 1) * 7;

      return date.AddDays (daysToAdd);
    }

    private static int GetDateForWeekDay(DayOfWeek DesiredDay, int Occurrence, int Month, int Year)
    {
      DateTime dtSat = new DateTime(Year, Month, 1);
      int j = 0;
      if (Convert.ToInt32(DesiredDay) - Convert.ToInt32(dtSat.DayOfWeek) >= 0)
        j = Convert.ToInt32(DesiredDay) - Convert.ToInt32(dtSat.DayOfWeek) + 1;
      else
        j = (7 - Convert.ToInt32(dtSat.DayOfWeek)) + (Convert.ToInt32(DesiredDay) + 1);

      return j + (Occurrence - 1) * 7;
    }
  }
}

