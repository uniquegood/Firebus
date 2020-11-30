using System;
using System.Collections.Generic;
using System.Text;

namespace Firebus
{
    public interface IExecuteJobFilter: IAfterExecuteJobFilter, IBeforeExecuteJobFilter
    {
    }
}
