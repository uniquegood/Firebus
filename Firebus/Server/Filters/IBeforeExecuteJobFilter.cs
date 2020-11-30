using System.Threading.Tasks;

namespace Firebus.Server.Filters
{
    public interface IBeforeExecuteJobFilter
    {
        Task<bool> OnBeforeExecuteJob(JobContext context);
    }
}
