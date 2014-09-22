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
    public const string SmsContact = "5555555555";
    public const string EmailContact = "example@email.com";

    protected void SetUpSms(string message="this is a test", string contact=SmsContact)
    {
      SetUpContact("sms", message, contact);
    }

    protected void SetUpEmail(string message="this is a test", string contact=EmailContact)
    {
      SetUpContact("email", message, contact);
    }

    protected void TearDownEmail()
    {
      TearDown(EmailContact);
    }

    protected void TearDownSMS()
    {
      TearDown(SmsContact);
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

