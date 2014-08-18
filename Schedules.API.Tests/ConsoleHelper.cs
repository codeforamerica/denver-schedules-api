using System;
using System.IO;

namespace Schedules.API.Tests
{
  public static class ConsoleHelper
  {
    public static string CaptureOutput (Action execute)
    {
      using (var sw = new StringWriter ()) {
        var previousOut = Console.Out;
        Console.SetOut(sw);
        execute();
        Console.SetOut(previousOut);
        return sw.ToString();
      }
    }
  }
}

