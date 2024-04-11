using Analyzer.Config;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer.CodeFix
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ForceBraceCodeFixProvider)), Shared]
    public class ForceBraceCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticIds.ForceBrace);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public async override Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();

            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var ifStatement = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<IfStatementSyntax>().FirstOrDefault();

            if (ifStatement == null)
            {
                return;
            }

            var codeAction = CodeAction.Create("Add braces", c => AddBracesAsync(context.Document, ifStatement, c), nameof(ForceBraceCodeFixProvider));

            context.RegisterCodeFix(codeAction, diagnostic);
        }

        private Task<Document> AddBracesAsync(Document document, IfStatementSyntax ifStatement, CancellationToken c)
        {
            var newIfStatement = ifStatement.WithStatement(SyntaxFactory.Block(ifStatement.Statement));

            var root = document.GetSyntaxRootAsync(c).Result;

            var newRoot = root.ReplaceNode(ifStatement, newIfStatement);

            return Task.FromResult(document.WithSyntaxRoot(newRoot));
        }
    }
}
