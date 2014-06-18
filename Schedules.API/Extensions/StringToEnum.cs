using System;

namespace Schedules.API.Extensions
{
  public static class StringToEnum
  {
    /// <summary>
    /// Turn string into the specified enum, case insensitive
    /// If invalid, return default
    /// </summary>
    /// <returns>The enum.</returns>
    /// <param name="s">S.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T ToEnum<T>(this string s) where T : struct
    { 
      T result;
      Enum.TryParse<T> (s, true, out result);
      return result;
    }
  }
}

