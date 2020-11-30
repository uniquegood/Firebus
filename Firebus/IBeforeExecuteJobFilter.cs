using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus
{
    public interface IBeforeExecuteJobFilter
    {
        Task<bool> OnBeforeExecuteJob(JobExecutionContext context);
    }
}
