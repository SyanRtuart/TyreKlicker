using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TyreKlicker.WebUI.Services
{
    public class CookieValidator
    {
        private static int _seenCount = 0;

        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            _seenCount++;

            if (_seenCount >= 5)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _seenCount = 0;
            }
        }
    }
}