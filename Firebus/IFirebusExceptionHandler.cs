using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebus
{
    public interface IFirebusExceptionHandler
    {
        Task HandleAsync(Exception e);
    }
}
