using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UserHandler
{
    public class Start
    {
        public static void Builder(ContainerBuilder builder)
        {
            builder.AddMediatR(Assembly.GetExecutingAssembly());

        }
    }
}
