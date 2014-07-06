using System;
using Schedules.API.Repositories;
using System.Configuration;
using Schedules.API.Models;
using System.Collections.Generic;

namespace Schedules.API.Helpers
{
  public class Results
  {
    public Results() {
      Sent = 0;
      Errors = new List<String>();
    }
    public int Sent {
      get;
      set;
    }

    public List<String> Errors {
      get;
      set;
    }
  }
    
  public static class Notifier
  {
    public static Results TextDoIt(){
      return DoIt (ReminderRepository.Get ());
    }

    public static Results DoIt(IEnumerable<Reminder> reminders) {
      var results = new Results ();
      foreach (var reminder in reminders) {
        var error = reminder.Send();
        if (string.IsNullOrEmpty (error))
          results.Sent++;
        else {
          Console.WriteLine (error);
          results.Errors.Add (error);
        }
      }
      return results;
    }
  }
}

