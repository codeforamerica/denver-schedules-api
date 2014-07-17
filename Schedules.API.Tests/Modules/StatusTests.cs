using System;
using NUnit.Framework;
using Nancy.Testing;
using Schedules.API.Models;

namespace Schedules.API.Tests
{
  [TestFixture ()]
  public class StatusTest
	{
    Browser browser;

    [TestFixtureSetUp]
    public void SetUp()
    {
      browser = new Browser(new CustomBootstrapper());
    }

    [Test ()]
    public void StatusShouldReturnOK()
    {
      var response = browser.Get("/status", with => with.HttpRequest());
      Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
    }

    [Test ()]
    public void StatusShouldReturnJson()
    {
      var response = browser.Get("/status", with => with.HttpRequest());
      Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
    }

    [Test ()]
    public void StatusShouldHaveRequiredFields()
    {
      var response = browser.Get("/status", with => with.HttpRequest());
      var state = response.Context.JsonBody<State>();
      Assert.IsNotNullOrEmpty (state.Status);
      Assert.That (state.Updated, Is.GreaterThan(0));
    }
  }
}

