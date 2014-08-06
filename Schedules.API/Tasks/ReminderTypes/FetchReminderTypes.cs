using System;
using Simpler;
using Schedules.API.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Schedules.API.Tasks.ReminderTypes
{
  public class FetchReminderTypes : OutTask<FetchReminderTypes.Output>
  {
    public class Output
    {
      public List<ReminderType> ReminderTypes { get; set; }
    }

    public override void Execute()
    {
      using (var connection = Db.Connect()) {
        try{
          Out.ReminderTypes = connection.Query<ReminderType> (sql).ToList();
        }
        catch(Exception ex){
          Console.WriteLine (ex);
        }
      }
    }

    const string sql = @"
      select
        id,
        name,
        description
      from
        reminder_types;
    ";
  }
}

