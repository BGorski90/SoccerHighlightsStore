using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using SoccerHighlightsStore.Storefront.Payments;
using Autofac.Integration.Mvc;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using SoccerHighlightsStore.Storefront.Controllers;
using SoccerHighlightsStore.Storefront.Areas.Admin.Controllers;

namespace SoccerHighlightsStore.Storefront
{
    public static class AutofacConfig
    {
        public static void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<VideoRepository>().As<IVideoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MockPaymentProcessor>().As<IPaymentProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<StoreController>()
                                .UsingConstructor(typeof(IVideoRepository), typeof(IUserRepository));
            builder.RegisterType<CartController>()
                                .UsingConstructor(typeof(IVideoRepository));
            builder.RegisterType<WishlistController>()
                                .UsingConstructor(typeof(IUserRepository), typeof(IVideoRepository));
            builder.RegisterType<OrderController>()
                                .UsingConstructor(typeof(IVideoRepository), typeof(IOrderRepository), typeof(IUserRepository), typeof(IPaymentProcessor));
            builder.RegisterType<HomeController>()
                                .UsingConstructor(typeof(IVideoRepository), typeof(IOrderRepository), typeof(IUserRepository));
            builder.RegisterType<VideosController>()
                                .UsingConstructor(typeof(IVideoRepository));
            builder.RegisterType<OrdersController>()
                                .UsingConstructor(typeof(IOrderRepository));
            builder.RegisterType<UsersController>()
                                .UsingConstructor(typeof(IUserRepository));
            builder.RegisterType<AccountController>()
                                .UsingConstructor(typeof(IUserRepository));
            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }
    }
}