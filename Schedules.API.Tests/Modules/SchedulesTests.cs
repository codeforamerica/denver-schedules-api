using NUnit.Framework;
using System.Net;
using System;
using Nancy.Testing;
using Nancy;

namespace Schedules.API.Tests
{
	[TestFixture ()]
	public class SchedulesTest
	{
		Browser browser;

		[SetUp]
		public void SetUp()
		{
			browser = new Browser(with =>
				{
					with.Module<SchedulesModule>();
					with.EnableAutoRegistration();
				});
		}
			
		[Test ()]
		public void ListIndexShouldReturnOk()
		{
			var response = browser.Get("/schedules", with => with.HttpRequest());
			Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
		}

		[Test ()]
		public void ListIndexShouldReturnJson()
		{
			var response = browser.Get("/schedules", with => with.HttpRequest());
			Assert.AreEqual ("application/json; charset=utf-8", response.ContentType);
		}
	}
}

