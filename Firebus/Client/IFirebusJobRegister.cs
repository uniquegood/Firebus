using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus.Client
{
    public interface IFirebusJobRegister
    {
        Task RegisterJobAsync(FirebusJob job, DateTime? scheduledTimeUtc);
    }
}
