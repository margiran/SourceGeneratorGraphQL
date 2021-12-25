using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class MutationSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var syntaxTrees = context.Compilation.SyntaxTrees;
            var models = syntaxTrees.Where(s => s.GetText().ToString().Contains("[BsonId]"));
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var builder = new StringBuilder($@"using System;
using System.Threading;
using System.Threading.Tasks;
using GraphQLProject.Models;
using GraphQLProject.Repositories;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}.GraphQL
{{
    [GraphQLDescription(""Represents..."")]
    public partial class Mutation
    {{
");

            foreach (var model in models)
            {
                var usingDirectives = model.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>(); ;
                var textUsingDirectives = string.Join(";", usingDirectives.Select(s => s.GetText()));

                var classDeclaration = model.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                var className = classDeclaration.Identifier.ToString();

                var generateClassName = $"{className}test";
              //  var props = model.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>().Where(p => !p.AttributeLists.ToString().Contains("[DoNotExpose]")).Select(p => $"{p.Identifier}: input.{p.Identifier}").ToList();
                var props = model.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>().Where(p => !p.AttributeLists.ToString().Contains("[DoNotExpose]")).Select(p => $"input.{p.Identifier}").ToList();
                var ctorParameter = string.Join(",", props);
                var splitClass = classDeclaration.ToString().Split(new[] { '{' }, 2);
                var modelName= className.ToLowerInvariant();
                builder.Append($@"
         [GraphQLDescription($""Add a {className}."")]
         public async Task<Add{className}Payload> Add{className}Async(
            Add{className}Input input,
            [Service] IRepository<{className},Guid> service,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
            ) 
            {{
                var {modelName} = new {className}({ctorParameter});

                await service.AddAsync({modelName});

                await eventSender.SendAsync(nameof(Subscription.On{className}Added), {modelName}, cancellationToken);

                return new Add{className}Payload({modelName});
            }}

        ");
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            context.AddSource($"Mutation.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
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