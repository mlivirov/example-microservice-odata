using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.Options;

namespace ProjectName.Dal.Core.Ef.Scaffolding
{
    public class CustomCSharpDbContextGenerator : CSharpDbContextGenerator
    {
        private readonly string _modelsNamespace;

        private readonly string[] _excludedEntityTypes = new[]
        {
            // exclude liquibase operational tables
            "databasechangelog",
            "databasechangeloglock"
        };
        
        public CustomCSharpDbContextGenerator(
#pragma warning disable 618
            IEnumerable<IScaffoldingProviderCodeGenerator> legacyProviderCodeGenerators, 
#pragma warning restore 618
            IEnumerable<IProviderConfigurationCodeGenerator> providerCodeGenerators, 
            IAnnotationCodeGenerator annotationCodeGenerator, 
            ICSharpHelper cSharpHelper,
            SharpDbContextGeneratorConfiguration options)
            : base(legacyProviderCodeGenerators, providerCodeGenerators, annotationCodeGenerator, cSharpHelper)
        {
            _modelsNamespace = options.ModelsNamespace;
        }

        public override string WriteCode(IModel model, string @namespace, string contextName, string connectionString, bool useDataAnnotations,
            bool suppressConnectionStringWarning)
        {
            var result = base.WriteCode(
                new ModelDecorator(model, _excludedEntityTypes),
                @namespace,
                contextName,
                connectionString, 
                useDataAnnotations, 
                suppressConnectionStringWarning);

            result = CodeHelper.AppendUsing(result, _modelsNamespace);

            return result;
        }
    }
}