using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectName.Essential.DataService.OData;

namespace ProjectName.Essential.DataService.Infrastructure
{
    public class ODataControllerAuthorizationHandler : AuthorizationHandler<ODataAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ODataAuthorizationRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext mvcContext 
                && mvcContext.ActionDescriptor is ControllerActionDescriptor descriptor
                && descriptor.ControllerTypeInfo.IsGenericType 
                && descriptor.ControllerTypeInfo.GetGenericTypeDefinition() == typeof(GenericODataEntityController<>))
            {
                var entityType = descriptor.ControllerTypeInfo.GenericTypeArguments.First();
                var requiredRole = $"{entityType.Name}:{descriptor.ActionName}";

                if (context.User.IsInRole(requiredRole))
                {
                    context.Succeed(new ODataAuthorizationRequirement());
                }
                else
                {
                    context.Fail();
                }
            }
            
            return Task.CompletedTask;
        }
    }
}