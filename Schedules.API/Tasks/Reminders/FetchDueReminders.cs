using Simpler;
using Schedules.API.Models;
using System;

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
      Out.DueReminders = new[] { new Reminder { Contact = "denver@example.com" } };
    }
  }
}
