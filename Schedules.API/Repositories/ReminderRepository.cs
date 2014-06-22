using System;
using System.Collections.Generic;
using Schedules.API.Models;

namespace Schedules.API.Repositories
{
  public static class ReminderRepository
  {
    public static IEnumerable<Reminder> Get()
    {
      return new[] { new Reminder { Message = "Hi", Cell = "+15555555555" } };
    }
  }
}

