using System;
using NUnit.Framework;
using Simpler;
using Schedules.API.Models;
using Schedules.API.Tasks.Schedules;

namespace Schedules.API.Tests
{
  [TestFixture]
  public class FetchSchedulesTests
  {
    private FetchSchedules fetchSchedules;

    [TestFixtureSetUp]
    public void SetUp(){
      fetchSchedules = Task.New<FetchSchedules>();
      var address = new Address { Latitude = 39.7659901751922, Longitude = -104.95474457244 };
      fetchSchedules.In.Address = address;
    }

    [Test]
    public void SchedulesShouldNotBeNull(){
      fetchSchedules.Execute();
      Assert.That(fetchSchedules.Out.Schedules, Is.Not.Null);
    }

    [Test]
    public void ShouldFetchHolidays() {
      fetchSchedules.In.Category = Categories.Holidays;
      fetchSchedules.Execute();
      Assert.That(fetchSchedules.Out.Schedules, Is.Not.Empty);
    }

    [Test]
    public void ShouldFetchStreetSweepings(){
      fetchSchedules.In.Category = Categories.StreetSweeping;
      fetchSchedules.Execute();
      Assert.That(fetchSchedules.Out.Schedules, Is.Not.Empty);
    }
  }
}

