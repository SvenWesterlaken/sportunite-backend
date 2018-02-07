using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportUnite.Data;
using SportUnite.Data.Abstract;
using SportUnite.Data.Concrete;
using SportUnite.Data.Sportevent.Abstract;
using SportUnite.Logic;
using SportUnite.UI.Models.ModelBinder;


namespace SportUnite.UI
{
	public class Startup
	{

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", false, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();

			HostingEnvironment = env;
		}

		public IConfigurationRoot Configuration { get; }
		public IHostingEnvironment HostingEnvironment { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
			{
				// add custom binder
				options.ModelBinderProviders.Insert(0, new SportEventModelBinderProvider());
				options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
			});
			services.AddMemoryCache();
            services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration["Data:SportUnite:ConnectionString"]));
			services.AddDbContext<AppIdentityDbContext>(options =>
				options.UseSqlServer(
					Configuration["Data:SportUniteIdentity:ConnectionString"]));
			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>();
			services.AddTransient<ISportEventRepository, EfSportEventRepository>();
			services.AddTransient<ISportEventManager, SportEventManager>();
			services.AddTransient<ISportRepository, EFSportRepository>();
		    services.AddTransient<ISportManager, SportManager>();
		    services.AddTransient<IBuildingRepository, EFBuildingRepository>();
		    services.AddTransient<IBuildingManager, EFBuildingManager>();
		    services.AddTransient<IHallRepository, EFHallRepository>();
		    services.AddTransient<IHallManager, EFHallManager>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
			services.AddSession();


		}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();
			app.UseSession();
			app.UseIdentity();

		    app.UseMvc(routes =>
		    {
		        routes.MapRoute(
		            name: "default",
		            template: "{Controller=Home}/{Action=Index}/{id?}");
		    });

			SeedData.EnsurePopulated(app);
			IdentitySeedData.EnsurePopulated(app);
		}


   
        
    }
}
