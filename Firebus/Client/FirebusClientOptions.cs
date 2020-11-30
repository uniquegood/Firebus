using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Client.Filters;

namespace Firebus.Client
{
    public class FirebusClientOptions
    {
        public IFirebusJobRegister JobRegister { get; set; }

        internal ISet<IBeforeRegisterJobFilter> BeforeRegisterJobFilters { get; set; } = new HashSet<IBeforeRegisterJobFilter>();
        internal ISet<IAfterRegisterJobFilter> AfterRegisterJobFilters { get; set; } = new HashSet<IAfterRegisterJobFilter>();
    }
}
