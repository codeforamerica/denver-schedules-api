using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Schedules.API
{
	public class CustomJsonSerializer : JsonSerializer
	{
		public CustomJsonSerializer ()
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}

