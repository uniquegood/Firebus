using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus.Manage
{
    public class FirebusManageOptionsBuilder
    {
        public FirebusManageOptions Options { get; } = new FirebusManageOptions();

        public void UseJobPeeker(IFirebusJobPeeker peeker)
        {
            Options.Peeker = peeker;
        }
    }
}
