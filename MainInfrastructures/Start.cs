using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MainInfrastructures
{
    public class Start
    {
        public static void Buidler(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
