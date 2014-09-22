using System;
using Simpler;
using Schedules.API.Models;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace Schedules.API.Tasks.Reminders
{
  public class FetchRemindersForContact : InOutTask<FetchRemindersForContact.Input, FetchRemindersForContact.Output>
  {
    public class Input
    {
      public String Contact { get; set; }
    }

    public class Output
    {
      public List<string> RemindersForContact { get; set; }
    }

    public override void Execute()
    {
      Out.RemindersForContact = new List<string> ();
      try
      {
        using(var connection = Db.Connect()) {
          Out.RemindersForContact = connection.Query<string>(sql, new {contact =In.Contact}).ToList();
        }
      }
      catch(Exception ex) {
        Console.WriteLine(ex);
        throw;
      }
    }

    const string sql = @"
      select
        distinct message
      from
        reminders
      where
        contact = @contact;
    ";
  }
}

