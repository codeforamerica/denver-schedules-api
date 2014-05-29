using System;

namespace Schedules.API.Extensions
{

  public static class DateTimeUnixTimeStamp
  {
    private static DateTime epoch = new DateTime(1970, 1, 1, 0,0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Converts to a UNIX Timestamp
    /// </summary>
    /// <returns>The unix timestamp.</returns>
    /// <param name="d">D.</param>
    public static int ToUnixTimestamp(this DateTime d)
    {
      TimeSpan t = (d - epoch);
      return (int) t.TotalSeconds;
    }

    /// <summary>
    /// Converts a UNIX Tmestamp to a date
    /// </summary>
    /// <returns>The unix timestamp.</returns>
    /// <param name="i">The index.</param>
    public static DateTime ToDateTime(this int i)
    {
      return epoch.AddSeconds(i);
    }
  }
}

