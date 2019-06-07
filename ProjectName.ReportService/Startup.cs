using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Dal.Core;
using ProjectName.Dal.OData;
using ProjectName.ReportService.Application.Services;
using Simple.OData.Client;

namespace ProjectName.ReportService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddSingleton<IODataClient>(new ODataClient(_configuration["ODataClient:EndpointUrl"]));
            services.AddSingleton<HttpClient>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IModelQueryBuilder, ModelQueryBuilder>();
            services.AddTransient<IReportService, Application.Services.ReportService>();

            services.Configure<HttpFileStorageOptions>(_configuration);
            services.AddTransient<IFileStorage, HttpFileStorage>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .Build());

            app.UseMvc();
        }
    }
}