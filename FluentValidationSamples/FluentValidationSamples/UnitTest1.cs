using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentValidationSamples
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_AddOrder_with_Decorator()
        {
            var orderService = new OrderServiceWithValidation(new OrderService());
            var order = new Order() { CustomerName = "" };

            var result = orderService.AddOrder(order);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Test_OrderValidator()
        {
            var validator = new OrderValidator();

            var validationResults = validator.Validate(new Order());

            // Id = 0 驗證應該要有錯
            validator.ShouldHaveValidationErrorFor(order => order.Id, 0);

            // Id = 1 驗證不應該有錯
            validator.ShouldNotHaveValidationErrorFor(order => order.Id, 1);
        }
    }
}
