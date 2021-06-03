using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesManagement.Models;
using SalesManagement.ServiceLayer;
using SalesManagement.Services;

namespace SalesManagement
{
    public class Startup
    {
        
        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            //string constr = this.Configuration.GetConnectionString("DefaultConnection");
            var builder = services.AddRazorPages();
            builder.AddRazorRuntimeCompilation();

            services.AddScoped<IProductDataAccessLayer, ProductDataAccessLayer>();
            services.AddScoped<ICustomerDataAccessLayer, CustomerDataAccessLayer>();
            services.AddScoped<ISaleDataAccessLayer, SaleDataAccessLayer>();
            services.AddScoped<IInvoiceDataAccessLayer, InvoiceDataAccessLayer>();
            services.AddScoped<IUserRegisterAccessLayer, UserRegisterAccessLayer>();
            services.AddScoped<IAdminRegisterAccessLayer, AdminRegisterAccessLayer>();
            services.AddSingleton<IUtilityServices, UtilityServices>();
            services.AddSingleton(Config);
           
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
