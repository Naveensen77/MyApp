using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Core.Data
{
    public interface IUserInfoAccessor
    {
        string GetUserName();
        string GetRemoteIp();
    }
}
