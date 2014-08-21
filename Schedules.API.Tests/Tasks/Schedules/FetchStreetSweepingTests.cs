using NUnit.Framework;
using Schedules.API.Models;
using System.Collections.Generic;
using Simpler;
using Schedules.API.Tasks.Schedules;

namespace Schedules.API.Tests.Tasks.Schedules
{
  [TestFixture()]
  public class FetchStreetSweepingTests
  {
    FetchStreetSweepings fetchStreetSweeping;
    List<Schedule> streetSweepings;

    [TestFixtureSetUp]
    public void SetUp(){
      fetchStreetSweeping = Task.New<FetchStreetSweepings>();
      var address = new Address { Latitude = 39.7211511226435, Longitude = -104.952312 };
      fetchStreetSweeping.In.Address = address;
      fetchStreetSweeping.Execute();
      streetSweepings = fetchStreetSweeping.Out.StreetSweepings;
    }

    [Test]
    public void StreetSweepingsShouldNotBeNull() {
      Assert.That(streetSweepings, Is.Not.Null);
    }

    [Test]
    public void ShouldHaveSomeStreetSweepings() {
      Assert.That(streetSweepings, Is.Not.Empty);
    }
  }
}

