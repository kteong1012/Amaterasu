using Analyzer.Config;
using Analyzer.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Analyzer.Analyzer
{

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseGameLogAnalyzer : DiagnosticAnalyzer
    {
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.UseGameLogRule.Rule);

        private const string _debugClassNameSpace = "UnityEngine";
        private const string _debugClassName = "Debug";

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

                var isNameSpaceOfUnityEngine = methodSymbol.ContainingType.GetNameSpace() == _debugClassNameSpace;
                var isClassNameOfDebug = methodSymbol.ContainingType.Name.Equals(_debugClassName);

                if (isNameSpaceOfUnityEngine && isClassNameOfDebug)
                {
                    var location = invocationExpression.GetLocation();
                    var diagnostic = Diagnostic.Create(DiagnosticRules.UseGameLogRule.Rule, location);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
