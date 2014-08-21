using System;
using NUnit.Framework;
using Schedules.API.Tasks.Schedules;
using Schedules.API.Models;
using System.Collections.Generic;
using Simpler;

namespace Schedules.API.Tests.Tasks.Schedules
{
  [TestFixture()]
  public class FetchHolidaysTests
  {
    FetchHolidays fetchHolidays;
    List<Schedule> holidays;

    [TestFixtureSetUp]
    public void SetUp(){
      fetchHolidays = Task.New<FetchHolidays>();
      fetchHolidays.Execute();
      holidays = fetchHolidays.Out.Holidays;
    }

    [Test]
    public void HolidaysShouldNotBeNull() {
      Assert.That(holidays, Is.Not.Null);
    }

    [Test]
    public void ShouldHaveSomeHolidays() {
      Assert.That(holidays, Is.Not.Empty);
    }
  }
}

