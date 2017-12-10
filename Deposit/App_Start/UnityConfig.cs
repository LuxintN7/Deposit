using System;
using DepositDatabase;
using DepositDatabase.Handlers;
using DomainLogic;
using DomainLogic.Handlers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Data.Entity;
using Deposit.Models;
using Microsoft.AspNet.Identity;
using Deposit.Controllers;
using DepositDatabase.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using DepositDatabase.Model.DbContextFactories;

namespace Deposit.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<ICardsService, CardsService>(new InjectionConstructor());
            container.RegisterType<IDepositWaysOfAccumulationService, DepositWaysOfAccumulationService>(new InjectionConstructor());
            container.RegisterType<IDepositTermsService, DepositTermsService>(new InjectionConstructor());
            container.RegisterType<ICurrenciesService, CurrenciesService>(new InjectionConstructor());
            container.RegisterType<IAddCardHandler, AddCardHandler>(new InjectionConstructor());
            container.RegisterType<INewDepositHandler, NewDepositHandler>(new InjectionConstructor());
            container.RegisterType<ICloseDepositHandler, CloseDepositHandler>(new InjectionConstructor());
            
            container.RegisterType<DbContext, DepositDbContext>(new InjectionConstructor());
            container.RegisterType<IDepositDbContextFactory, DepositDbContextFactoryWithPresetData>(new InjectionConstructor());

            container.RegisterType<UserManager<AspNetUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<AspNetUser>, UserStore<AspNetUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IUserStore<AspNetUser>, UserStore<AspNetUser>>(new InjectionConstructor());
        }
    }
}
