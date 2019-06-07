using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Dal.Core;
using ProjectName.Essential.Dal.Ef;
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

            services.AddOData();
            services.AddMvc()
                .ConfigureApplicationPartManager(p => p.FeatureProviders
                    .Add(new GenericODataControllerFeatureProvider<DatabaseContext>()));

            ConfigureODataTriggers(services, typeof(ICreateTrigger<>));
            ConfigureODataTriggers(services, typeof(IUpdateTrigger<>));
            ConfigureODataTriggers(services, typeof(IDeleteTrigger<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IModelQueryBuilder, ModelQueryBuilder>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = _configuration.GetValue<string>("Endpoints.SingleSignOnUrl");
                    options.Audience = GetType().Namespace;
                });
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

            app.UseAuthentication();
        }
    }
}