using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy2;

namespace FluentValidationSamples
{
    [Intercept(typeof(ValidationInterceptor))]
    public class OrderService : IOrderService
    {
        private OrderDataAccess orderDataAccess = new OrderDataAccess();

        public OrderCreatingResult AddOrder(Order order)
        {
            var result = new OrderCreatingResult();

            // 計算總價
            order.TotalPrice = order.Amount * order.UnitPrice;

            // 將訂單資訊寫入資料庫
            this.orderDataAccess.Add(order);

            result.IsSuccess = true;
            result.Order = order;

            return result;
        }

        [IgnoreInterception(typeof(ValidationInterceptor))]
        public OrderDeletingResult Delete(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
