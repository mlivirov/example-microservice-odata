using System;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Dal.Core.Ef.Scaffolding;

namespace ProjectName.Essential.Dal.Ef.Scaffolding
{
    public class ScaffoldingDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(new SharpDbContextGeneratorConfiguration
            {
                ModelsNamespace = GetModelsNamespace()
            });

            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, CustomCSharpEntityTypeGenerator>();
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, CustomCSharpDbContextGenerator>();
        }
        
        private string GetModelsNamespace()
        {
            var fullName = GetType().Assembly.GetName().Name;
            var lastIndex = fullName.LastIndexOf(".", StringComparison.Ordinal);
            return $"{fullName.Substring(0, lastIndex)}.Core.Models";
        }
    }
}