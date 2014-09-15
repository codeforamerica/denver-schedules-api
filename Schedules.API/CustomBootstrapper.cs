using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Newtonsoft.Json;
using Schedules.API;
using Nancy.Authentication.Token;
using Nancy.Authentication.Token.Storage;
using Nancy.Conventions;

namespace Schedules.API
{
  public class CustomBootstrapper: DefaultNancyBootstrapper
  {
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);
      container.Register(typeof(JsonSerializer), typeof(CustomJsonSerializer));

      container.Register<ITokenizer>(
        new Tokenizer(cfg => cfg.WithKeyCache(new InMemoryTokenKeyStore()))
      );
    }

    protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
    {
      TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration (container.Resolve<ITokenizer>()));

      pipelines.BeforeRequest.AddItemToStartOfPipeline(c => {
        if (c.Request.Method != "OPTIONS") return null;

        return new Response { StatusCode = HttpStatusCode.NoContent }
          .WithHeader("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE")
          .WithHeader("Access-Control-Allow-Headers", "Content-Type");
      });

      pipelines.AfterRequest.AddItemToEndOfPipeline(c =>
          c.Response.WithHeader("Access-Control-Allow-Origin", "*")
      );
    }

    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      pipelines.OnError.AddItemToEndOfPipeline((context, exception) => {
        System.Console.Error.WriteLine(exception.ToString());
        return null;
      });
    }

    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
      nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("docs", "swagger-ui"));

      base.ConfigureConventions(nancyConventions);

    }
  }
}
