using NUnit.Framework;
using System;
using Nancy.Testing;
using Schedules.API.Models;
using System.Collections.Generic;

namespace Schedules.API.Tests.Modules
{
  [TestFixture()]
  public class ReminderTypesTests
  {
    Browser browser;

    [SetUp]
    public void SetUp()
    {
      browser = new Browser(new CustomBootstrapper());
    }
      
    [Test ()]
    public void GetShouldAllowAllOrigins()
    {
      var response = browser.Get("/reminderTypes", with => with.HttpRequest());
      Assert.That(response.Headers["Access-Control-Allow-Origin"], Is.EqualTo("*"));
    }

    [Test ()]
    public void GetShouldReturnOK()
    {
      var response = browser.Get("/reminderTypes", with => with.HttpRequest());
      Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
    }

    [Test ()]
    public void GetShouldReturnJson()
    {
      var response = browser.Get("/reminderTypes", with => with.HttpRequest());
      Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
    }

    [Test()]
    public void GetShouldReturnAllTypes()
    {// If adding a new reminder type breaks the API, we should have a test that reminds us of that
      var response = browser.Get("/reminderTypes", with => with.HttpRequest());
      var reminderTypes = response.Context.JsonBody<List<ReminderType>>();
      Assert.That (reminderTypes.Count, Is.EqualTo(3));
    }
  }
}

