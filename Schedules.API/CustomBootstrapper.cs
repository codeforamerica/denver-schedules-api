using System;
using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace Schedules.API
{
    public class CustomBootstrapper: DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof (JsonSerializer), typeof (CustomJsonSerializer));
        }
    }
}
