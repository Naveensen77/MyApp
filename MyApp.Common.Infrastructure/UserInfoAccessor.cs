using Microsoft.AspNetCore.Http;
using MyApp.Common.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Infrastructure
{
    public sealed class UserInfoAccessor(IHttpContextAccessor httpContextAccessor)
    : IUserInfoAccessor
    {
        public string GetUserName()
        {
            return httpContextAccessor.HttpContext?.User?.Identity?.Name
                   ?? "system";
        }
        public string GetRemoteIp()
        {
            return httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()
                   ?? "unknown";
        }
    }
}
