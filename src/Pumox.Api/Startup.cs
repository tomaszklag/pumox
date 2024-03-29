﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumox.Infrastructure.Authentication;
using Pumox.Infrastructure.Data;
using Pumox.Infrastructure.Mvc;
using Pumox.Services;

namespace Pumox.Api
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
            services.RegisterAllRepositories();
            services.RegisterAllServices();
            services.RegisterAllValidators();
            services.AddBasicAuthentication();
            services.AddErrorHandlerMiddleware();
            services.ConfigureFakeAdmins();
            services.AddSqlite();
            services.AddCustomMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.BuildDatabase();
            app.UseErrorHandler();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
