using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.IO;
using Nancy.IO;

namespace Schedules.API
{
	public class CustomJsonSerializer : JsonSerializer
	{
		public CustomJsonSerializer ()
		{
			// Map C# naming conventions to javascript
			ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Standardize datetime objects
			Converters.Add(new IsoDateTimeConverter
			{
				DateTimeStyles = DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal
			});
		}
	}
}

