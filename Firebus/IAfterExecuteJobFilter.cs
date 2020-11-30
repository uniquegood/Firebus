using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus
{
    public interface IAfterExecuteJobFilter
    {
        Task OnAfterExecuteJob(JobExecutionContext context);
    }
}
