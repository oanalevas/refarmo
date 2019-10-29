// <copyright>
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Oana Leva</author>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using refarmo.Models;
using refarmo.Models.DataManager;
using refarmo.Models.Repository;
using refarmo.Utility;

namespace refarmo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<RNTSContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:TestDB"], x => x.UseNetTopologySuite()));
            services.AddScoped<IDataRepository<FeatureCollection>, FeatureCollectionManager>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //Related data and serialization, Because EF Core will automatically fix-up navigation properties, you can end up with cycles in your object graph.
                //Configures Json.NET to ignore cycles that it finds in the object graph.
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTransient<GeoJsonDataSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, GeoJsonDataSeeder jsonSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Run just once on startup to populate the database with geojson data; next time will fail anyway
            //TODO Refactor
            try
            {
                jsonSeeder.Seed();
            }
            catch(Exception e)
            {

            }
        }
    }
}
