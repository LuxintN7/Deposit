using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using DepositCore.Data;
using DepositCore.Models;
using DepositCore.Services;
using DepositDatabaseCore;
using DepositDatabaseCore.Handlers;
using DepositDatabaseCore.Model;
using DomainLogic;
using DomainLogic.Handlers;

namespace DepositCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DepositDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")/*, b => b.MigrationsAssembly("DepositDatabaseCore")*/));

            // Use SQLite fopr deploying on Linux
            services.AddDbContext<DepositDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnectionSQLite")));

            services.AddIdentity<AspNetUser, IdentityRole>()
                .AddEntityFrameworkStores<DepositDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<ICardsService, CardsService>();
            services.AddTransient<IDepositWaysOfAccumulationService, DepositWaysOfAccumulationService>();
            services.AddTransient<IDepositTermsService, DepositTermsService>();
            services.AddTransient<ICurrenciesService, CurrenciesService>();
            services.AddTransient<IAddCardHandler, AddCardHandler>();
            services.AddTransient<INewDepositHandler, NewDepositHandler>();
            services.AddTransient<ICloseDepositHandler, CloseDepositHandler>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
