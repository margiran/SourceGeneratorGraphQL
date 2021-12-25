using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class QuerySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var syntaxTrees= context.Compilation.SyntaxTrees;
            var models= syntaxTrees.Where(s => s.GetText().ToString().Contains("[BsonId]"));
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var builder= new StringBuilder($@"using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQLProject.Models;
using GraphQLProject.Repositories;
using HotChocolate;
using HotChocolate.Data;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}.GraphQL
{{
    [GraphQLDescription(""Represents..."")]
    public partial class Query
    {{
");

            foreach (var model in models)
            {
                var usingDirectives=model.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>(); ;
                var textUsingDirectives=string.Join(";", usingDirectives.Select(s => s.GetText()));
                
                //var builder= new StringBuilder(textUsingDirectives);

                var classDeclaration= model.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                var className= classDeclaration.Identifier.ToString();

                var generateClassName=$"{className}test";

                var splitClass= classDeclaration.ToString().Split(new [] {'{'},2);
        builder.Append($@"
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription($""Gets the queryable {className}."")]
        public async Task< IQueryable<{className}>> Get{className}([Service] IRepository<{className},Guid> service)
        {{
            return await service.BrowseAsync();
        }}
        ");
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            //var typeName = mainMethod.ContainingType.Name;
           // builder.AppendLine(splitClass[1].Replace(className,generateClassName));
            // Add the source code to the compilation
             context.AddSource($"query.g.cs", SourceText.From(builder.ToString(),Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
        }
    }
}