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

namespace Analyzer.src.CodeFix
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(DontInvokeRoleDataObjectConstructorCodeFixProvider)), Shared]
    public class DontInvokeRoleDataObjectConstructorCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticIds.DontInvokeRoleDataObjectConstructor);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public async override Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();

            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var objectCreationExpression = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ObjectCreationExpressionSyntax>().FirstOrDefault();

            if (objectCreationExpression == null)
            {
                return;
            }

            var codeAction = CodeAction.Create("使用 RoleDataObjectCreator 创建", c => UseRoleDataObjectCreateAsync(context.Document, objectCreationExpression, c), nameof(DontInvokeRoleDataObjectConstructorCodeFixProvider));

            context.RegisterCodeFix(codeAction, diagnostic);
        }

        // 使用 RoleDataObjectCreator<{ClassName}>.Create() 代替 new {ClassName}()
        public async Task<Document> UseRoleDataObjectCreateAsync(Document document, ObjectCreationExpressionSyntax objectCreationExpression, CancellationToken c)
        {
            if (objectCreationExpression == null)
            {
                return document;
            }

            var className = objectCreationExpression.Type.ToString();
            var newExpression = SyntaxFactory.ParseExpression($"RoleDataObjectCreator<{className}>.Create()");

            // 替换原始的 ObjectCreationExpression
            var newRoot = await document.GetSyntaxRootAsync(c);
            newRoot = newRoot.ReplaceNode(objectCreationExpression, newExpression);

            return document.WithSyntaxRoot(newRoot);
        }
    }
}
