using Nancy;
using System;

namespace Schedules.API
{
    public static class CustomExtensions
    {
        public static Response AllowCorsFor(this IResponseFormatter responseFormatter, Request request)
        {
            var requested = request.Headers["Access-Control-Request-Headers"];
            var allowHeaders = String.Join(", ", requested);
            return new Response()
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "GET, POST, DELETE, PUT, PATCH, OPTIONS")
                .WithHeader("Access-Control-Allow-Headers", allowHeaders);
        }

        public static Response AddCorsHeader(this Response response)
        {
            return response.WithHeader("Access-Control-Allow-Origin", "*");
        }
    }
}
