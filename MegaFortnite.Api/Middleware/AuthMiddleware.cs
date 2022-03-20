using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MegaFortnite.Api.MiddleWare
{
    public static class AuthMiddleware
    {
        internal static async Task AuthQueryStringToHeader(HttpContext context, Func<Task> next)
        {
            var qs = context.Request.QueryString;

            if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]) && qs.HasValue)
            {
                if (qs.Value != null)
                {
                    var token = (from pair in qs.Value.TrimStart('?').Split('&')
                        where pair.StartsWith("token=")
                        select pair.Substring(6)).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + token);
                    }
                }
            }

            await next?.Invoke();
        }
    }
}
