using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus
{
    public interface IFirebusJobReceiver
    {
        void BeginReceive();
        void RegisterJobHandler(FirebusJobHandler handler);
    }
}
