using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using ProjectName.Dal.Core;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.DataService.OData
{
    public class GenericODataControllerFeatureProvider<T> : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var modelsAssembly = typeof(Person).Assembly;
            foreach (var entityType in DatabaseHelper.GetEntityTypes(modelsAssembly))
            {
                var controllerType = typeof(GenericODataEntityController<>)
                    .MakeGenericType(entityType)
                    .GetTypeInfo();

                feature.Controllers.Add(controllerType);
            }

            foreach (var viewType in DatabaseHelper.GetViewTypes(modelsAssembly))
            {
                var controllerType = typeof(GenericODataViewController<>)
                    .MakeGenericType(viewType)
                    .GetTypeInfo();

                feature.Controllers.Add(controllerType);
            }
        }
    }
}