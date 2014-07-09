using System;

namespace Schedules.API.Models
{
  public class ReminderType
  {
    public ReminderType ()
    {
    }

    public int Id {
      get;
      set;
    }

    public string Name {
      get;
      set;
    }

    public string Description {
      get;
      set;
    }
  }
}

