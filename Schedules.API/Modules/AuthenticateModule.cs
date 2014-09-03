using Nancy;
using Nancy.Authentication.Token;
using Schedules.API.Models;
using Nancy.ModelBinding;
using System;

namespace Schedules.API.Modules
{
  public class AuthenticateModule : NancyModule
  {
    private const string adminKey = "ADMIN_USERNAME";
    private const string passwordKey = "ADMIN_PASSWORD";

    public AuthenticateModule(ITokenizer tokenizer)
    {
      Post["/authenticate"] = x => {
        var user = this.Bind<User>();

        var username = Environment.GetEnvironmentVariable(adminKey);
        var password = Environment.GetEnvironmentVariable(passwordKey);

        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) return HttpStatusCode.Unauthorized;
        if (user.Username != username || user.Password != password) return HttpStatusCode.Unauthorized;

        var identity = new UserIdentity {
          UserName = user.Username,
          Claims = new[] { "admin" }
        };
        var token = tokenizer.Tokenize(identity, Context);
        return Response.AsJson(new Authenticate { Token = token });
      };
    }
  }
}