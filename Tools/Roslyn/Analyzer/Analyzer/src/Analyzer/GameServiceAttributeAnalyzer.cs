using Analyzer.Config;
using Analyzer.Extension;
using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Analyzer.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class GameServiceAttributeAnalyzer : DiagnosticAnalyzer
    {
        public const string GameServiceAttributeNameSpace = "Game";
        public const string GameServiceAttributeName = "GameServiceAttribute";
        public const string GameServiceClassName = "GameService";
        public const string GameServiceAttributeDefaultParam = "GameServiceDomain.None";
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
            DiagnosticRules.GameServiceAttributeRule.Rule,
            DiagnosticRules.RequirePartialRule.Rule,
            DiagnosticRules.GameServiceAttributeNoneRule.Rule
            );

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

            context.RegisterCompilationStartAction(analysisContext =>
            {
                if (AnalyzerHelper.IsAssemblyNeedAnalyze(analysisContext.Compilation.AssemblyName, Definitions.TargetAssemblyName))
                {
                    analysisContext.RegisterSemanticModelAction(AnalyzeAction);
                }
            });
        }

        private void AnalyzeAction(SemanticModelAnalysisContext context)
        {
            var classDeclarations = context.SemanticModel.SyntaxTree.GetRoot().DescendantNodes<ClassDeclarationSyntax>();

            foreach (var classDeclaration in classDeclarations)
            {
                var classTypeSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
                if (classTypeSymbol == null)
                {
                    continue;
                }

                // check if class is not abstract and is subclass of GameService
                if (classTypeSymbol.IsAbstract || !classTypeSymbol.BaseType?.Name.Equals(GameServiceClassName) == true)
                {
                    continue;
                }

                // check if class has no partial modifier
                if (!classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
                {
                    var location = classDeclaration.Identifier.GetLocation();
                    var diagnostic = Diagnostic.Create(DiagnosticRules.RequirePartialRule.Rule, location, GameServiceClassName);
                    context.ReportDiagnostic(diagnostic);
                }

                // check if class has GameServiceAttribute
                var gameServiceAttribute = classTypeSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.Name.Equals(GameServiceAttributeName));
                if (gameServiceAttribute == null)
                {
                    var location = classDeclaration.Identifier.GetLocation();
                    var diagnostic = Diagnostic.Create(DiagnosticRules.GameServiceAttributeRule.Rule, location);
                    context.ReportDiagnostic(diagnostic);
                }
                else
                {
                    var enumTypeName = "GameServiceDomain";
                    var uselessEnumValue = "None";
                    var attributeLocation = gameServiceAttribute.ApplicationSyntaxReference.GetSyntax().GetLocation();

                    // check if GameServiceAttribute has correct parameter, location should be on the enum value
                    var argumentList = gameServiceAttribute.ConstructorArguments;
                    var argument = argumentList.FirstOrDefault();
                    var typeWithoutNamespace = argument.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                    var valueString = argument.Value.ToString();

                    if (typeWithoutNamespace.Equals(enumTypeName))
                    {
                        var attributeSyntaxNode = gameServiceAttribute.ApplicationSyntaxReference.GetSyntax();
                        var argumentListSyntaxNode = attributeSyntaxNode.DescendantNodes<AttributeArgumentListSyntax>().FirstOrDefault();
                        var argumentSyntaxNode = argumentListSyntaxNode.DescendantNodes<AttributeArgumentSyntax>().FirstOrDefault();
                        var expression = argumentSyntaxNode.Expression;
                        // the value is the second child of the expression
                        var value = expression.ChildNodes().ElementAt(1);
                        var valueText = value.GetText();
                        if (valueText.ToString().Equals(uselessEnumValue))
                        {
                            var location = value.GetLocation();
                            var diagnostic = Diagnostic.Create(DiagnosticRules.GameServiceAttributeNoneRule.Rule, location);
                            context.ReportDiagnostic(diagnostic);
                        }
                    }
                }
            }
        }
    }
}
