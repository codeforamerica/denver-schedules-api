using System;
using Nancy;
using Schedules.API.Extensions;

public class StatusModule : NancyModule
{
	public StatusModule ()
	{
		// Engine Light App Monitoring - http://engine-light.codeforamerica.org/
		Get ["/status"] = _ => {

			var status = new {
				Status = "ok",
        Updated = DateTime.Now.ToUnixTimestamp(),
				Dependenceies = new string[0],
				Resources = new {}
			};

			return Response.AsJson (status);
		};
	}
}

