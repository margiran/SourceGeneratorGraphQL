using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class RegisterServiceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var syntaxTrees = context.Compilation.SyntaxTrees;
            var models = syntaxTrees.Where(s => s.GetText().ToString().Contains("[BsonId]"));
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var builder = new StringBuilder($@"using System;
using GraphQLProject.Models;
using GraphQLProject.Mongo;
using GraphQLProject.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}.SetupServices
{{
    public class RegisterService : IServiceInstallers
    {{
        public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
        {{
");

            foreach (var model in models)
            {
                var classDeclaration = model.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                var className = classDeclaration.Identifier.ToString();
                builder.Append($@"
            services.AddScoped<IRepository<{className},Guid>,Repository<{className},Guid>>();
        ");
            }
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            context.AddSource($"RegisterService.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
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