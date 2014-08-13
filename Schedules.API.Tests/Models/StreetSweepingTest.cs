using NUnit.Framework;
using Schedules.API.Models;

namespace Schedules.API.Tests
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
  }
}

