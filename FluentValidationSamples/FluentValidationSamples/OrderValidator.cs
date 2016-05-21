using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace FluentValidationSamples
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(order => order.Id)
                .GreaterThan(0).WithName("訂單代號").WithMessage("{PropertyName} 必須大於 0。");

            RuleFor(order => order.CustomerName)
                .NotNull().WithMessage("{PropertyName} 不可以是 null。")
                .NotEmpty().WithMessage("{PropertyName} 不可以是空字串。");

            RuleFor(order => order.CustomerName)
                .Length(0, 30).WithMessage("{PropertyName} 的長度必須小於 30。");

            RuleFor(order => order.ProductName)
                .NotNull().WithMessage("{PropertyName} 不可以是 null")
                .NotEmpty().WithMessage("{PropertyName} 不可以是空字串");

            RuleFor(order => order.ProductName)
                .Length(0, 20).WithMessage("{PropertyName} 的長度必須小於 20。");

            RuleFor(order => order.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} 必須大於 0。");

            RuleFor(order => order.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} 必須大於等於 0。");

            RuleFor(order => order.UnitPrice)
                .LessThan(1000).WithMessage("{PropertyName} 必須小於 1000。");

            RuleFor(order => order.TotalPrice)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} 必須大於等於 0。");
        }

        /// <summary>
        /// 覆寫 Validate 方法，檢查傳進來的 instance 是否為 null。
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override ValidationResult Validate(Order instance)
        {
            return instance == null
                ? new ValidationResult(new[] { new ValidationFailure("Order", "Order cannot be null") })
                : base.Validate(instance);
        }
    }
}
