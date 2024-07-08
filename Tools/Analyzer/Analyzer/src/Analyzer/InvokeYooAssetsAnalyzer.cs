using Analyzer.Config;
using Analyzer.Extension;
using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Analyzer.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class InvokeYooAssetsAnalyzer : DiagnosticAnalyzer
    {
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.InvokeYooAssetsRule.Rule);

        public sealed override void Initialize(AnalysisContext context)
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
            var invocationExpressions = context.SemanticModel.SyntaxTree.GetRoot().DescendantNodes<InvocationExpressionSyntax>();

            foreach (var invocationExpression in invocationExpressions)
            {
                var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression).Symbol as IMethodSymbol;
                if (methodSymbol == null)
                {
                    continue;
                }

                if (methodSymbol.ContainingType.Name.Equals("YooAssets"))
                {
                    var location = invocationExpression.GetLocation();
                    var diagnostic = Diagnostic.Create(DiagnosticRules.InvokeYooAssetsRule.Rule, location);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
