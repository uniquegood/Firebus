using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus.Manage
{
    public interface IFirebusJobPeeker
    {
        Task<FirebusJob[]> GetAllRegisteredJobsAsync(string queueName);
    }
}
