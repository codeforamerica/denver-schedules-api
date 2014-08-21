using System;
using Nancy.Testing;
using Schedules.API.Models;

namespace Schedules.API.Tests
{
  public static class AuthenticationHelper
  {
    public static string Authenticate(Browser browser)
    {
      var username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
      var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

      var response = browser.Post("/authenticate", with => {
        with.HttpRequest();
        with.Header("User-Agent", "test");
        with.JsonBody<User>(new User { Username = username, Password = password });
      });
      return response.Context.JsonBody<Authenticate>().Token;
    }
  }
}

