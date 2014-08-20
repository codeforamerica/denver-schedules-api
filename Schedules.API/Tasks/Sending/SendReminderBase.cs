using Simpler;
using Schedules.API.Models;
using System;

namespace Schedules.API.Tasks.Sending
{
  public class SendReminderBase:InOutTask<SendReminderBase.Input, SendReminderBase.Output>
  {
    public class Input
    {
      public Reminder[] DueReminders { get; set; }
    }

    public class Output
    {
      public int Sent { get; set; }
      public int Errors { get; set; }
    }

    /// <summary>
    /// Inheriting class must implement this, not sure how to enforce it
    /// </summary>
    public override void Execute ()
    {
    }
  }
}

