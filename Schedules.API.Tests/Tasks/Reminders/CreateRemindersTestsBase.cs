using System;
using Schedules.API.Tasks.Reminders;
using Simpler;
using Schedules.API.Models;
using Schedules.API.Tasks;
using Dapper;

namespace Schedules.API.Tests.Tasks.Reminders
{

  public class CreateRemindersTestsBase
  {
    protected CreateReminder CreateReminder;
    protected readonly DateTime JulyOne = DateTime.Parse("2014-07-01");
    const string smsContact = "5555555555";
    const string emailContact = "example@email.com";

    protected void SetUpSms(string message="this is a test", string contact=smsContact)
    {
      SetUpContact("sms", message, contact);
    }

    protected void SetUpEmail(string message="this is a test", string contact=emailContact)
    {
      SetUpContact("email", message, contact);
    }

    protected void TearDownEmail()
    {
      TearDown(emailContact);
    }

    protected void TearDownSMS()
    {
      TearDown(smsContact);
    }

    private void TearDown(string contact)
    {
      var sql = string.Format("delete from reminders where contact = '{0}'", contact);
      using (var db = Db.Connect()) {
        db.Execute(sql);
      }
    }

    private void SetUpContact(string type, string message, string contact)
    {
      CreateReminder = Task.New<CreateReminder>();
      CreateReminder.In.ReminderTypeName = type;
      CreateReminder.In.Reminder = new Reminder {
        Contact = contact,
        Message = message,
        RemindOn = JulyOne,
        Verified = false,
        Address = "1234 address ave",
        CreatedAt = DateTime.Now
      };
      CreateReminder.Execute();
    }
  }
}

