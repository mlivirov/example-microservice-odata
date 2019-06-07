using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ProjectName.Essential.DataService.OData
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenericODataControllerNameConventionAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType 
                || (controller.ControllerType.GetGenericTypeDefinition() != (typeof(GenericODataEntityController<>))
                    && controller.ControllerType.GetGenericTypeDefinition() != (typeof(GenericODataViewController<>))))
            {
                return;
            }

            var entityTypeGenericAttributeIndex = 0;
            var entityType = controller.ControllerType.GenericTypeArguments[entityTypeGenericAttributeIndex];
            controller.ControllerName = entityType.Name;
        }
    }
}