using Analyzer.Config;
using Analyzer.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Analyzer.Analyzer
{
    [DiagnosticAnalyzer(Microsoft.CodeAnalysis.LanguageNames.CSharp)]
    public class GameServiceAttributeAnalyzer : DiagnosticAnalyzer
    {
        public const string GameServiceAttributeNameSpace = "Game";
        public const string GameServiceAttributeName = "GameServiceAttribute";
        public const string GameServiceClassName = "GameService";
        public const string GameServiceAttributeDefaultParam = "GameServiceLifeSpan.None";
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.GameServiceAttributeRule.Rule);

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

                // check if class has GameServiceAttribute
                var hasGameServiceAttribute = classTypeSymbol.GetAttributes().Any(a => a.AttributeClass.Name.Equals(GameServiceAttributeName));
                if (!hasGameServiceAttribute)
                {
                    var location = classDeclaration.Identifier.GetLocation();
                    var diagnostic = Diagnostic.Create(DiagnosticRules.GameServiceAttributeRule.Rule, location);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
