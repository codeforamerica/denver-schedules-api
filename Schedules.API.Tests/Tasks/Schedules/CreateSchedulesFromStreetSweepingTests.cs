using NUnit.Framework;
using Schedules.API.Models;
using Schedules.API.Tasks.Schedules;
using Simpler;

namespace Schedules.API.Tests.Tasks.Schedules
{
  [TestFixture]
  public class CreateSchedulesFromStreetSweepingTests
  {
    private readonly CreateSchedulesFromStreetSweeping createSchedulesFromStreetSweeping = Task.New<CreateSchedulesFromStreetSweeping>();

    [Test]
    public void ShouldHandleNullSweepingData()
    {
      createSchedulesFromStreetSweeping.In.StreetSweeping = new StreetSweeping () {
        FullName = "hello"
      };

      Assert.DoesNotThrow(createSchedulesFromStreetSweeping.Execute);
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
      createSchedulesFromStreetSweeping.In.StreetSweeping = new StreetSweeping () {
        LeftSweep = leftSweep,
        RightSweep = rightSweep
      };

      createSchedulesFromStreetSweeping.Execute();
      Assert.AreEqual(0, createSchedulesFromStreetSweeping.Out.Schedules.Count);
    }
  }
}

