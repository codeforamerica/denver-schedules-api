using System;
using Schedules.API.Models;
using System.Collections.Generic;
using Nancy;
using Dapper;
using System.Linq;

namespace Schedules.API.Repositories
{
  public class SchedulesRepository:RepositoryBase
  {
    public enum Categories
    {
      Holidays
    }

    public SchedulesRepository ()
    {
    }

    public List<Schedule> Get (SchedulesRepository.Categories category, Nancy.DynamicDictionary dictionary)
    {
      var schedules = new List<Schedule> ();
      switch (category) {
        case Categories.Holidays:
        default:
          schedules = GetHolidays ();
          break;
      }
      return schedules;
    }

    public List<Schedule> GetHolidays() {
      return new List<Schedule> (){
        new Schedule(){
          Name = "New Years Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 1, 1)}
        },
        new Schedule(){
          Name = "Martin Luther King Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 1, 20)}
        },
        new Schedule(){
          Name = "President's Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 2, 27)}
        },
        new Schedule(){
          Name = "Cesar Chavez Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 3, 31)}
        },
        new Schedule(){
          Name = "Memorial Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 5, 26)}
        },
        new Schedule(){
          Name = "Independence Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 7, 4)}
        },
        new Schedule(){
          Name = "Labor Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 11, 1)}
        },
        new Schedule(){
          Name = "Thanksgiving Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 11, 27)}
        },
        new Schedule(){
          Name = "Christmas Day",
          Category = Categories.Holidays.ToString(),
          Upcoming = new List<DateTime>(){ new DateTime(2014, 12, 25)}
        }
      };
    }
  }
}

