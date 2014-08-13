using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Newtonsoft.Json;
using Schedules.API;
using Nancy.Authentication.Token;
using Nancy.Authentication.Token.Storage;

namespace Schedules.API
{
  public class CustomBootstrapper: DefaultNancyBootstrapper
  {
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);
      container.Register(typeof(JsonSerializer), typeof(CustomJsonSerializer));

      // TODO - Add something custom to the Tokenizer (commented example below shows passing cfg to constructor).
      container.Register<ITokenizer>(new Tokenizer(
        cfg => cfg.WithKeyCache(new InMemoryTokenKeyStore())
//        cfg => cfg.AdditionalItems(
//          ctx => ctx.Request.Headers["X-Custom-Header"].FirstOrDefault(),
//          ctx => ctx.Request.Query.extraValue)
      ));
    }

    protected override void RequestStartup (TinyIoCContainer container, IPipelines pipelines, NancyContext context)
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

    protected override void ApplicationStartup (TinyIoCContainer container, IPipelines pipelines)
    {
      pipelines.OnError.AddItemToEndOfPipeline((context, exception) => {
        System.Console.Error.WriteLine(exception.ToString());
        return null;
      });
    }
  }
}
