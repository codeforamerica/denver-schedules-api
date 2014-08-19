using System;
using NUnit.Framework;
using Centroid;

namespace Schedules.API.Tests
{
  [TestFixture, Category("System")]
  public class SystemTests
  {
    [Test]
    public void CheckThatEnvironmentVariablesExist()
    {
      dynamic config = Config.FromFile("config.json");
      foreach (var variable in config.variables) {
        var value = Environment.GetEnvironmentVariable(variable);
        Assert.That(!String.IsNullOrEmpty(value), String.Format("{0} does not have a value.", variable));
      }
    }
  }
}
