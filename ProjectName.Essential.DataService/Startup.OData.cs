using System;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using ProjectName.Dal.Core;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.DataService
{
    public partial class Startup
    {
        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            var modelsAssembly = typeof(Person).Assembly;
            var types = DatabaseHelper.GetEntityTypes(modelsAssembly)
                .Union(DatabaseHelper.GetViewTypes(modelsAssembly));

            foreach (var type in types)
            {
                var entityType = builder.AddEntityType(type);
                var configuration = builder.AddEntitySet(type.Name, entityType);
            }

            return builder.GetEdmModel();
        }

        private void ConfigureODataTriggers(IServiceCollection services, Type type)
        {
            var triggerTypes = GetType().Assembly.DefinedTypes
                .Where(p => p.GetInterfaces()
                    .Any(ifs => ifs.IsGenericType && ifs.GetGenericTypeDefinition() == type));
            
            foreach (var triggerType in triggerTypes)
            {
                var interfaces = triggerType.GetInterfaces().Where(p => p.GetGenericTypeDefinition() == type);
                foreach (var @interface in interfaces)
                {
                    services.AddScoped(@interface, triggerType);
                }
            }
        }
    }
}