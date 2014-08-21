using Simpler;
using Schedules.API.Models;
using System;
using Dapper;
using System.Linq;
using Schedules.API.Tasks.ReminderTypes;

namespace Schedules.API.Tasks.Reminders
{
  public class FetchDueReminders : InOutTask<FetchDueReminders.Input, FetchDueReminders.Output>
  {
    public class Input
    {
      public String ReminderTypeName { get; set; }
      public DateTime RemindOn { get; set; }
    }

    public class Output
    {
      public Reminder[] DueReminders { get; set; }
    }

    public FetchReminderType FetchReminderType { get; set; }

    public override void Execute ()
    {
      FetchReminderType.In.ReminderTypeName = In.ReminderTypeName;
      FetchReminderType.Execute();

      using (var connection = Db.Connect()) {
        Out.DueReminders = connection.Query<Reminder, ReminderType, Reminder>(
          sql,
          (reminder, reminderType) => {
            reminder.ReminderType = reminderType;
            return reminder;
          },
          new {
            ReminderTypeId = FetchReminderType.Out.ReminderType.Id,
            In.RemindOn
          }
        ).ToArray();
      }
    }

    const string sql = @"
      select
        r.id as Id,
        r.reminder_type_id as ReminderTypeId,
        r.contact as Contact,
        r.message as Message,
        r.remind_on as RemindOn,
        r.verified as Verified,
        r.address as Address,
        r.created_at as CreatedAt,
        t.*
      from
        reminders r
        left join
        reminder_types t
          on t.id = r.reminder_type_id
      where
        reminder_type_id = @ReminderTypeId
        and
        date_trunc('day', remind_on) = date_trunc('day', @RemindOn);
    ";
  }
}
