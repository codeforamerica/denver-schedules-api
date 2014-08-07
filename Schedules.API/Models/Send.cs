using System;

namespace Schedules.API.Models
{
  public class Send
  {
    public DateTime RemindOn { get; set; }
    public int Sent { get; set; }
    public int Errors { get; set; }
  }
}

