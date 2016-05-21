using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationSamples
{
    public class OrderServiceWithValidation : OrderServiceWrapper
    {
        public OrderServiceWithValidation(IOrderService orderService)
            : base(orderService)
        {
        }

        public override OrderCreatingResult AddOrder(Order order)
        {
            var result = new OrderCreatingResult();

            var validator = new OrderValidator();

            var validationResult = validator.Validate(order);

            if (validationResult.IsValid)
            {
                result = this.orderService.AddOrder(order);
            }
            else
            {
                result.Message = string.Join("\r\n", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            return result;
        }

        public override OrderDeletingResult Delete(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
