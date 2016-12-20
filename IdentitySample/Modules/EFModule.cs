using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Autofac;
using IdentitySample.Entities;
using IdentitySample.Repository;
using IdentitySample.Models;

namespace IdentitySample.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
         //   builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(ScoreSystemDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();         
        }
    }
}