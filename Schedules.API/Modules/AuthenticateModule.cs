using Nancy;
using Nancy.Security;
using Nancy.Authentication.Token;
using Schedules.API.Models;
using Nancy.ModelBinding;
using System;

namespace Schedules.API.Modules
{
  public class AuthenticateModule : NancyModule
  {
    public AuthenticateModule(ITokenizer tokenizer)
    {
      Post["/authenticate"] = x =>
      {
        var user = this.Bind<User>();

        // TODO - Look below.
        IUserIdentity identity;
        if (user.Username == "admin" && user.Password == "admin") {
          identity =  new UserIdentity { UserName = "admin", Claims = new[] { "admin" } };
        } else {
          return HttpStatusCode.Unauthorized;
        }

        var token = tokenizer.Tokenize(identity, Context);
        return Response.AsJson(new Authenticate { Token = token });
      };
    }
  }
}