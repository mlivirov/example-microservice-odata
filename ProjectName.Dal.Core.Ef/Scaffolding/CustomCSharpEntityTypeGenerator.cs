using System;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.Options;

namespace ProjectName.Dal.Core.Ef.Scaffolding
{
    public class CustomCSharpEntityTypeGenerator : CSharpEntityTypeGenerator
    {
        private readonly string _modelsNamespace;
        
        public CustomCSharpEntityTypeGenerator(
            ICSharpHelper cSharpHelper, 
            SharpDbContextGeneratorConfiguration options) : base(cSharpHelper)
        {
            _modelsNamespace = options.ModelsNamespace;
        }

        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            var result = base.WriteCode(entityType, _modelsNamespace, useDataAnnotations);

            result = CodeHelper.AppendUsing(result, typeof(IEntity).Namespace);
            result = CodeHelper.AppendInterface(result, entityType, typeof(IEntity));

            return result;
        }
    }
}