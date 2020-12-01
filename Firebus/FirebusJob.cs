using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Firebus
{
    public class FirebusJob
    {
        public string ServiceTypeName { get; set; }
        public string MethodName { get; set; }
        public object[] Parameters { get; set; }
        public string[] ParameterTypeNames { get; set; }
        public Dictionary<string, object> Items { get; set; }
    }
}
