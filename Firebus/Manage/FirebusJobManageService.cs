using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebus.Client;

namespace Firebus.Manage
{
    public class FirebusJobManageService
    {
        private readonly IFirebusJobPeeker _peeker;

        public FirebusJobManageService(FirebusManageOptions options)
        {
            _peeker = options.Peeker;
        }

        public async Task<FirebusJob[]> GetAllRegisteredJobsAsync(string queueName)
        {
            return await _peeker.GetAllRegisteredJobsAsync(queueName);
        }

        public async Task CancelRegisteredJobAsync(string queueName, FirebusJob job)
        {
            await _peeker.CancelRegisteredJobAsync(queueName, job);
        }
    }
}
