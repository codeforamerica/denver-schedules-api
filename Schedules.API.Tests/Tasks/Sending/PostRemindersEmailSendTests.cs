using NUnit.Framework;
using Nancy.Testing;
using Schedules.API.Models;

namespace Schedules.API.Tests.Tasks.Sending
{
  [TestFixture]
  public class PostReminderEmailSendTests
  {
    Browser browser;

    [TestFixtureSetUp]
    public void SetUp ()
    {
      browser = new Browser (new CustomBootstrapper ());
    }
  }
}
