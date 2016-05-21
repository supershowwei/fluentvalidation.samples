using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationSamples
{
    public abstract class ServiceResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
