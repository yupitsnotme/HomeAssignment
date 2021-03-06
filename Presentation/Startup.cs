using Application.Services;
using DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using DataAccess.Repositories;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<FileTransferContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection")));



            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<FileTransferContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //these lines of code will inform the injector class what implementation to instantiate
            //for the requested interface

            //AddScoped > 1 instance per request
            //         > e.g. user opens the Index method and the index method makes two calls for the same repository
            //                class. result: 1 instance of the repository class is created

            services.AddScoped<IFileTransfersService, FileTransfersService>();
            services.AddScoped<IFileTransfersRepository, FileTransfersRepository>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=FileTransfer}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
            });
        }
    }
}
