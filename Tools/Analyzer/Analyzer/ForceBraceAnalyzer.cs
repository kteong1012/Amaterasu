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
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => throw new NotImplementedException();

        public override void Initialize(AnalysisContext context)
        {

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxTreeAction(AnalyzeSymbol);
        }

        private static void AnalyzeSymbol(SyntaxTreeAnalysisContext context)
        {
            var root = context.Tree.GetRoot(context.CancellationToken);
            foreach (var ifstatement in root.DescendantNodes().OfType<IfStatementSyntax>())
            {
                var expressStatements = ifstatement.DescendantNodes().OfType<ExpressionStatementSyntax>().ToList();
                foreach (var estate in expressStatements)
                {
                    if (!(estate.Parent is BlockSyntax))
                    {
                        var diagnostic = Diagnostic.Create(DiagnosticRules.ForceBraceRule.Rule, estate.GetFirstToken().GetLocation());
                        context.ReportDiagnostic(diagnostic);
                    }
                }

            }
        }
    }
}
