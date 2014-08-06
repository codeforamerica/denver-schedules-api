using Simpler;
using Schedules.API.Models;

namespace Schedules.API.Tasks.Reminders
{
  public class FetchDueReminders : OutTask<FetchDueReminders.Output>
  {
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
