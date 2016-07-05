using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using FluentValidation;

namespace FluentValidationSamples
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type type)
        {
            Type validatorType = typeof(IValidator<>).MakeGenericType(type);

            var serviceTypes =
                AutofacConfig.Container.ComponentRegistry.Registrations
                    .SelectMany(r => r.Services.OfType<TypedService>())
                    .Select(s => s.ServiceType);

            if (serviceTypes.Contains(validatorType))
            {
                return AutofacConfig.Container.Resolve(validatorType) as IValidator;
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
