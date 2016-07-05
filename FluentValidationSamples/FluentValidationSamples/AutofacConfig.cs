using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using FluentValidation;

namespace FluentValidationSamples
{
    public class AutofacConfig
    {
        public static IContainer Container { get; private set; }

        public static void Config()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<OrderValidator>()
                .As<IValidator<Order>>();

            builder
                .RegisterType<ValidationInterceptor>();

            builder
                .RegisterType<OrderService>()
                .As<IOrderService>()
                .EnableInterfaceInterceptors();

            Container = builder.Build();
        }
    }
}
