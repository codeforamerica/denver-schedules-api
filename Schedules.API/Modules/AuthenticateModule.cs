using Nancy;
using Nancy.Authentication.Token;
using Schedules.API.Models;
using Nancy.ModelBinding;
using System;
using Nancy.Metadata.Module;
using Nancy.Swagger;

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

  public class AuthenticateMetadataModule: MetadataModule<SwaggerRouteData>
  {
    public AuthenticateMetadataModule ()
    {
//      Nancy.Swagger.SwaggerModelData model = new SwaggerModelData (System.Type.FilterNameIgnoreCase);
//      Nancy.Swagger.Modules.SwaggerModule model = 

        Describe["PostAuthentication"] = description => description.AsSwagger(with =>
        {
          with.ResourcePath("/authenticate");
          with.Summary("Authenticate a user.");
          with.Notes("Needed to send reminders.");
          with.Model<User>();
          with.BodyParam<User>("A User object", required: true);
          //        with.Response(HttpStatusCode.Unauthorized, "User not authenticated.");
          //        with.Response(HttpStatusCode.OK, "User authenticated.");
        });
    }
  }
}