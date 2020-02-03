using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Dal.Core;
using ProjectName.Essential.Dal.Ef;
using ProjectName.Essential.DataService.Infrastructure;
using ProjectName.Essential.DataService.OData;

namespace ProjectName.Essential.DataService
{
    public partial class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(p => 
                p.UseLazyLoadingProxies()
                 .UseSqlServer(_configuration.GetConnectionString("Database")));

            services.AddCors();
            services.AddOData();
            services.AddMvc()
                .ConfigureApplicationPartManager(p => p.FeatureProviders
                    .Add(new GenericODataControllerFeatureProvider<DatabaseContext>()));

            ConfigureODataTriggers(services, typeof(ICreateTrigger<>));
            ConfigureODataTriggers(services, typeof(IUpdateTrigger<>));
            ConfigureODataTriggers(services, typeof(IDeleteTrigger<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IModelQueryBuilder, ModelQueryBuilder>();
            services.AddSingleton<IAuthorizationHandler, ODataControllerAuthorizationHandler>();
            //
            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("OData", opts => 
            //         opts.Requirements.Add(new ODataAuthorizationRequirement()));
            // });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opts =>
                {
                    opts.Authority = "http://localhost:5000";
                    opts.ApiName = "ProjectName.Essential.DataService";
                    opts.RequireHttpsMetadata = false;
                });
            
            // .AddJwtBearer(options =>
            // {
            //     options.Authority = "http://localhost:5000";
            //     options.RequireHttpsMetadata = false;
            //
            //     options.Audience = GetType().Namespace;
            // });
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
                .Build());
            
            app.UseAuthentication();
            
            app.UseMvc(builder =>
            {
                builder
                    .Select()
                    .Expand()
                    .Filter()
                    .OrderBy()
                    .Count()
                    .MaxTop(500);

                builder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
    }
}