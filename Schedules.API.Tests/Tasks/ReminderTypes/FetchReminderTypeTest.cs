using NUnit.Framework;
using Simpler;
using Schedules.API.Tasks;
using Schedules.API.Tasks.ReminderTypes;

namespace Schedules.API.Tests.Tasks.ReminderTypes
{
  [TestFixture]
  public class FetchReminderTypeTest
  {
    FetchReminderType fetchReminderType;

    [TestFixtureSetUp]
    public void SetUp(){
      fetchReminderType = Task.New<FetchReminderType>();
    }

    [Test]
    public void ShouldFetchSMSReminderType() {
      CheckForReminderType("sms");
    }

    [Test]
    public void ShouldFetchEmailReminderType() {
      CheckForReminderType("email");
    }

    [Test]
    public void ShouldFetchYoReminderType() {
      CheckForReminderType("yo");
    }

    private void CheckForReminderType (string type)
    {
      fetchReminderType.In.ReminderTypeName = type;
      fetchReminderType.Execute();
      Assert.That(fetchReminderType.Out.ReminderType.Name, Is.EqualTo(type));
    }
  }
}

