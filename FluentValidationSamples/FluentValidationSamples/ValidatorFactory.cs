using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using FluentValidation;

namespace FluentValidationSamples
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type type)
        {
            Type validatorType = typeof(IValidator<>).MakeGenericType(typeof(Order));

            var serviceTypes =
                AutoConfig.Container.ComponentRegistry.Registrations
                    .SelectMany(r => r.Services.OfType<TypedService>())
                    .Select(s => s.ServiceType);

            if (serviceTypes.Contains(validatorType))
            {
                return AutoConfig.Container.Resolve(validatorType) as IValidator;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)this.GetValidator(typeof(T));
        }
    }
}
