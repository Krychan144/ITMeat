using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace ITMeat.WEB.Helpers
{
    public static class ControlerExtensionMethods
    {
        public static Guid Actor(this HttpContext controlerContext)
        {
            var userIdentity = (ClaimsIdentity)controlerContext.User.Identity;

            return new Guid(userIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
        }

        public static string UserName(this HttpContext controlerContext)
        {
            var userIdentity = (ClaimsIdentity)controlerContext.User.Identity;

            return userIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}