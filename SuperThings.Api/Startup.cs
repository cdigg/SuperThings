using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperThings.Data.Repository;
using SuperThings.Logic.Services;
using SuperThings.Logic.Shared;

namespace SuperThings.Api
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
            //db connection
            services.AddDbContext<SuperThingsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SuperThingsContext")));

            //services
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IMonitoringService, MonitoringService>();

            //repositories
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();

            services.AddControllers();

            //automapper
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
