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
using Microsoft.Extensions.Options;
using SuperThings.Data.Repository;
using SuperThings.Data.Repository.Interfaces;
using SuperThings.Data.Repository.Mongo;
using SuperThings.Data.Repository.SQL;
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
            var settings = Configuration.GetSection("SuperThingsDatabaseSettings").Get<SuperThingsDatabaseSettings>();

            if (settings.UseSql)
            {
                //db connection
                services.AddDbContext<SuperThingsSqlContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SuperThingsContext")));    //repositories
                                                                                                                                                           //SQL
                services.AddScoped<IRegistrationRepository, RegistrationRepository_SQL>();
            }
            else
            {
                services.AddSingleton<ISuperThingsDatabaseSettings>(sp => settings);
                //mongo
                services.AddScoped<IRegistrationRepository, RegistrationRepository_Mongo>();
            }

            //services
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IMonitoringService, MonitoringService>();

        

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
