using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User is null)
            {
                return string.Empty;
            }
            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public static string GetUserName(this HttpContext httpContext)
        {
            if (httpContext.User is null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}
