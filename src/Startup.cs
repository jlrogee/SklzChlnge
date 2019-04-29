﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using src.BL;
using src.DAL;

namespace src
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IShortLinkGenerator, ShortLinkGenerator>();
            services.AddTransient<ILinksCtx, LinksCtx>();
            services.AddTransient<ILinksService, LinksService>();
            services.AddTransient<ILinksRepository, LinksRepository>();
            
            services.Configure<MongoSettings>(
                options => { 
                    options.ConnectionString 
                        = Configuration.GetSection("MongoDb:ConnectionString").Value; 
                    options.Database
                        = Configuration.GetSection("MongoDb:Database").Value; 
                }
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}