using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectName.Dal.Core.Ef.Scaffolding
{
    public static class CodeHelper
    {
        public static string AppendUsing(string code, string @using)
        {
            var lastUsing = Regex.Matches(code, @"using [\w.]+;").LastOrDefault();

            var usingLine = $"using {@using};";
            if (lastUsing != null)
            {
                code = code.Insert(
                    lastUsing.Index + lastUsing.Length,
                    $"{Environment.NewLine}{usingLine}");
            }
            else
            {
                code = $"{usingLine}{Environment.NewLine}{code}";
            }


            return code;
        }

        public static string AppendInterface(string code, IEntityType entityType, Type type)
        {
            var classDeclarationSnippet = $"class {entityType.Name}";
            var classDeclarationPosition = code.IndexOf(classDeclarationSnippet, StringComparison.InvariantCultureIgnoreCase);
            code = code.Insert(classDeclarationPosition + classDeclarationSnippet.Length, $" : {type.Name}");

            return code;
        }
    }
}