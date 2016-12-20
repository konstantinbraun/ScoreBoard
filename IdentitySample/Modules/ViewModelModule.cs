using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using System.Reflection;

namespace IdentitySample.Modules
{
    public class ViewModelModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("IdentitySample.ViewModels"))
                      .Where(t => t.Name.EndsWith("Model1"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
}