using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Store.DataModel;
using System.IO;


namespace Store.WebApp
{
    public class Startup
    {
        const string connectionStringPath = "../connectionString.txt";
        static string GetConnectionString(string path)
        {
            string connectionString;
            connectionString = File.ReadAllText(path);
            return connectionString;
        }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLogging(lb => lb.AddEventLog());
            services.AddDbContext<Project1Context>(optionsBuilder => 
                optionsBuilder.UseSqlServer(
                    Configuration.GetConnectionString("SqlServer")
                    // GetConnectionString(connectionStringPath)
                    ));
            services.AddScoped<IRepository, Repository>();
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
