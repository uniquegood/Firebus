using System.Threading.Tasks;

namespace Firebus.Server.Filters
{
    public interface IAfterExecuteJobFilter
    {
        Task OnAfterExecuteJob(JobContext context);
    }
}
