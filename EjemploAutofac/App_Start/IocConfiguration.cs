﻿using Autofac;
using Autofac.Integration.Mvc;
using EjemploAutofac.Interfaces;
using EjemploAutofac.Repositories;
using EjemploAutofac.Services;
using System.Reflection;
using System.Web.Mvc;

namespace EjemploAutofac.App_Start
{
    public static class IocConfiguration
    {
        public static IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterRepositories(builder);
            RegisterServices(builder);
            RegisterControllers(builder);

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }
    }
}