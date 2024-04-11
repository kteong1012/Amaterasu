using Analyzer.Config;
using Analyzer.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
    public class ForceBraceAnalyzer : DiagnosticAnalyzer
    {
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.ForceBraceRule.Rule);

        public sealed override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.RegisterCompilationStartAction(analysisContext =>
            {
                if (AnalyzerHelper.IsAssemblyNeedAnalyze(analysisContext.Compilation.AssemblyName, Definitions.TargetAssemblyName))
                {
                    analysisContext.RegisterSyntaxTreeAction(AnalyzeAction);
                }
            });

        }

        private static void AnalyzeAction(SyntaxTreeAnalysisContext context)
        {
            var root = context.Tree.GetRoot(context.CancellationToken);
            foreach (var ifstatement in root.DescendantNodes().OfType<IfStatementSyntax>())
            {
                var expressStatements = ifstatement.DescendantNodes().OfType<ExpressionStatementSyntax>().ToList();
                foreach (var estate in expressStatements)
                {
                    if (!(estate.Parent is BlockSyntax))
                    {
                        // whole line
                        var location = estate.GetLocation();
                        var diagnostic = Diagnostic.Create(DiagnosticRules.ForceBraceRule.Rule, location);
                        context.ReportDiagnostic(diagnostic);
                    }
                }

            }
        }
    }
}
