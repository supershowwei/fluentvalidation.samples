using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationSamples
{
    public abstract class OrderServiceWrapper : IOrderService
    {
        protected IOrderService orderService;

        public OrderServiceWrapper(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public abstract OrderCreatingResult AddOrder(Order order);

        public abstract OrderDeletingResult Delete(Order order);
    }
}
