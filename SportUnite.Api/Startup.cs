using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SportUnite.Data;
using SportUnite.Data.Abstract;
using SportUnite.Data.Concrete;
using SportUnite.Data.Sportevent.Abstract;
using SportUnite.Domain;
using SportUnite.Logic;

namespace SportUnite.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportUnite:ConnectionString"]));
            services.AddTransient<ISportEventManager, SportEventManager>();
            services.AddTransient<ISportEventRepository, EfSportEventRepository>();
            services.AddTransient<IBuildingManager, EFBuildingManager>();
            services.AddTransient<IBuildingRepository, EFBuildingRepository>();
            services.AddTransient<IHallManager, EFHallManager>();
            services.AddTransient<IHallRepository, EFHallRepository>();
	        services.AddTransient<ISportRepository, EFSportRepository>();
	        services.AddTransient<ISportManager, SportManager>();


            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200", "https://sportuniteb3-angular-api.herokuapp.com", "https://sportuniteb3-angular.herokuapp.com")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMvc();
        }
    }
}
