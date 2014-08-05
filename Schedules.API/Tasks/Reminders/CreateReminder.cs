using System;
using Simpler;
using Schedules.API.Models;
using Dapper;

namespace Schedules.API.Tasks
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

      Out.Reminder = In.Reminder;

      using (var connection = Db.Connect ()) {
        try{
          Out.Reminder.Id = connection.Execute(sql, In.Reminder);
        }
        catch(Exception ex){
          Console.WriteLine (ex);
        }
      }
    }

    const string sql = @"
      insert into Reminders(contact, message, verified, address, reminder_type_id) 
      values(@Contact, @Message, @Verified, @Address, @ReminderTypeId)
    ";
  }
}

