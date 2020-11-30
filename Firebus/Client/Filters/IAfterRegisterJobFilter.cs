using System.Threading.Tasks;
using Firebus.Server;

namespace Firebus.Client.Filters
{
    public interface IAfterRegisterJobFilter
    {
        Task OnAfterRegisterJob(JobContext context);
    }
}
