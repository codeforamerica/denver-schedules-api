using System;
using Simpler;
using Schedules.API.Models;
using Dapper;
using System.Linq;
using Schedules.API.Tasks.ReminderTypes;

namespace Schedules.API.Tasks.Reminders
{
  public class CreateReminder: InOutTask<CreateReminder.Input, CreateReminder.Output>
  {
    public FetchReminderType FetchReminderType { get; set; }

    public class Input
    {
      public Reminder Reminder { get; set; }
      public String ReminderTypeName { get; set; }
    }

    public class Output
    {
      public Reminder Reminder { get; set; }
    }
   
    public override void Execute()
    {
      FetchReminderType.In.ReminderTypeName = In.ReminderTypeName;
      FetchReminderType.Execute();
      In.Reminder.ReminderType = FetchReminderType.Out.ReminderType;

      Out.Reminder = new Reminder ();

      using (var connection = Db.Connect ()) {
        try{
          Out.Reminder = connection.Query<Reminder, ReminderType, Reminder>(
            sql,
            (reminder, reminderType) => {reminder.ReminderType = reminderType; return reminder;},
            In.Reminder).SingleOrDefault();
        }
        catch(Exception ex){
          Console.WriteLine (ex);
          throw;
        }
      }
    }

    const string sql = @"
      with insertReminder as (
        insert into Reminders(contact, message, remind_on, verified, address, reminder_type_id)
        values(@Contact, @Message, @RemindOn, @Verified, @Address, @ReminderTypeId)
        returning *
      )
      select
        r.id,
        r.contact,
        r.message,
        r.remind_on as RemindOn,
        r.verified,
        r.address,
        r.reminder_type_id,
        t.*
      from insertReminder r
      left join reminder_types t
        on t.id = r.reminder_type_id
    ";
  }
}

