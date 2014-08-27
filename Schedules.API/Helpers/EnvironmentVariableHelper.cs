using System;

namespace Schedules.API.Helpers
{
  public static class EnvironmentVariableHelper
  {
    /// <summary>
    /// Gets an environment variable. 
    /// Throws<exception cref="Exception"></exception>if it doesn't exist.
    /// </summary>
    /// <returns>The environment variable.</returns>
    /// <param name="key">Key.</param>
    public static string GetEnvironmentVariable(string key)
    {
      var value = Environment.GetEnvironmentVariable(key);
      if (String.IsNullOrEmpty(value)) throw new Exception (key + " environment variable not found.");
      return value;
    }
  }
}

