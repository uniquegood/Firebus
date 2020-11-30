using System.Threading.Tasks;
using Firebus.Server;

namespace Firebus.Client.Filters
{
    public interface IBeforeRegisterJobFilter
    {
        Task<bool> OnBeforeRegisterJob(JobContext context);
    }
}
