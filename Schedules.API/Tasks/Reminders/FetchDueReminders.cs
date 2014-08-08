using Simpler;
using Schedules.API.Models;
using System;
using Dapper;
using System.Linq;

namespace Schedules.API.Tasks.Reminders
{
  public class FetchDueReminders : InOutTask<FetchDueReminders.Input, FetchDueReminders.Output>
  {
    public class Input
    {
      public DateTime RemindOn { get; set; }
    }

    public class Output
    {
      public Reminder[] DueReminders { get; set; }
    }

    public override void Execute ()
    {
      using (var connection = Db.Connect()) {
        Out.DueReminders = connection.Query<Reminder>(sql, In).ToArray();
      }
    }

    const string sql = @"
      select
        id as Id,
        reminder_type_id as ReminderTypeId,
        contact as Contact,
        message as Message,
        remind_on as RemindOn,
        verified as Verified,
        address as Address,
        created_at as CreatedAt
      from
        reminders
      where
        date_trunc('day', remind_on) = date_trunc('day', @RemindOn);
    ";
  }
}
