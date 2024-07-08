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
    public class UI2DAttributeAnalyzer : DiagnosticAnalyzer
    {
        public const string UI2DAttributeNameSpace = "Game";
        public const string UI2DAttributeName = "UI2DAttribute";
        public const string UI2DClassName = "UI2D";
        public const string UI2DAttributeDefaultParam = "\"\"";

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.UI2DAttributeRule.Rule);

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

                if (classTypeSymbol.IsAbstract)
                {
                    continue;
                }

                if (!HasBaseType(classTypeSymbol, UI2DClassName))
                {
                    continue;
                }

                var ui2DAttribute = classTypeSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.Name.Equals(UI2DAttributeName));
                if (ui2DAttribute == null)
                {
                    var diagnostic = Diagnostic.Create(DiagnosticRules.UI2DAttributeRule.Rule, classDeclaration.Identifier.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private static bool HasBaseType(INamedTypeSymbol symbol, string baseTypeName)
        {
            if (symbol == null)
            {
                return false;
            }

            var baseType = symbol.BaseType;
            while (baseType != null)
            {
                if (baseType.Name.Equals(baseTypeName))
                {
                    return true;
                }

                baseType = baseType.BaseType;
            }

            return false;
        }
    }
}
