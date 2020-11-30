using System;
using System.Collections.Generic;
using System.Text;
using Firebus.Client.Filters;

namespace Firebus.Client
{
    public class FirebusClientOptionsBuilder
    {
        public FirebusClientOptions Options { get; } = new FirebusClientOptions();


        public FirebusClientOptionsBuilder AddBeforeRegisterJobFilter(IBeforeRegisterJobFilter filter)
        {
            Options.BeforeRegisterJobFilters.Add(filter);
            return this;
        }

        public FirebusClientOptionsBuilder AddBeforeRegisterJobFilter<TFilter>() where TFilter : IBeforeRegisterJobFilter, new()
        {
            Options.BeforeRegisterJobFilters.Add(new TFilter());
            return this;
        }

        public FirebusClientOptionsBuilder AddAfterRegisterJobFilter(IAfterRegisterJobFilter filter)
        {
            Options.AfterRegisterJobFilters.Add(filter);
            return this;
        }

        public FirebusClientOptionsBuilder AddAfterRegisterJobFilter<TFilter>() where TFilter : IAfterRegisterJobFilter, new()
        {
            Options.AfterRegisterJobFilters.Add(new TFilter());
            return this;
        }

        public FirebusClientOptionsBuilder AddRegisterJobFilter(IRegisterJobFilter filter)
        {
            Options.BeforeRegisterJobFilters.Add(filter);
            Options.AfterRegisterJobFilters.Add(filter);
            return this;
        }

        public FirebusClientOptionsBuilder AddRegisterJobFilter<TFilter>() where TFilter : IRegisterJobFilter, new()
        {
            var filterInstance = new TFilter();
            Options.BeforeRegisterJobFilters.Add(filterInstance);
            Options.AfterRegisterJobFilters.Add(filterInstance);
            return this;
        }
    }
}
