using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MegaFortnite.Angular.Helpers
{
    public static class UsersHelper
    {
        public static string GetUserId(this ControllerBase controller)
        {
            var claimsIdentity = (ClaimsIdentity)controller.User.Identity;
            var claim = claimsIdentity?.FindFirst("sub");
            return claim?.Value;
        }
    }
}
