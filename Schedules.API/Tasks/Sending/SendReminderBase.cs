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

    public override void Execute ()
    {
      throw new NotImplementedException();
    }
  }
}

