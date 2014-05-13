using NUnit.Framework;
using System.Net;
using System;
using Nancy.Testing;
using Nancy;

namespace Schedules.API.Tests
{
	[TestFixture ()]
	public class Test
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
			Nancy.Testing.BrowserResponse result = browser.Get("/schedules", with => with.HttpRequest());
			Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
		}
	}
}

