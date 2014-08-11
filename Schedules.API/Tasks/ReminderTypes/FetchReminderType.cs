using System;
using Simpler;
using Schedules.API.Models;
using Dapper;
using System.Linq;

namespace Schedules.API.Tasks
{
  public class FetchReminderType: InOutTask<FetchReminderType.Input, FetchReminderType.Output>
  {
    public class Input
    {
      public string ReminderTypeName { get; set; }
    }

    public class Output
    {
      public ReminderType ReminderType { get; set; }
    }

    public override void Execute(){
      Out.ReminderType = new ReminderType ();
      try
      {
        using(var connection = Db.Connect()) {
          Out.ReminderType = connection.Query<ReminderType>(sql, new {name =In.ReminderTypeName}).Single();
        }
      }
      catch(Exception ex) {
        Console.WriteLine(ex);
        throw;
      }
    }

    const string sql = @"
      select
        *
      from
        reminder_types
      where
        name = @name
    ";
  }
}

