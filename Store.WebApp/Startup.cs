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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Store.DataModel;

using System.IO;
using Microsoft.Extensions.Logging;

namespace Store.WebApp
{
    public class Startup
    {
        const string connectionStringPath = "../connectionString.txt";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        static string GetConnectionString(string path)
        {
            string connectionString;
            connectionString = File.ReadAllText(path);
            return connectionString;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLogging(lb => lb.AddEventLog()); // FIXME: lookpu AddLogging
            services.AddDbContext<Project1Context>(optionsBuilder => 
                optionsBuilder.UseSqlServer(GetConnectionString(connectionStringPath)));
            services.AddScoped<ISession, Session>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
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
            app.UseMvcWithDefaultRoute();
        }
    }
}
