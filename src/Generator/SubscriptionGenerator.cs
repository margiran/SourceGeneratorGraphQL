using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class SubscriptionGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var syntaxTrees = context.Compilation.SyntaxTrees;
            var models = syntaxTrees.Where(s => s.GetText().ToString().Contains("[BsonId]"));
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var builder = new StringBuilder($@"using GraphQLProject.Models;
using HotChocolate;
using HotChocolate.Types;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}.GraphQL
{{
    [GraphQLDescription(""Represents...."")]
    public partial class Subscription
    {{
");

            foreach (var model in models)
            {
                var usingDirectives = model.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>(); ;
                var textUsingDirectives = string.Join(";", usingDirectives.Select(s => s.GetText()));

                var classDeclaration = model.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                var className = classDeclaration.Identifier.ToString();

                var generateClassName = $"{className}test";
                var modelName = className.ToLowerInvariant();
                builder.Append($@"
        [Subscribe]
        [Topic]
        [GraphQLDescription(""The subscription for added {className}"")]
        public {className} On{className}Added([EventMessage] {className} {modelName})
        {{
            return {modelName};
        }}
        ");
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");
            context.AddSource($"Subscription.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
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