using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TrandingSystem.Domain.Helper
{
    public static class DeviceHelper
    {
        public static string GetOrCreateDeviceId(HttpContext context)
        {
            var deviceCookie = context.Request.Cookies["DeviceId"];
            if (string.IsNullOrEmpty(deviceCookie))
            {
                deviceCookie = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("DeviceId", deviceCookie, new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
            }
            return deviceCookie;
        }
    }
}
