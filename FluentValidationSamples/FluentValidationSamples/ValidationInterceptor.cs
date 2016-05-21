using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using FluentValidation;
using FluentValidation.Results;

namespace FluentValidationSamples
{
    public class ValidationInterceptor : IInterceptor
    {
        private static readonly IValidatorFactory ValidatorFactory = new ValidatorFactory();

        public void Intercept(IInvocation invocation)
        {
            if (IsIgnoreValidation(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
            }
            else
            {
                var validationResult = ValidateArguments(invocation.Arguments);

                if (validationResult.IsValid)
                {
                    invocation.Proceed();
                }
                else
                {
                    ServiceResult serviceResult = Activator.CreateInstance(invocation.Method.ReturnType) as ServiceResult;
                    serviceResult.IsSuccess = false;
                    serviceResult.Message = string.Join("\r\n", validationResult.Errors.Select(err => err.ErrorMessage));

                    invocation.ReturnValue = serviceResult;
                }
            }
        }

        private ValidationResult ValidateArguments(object[] arguments)
        {
            var validationResult = new ValidationResult();

            foreach (var arg in arguments)
            {
                var validator = ValidatorFactory.GetValidator(arg.GetType());

                validationResult =
                    validator == null
                        ? new ValidationResult()
                        : validator.Validate(arg);

                if (validationResult.IsValid == false)
                {
                    return validationResult;
                }
            }

            return validationResult;
        }

        private bool IsIgnoreValidation(MethodInfo methodInvocationTarget)
        {
            return
                methodInvocationTarget
                    .CustomAttributes
                    .Where(attr => attr.AttributeType.Equals(typeof(IgnoreInterceptionAttribute)))
                    .SelectMany(attr => attr.ConstructorArguments.Select(arg => arg.Value))
                    .Any(type => type.Equals(this.GetType()));
        }
    }
}
