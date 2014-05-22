using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Newtonsoft.Json;
using Schedules.API;

namespace Schedules.API
{
    public class CustomBootstrapper: DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof (JsonSerializer), typeof (CustomJsonSerializer));
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
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
    }
}
