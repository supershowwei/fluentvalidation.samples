using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationSamples
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class IgnoreInterceptionAttribute : Attribute
    {
        public IgnoreInterceptionAttribute(Type interceptorServiceType)
        {
            this.InterceptorServiceType = interceptorServiceType;
        }

        public Type InterceptorServiceType { get; }
    }
}
