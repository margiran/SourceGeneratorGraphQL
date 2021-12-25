using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class AddPayloadsGenerator : ISourceGenerator
    {
            public void Execute(GeneratorExecutionContext context)
            {
                // Find the main method
                var syntaxTrees = context.Compilation.SyntaxTrees;
                var models = syntaxTrees.Where(s => s.GetText().ToString().Contains("[BsonId]"));
                var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
                var builder = new StringBuilder($@"
using {mainMethod.ContainingNamespace.ToDisplayString()}.Models;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}.GraphQL
{{
");

                foreach (var model in models)
                {
                    var classDeclaration = model.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                    var className = classDeclaration.Identifier.ToString();

                    builder.Append($@"
    public record Add{className}Payload({className} model);

");
                }
                builder.AppendLine("}");
                //var typeName = mainMethod.ContainingType.Name;
                // builder.AppendLine(splitClass[1].Replace(className,generateClassName));
                // Add the source code to the compilation
                context.AddSource($"AddPayloads.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
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


