using NUnit.Framework;
using Schedules.API.Models;

namespace Schedules.API.Tests.Models
{
  [TestFixture]
  public class StreetSweepingTest
  {
    [Test]
    public void ShouldHandleNullSweepingData()
    {
      var ss = new StreetSweeping () {
        FullName = "hello"
      };

      Assert.DoesNotThrow(() => ss.CreateSchedules());
    }

    [Test]
    public void ShouldNotCreateSchedulesForNDM() {
      ShouldNotCreateSchedules("NDM", "ndm");
    }

    [Test]
    public void ShouldNotCreateSchedulesForBlanks() {
      ShouldNotCreateSchedules(string.Empty, string.Empty);
    }

    [Test]
    public void ShouldNotCreateSchedulesForX() {
      ShouldNotCreateSchedules("2X", "2x");
    }

    private void ShouldNotCreateSchedules(string leftSweep, string rightSweep)
    {
      var ss = new StreetSweeping () {
        LeftSweep = leftSweep,
        RightSweep = rightSweep
      };

      Assert.AreEqual(0, ss.CreateSchedules().Count);
    }
  }
}

