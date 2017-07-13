﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace ITMeat.WEB.Helpers
{
    public static class HubExtensionMethods
    {
        public static Guid Actor(this HubCallerContext hubContext)
        {
            var userIdentity = (ClaimsIdentity)hubContext.User.Identity;

            return new Guid(userIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
        }

        public static string UserName(this HubCallerContext hubContext)
        {
            var userIdentity = (ClaimsIdentity)hubContext.User.Identity;

            return userIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}