using Analyzer.Analyzer;
using Analyzer.Config;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer.CodeFix
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(GameServiceAttributeCodeFixProvider)), Shared]
    public class GameServiceAttributeCodeFixProvider : CodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticIds.GameServiceAttribute);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public async override Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();

            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var classDeclaration = root?.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();
            // 构造Code Action
            var action = CodeAction.Create(
                title: "Add GameServiceAttribute",
                createChangedDocument: c => AddGameServiceAttributeAsync(context.Document, classDeclaration, GameServiceAttributeAnalyzer.GameServiceAttributeName, c),
                equivalenceKey: nameof(GameServiceAttributeCodeFixProvider));

            // 注册codeFix Code Action
            context.RegisterCodeFix(action, diagnostic);
        }

        private async Task<Document> AddGameServiceAttributeAsync(Document document, ClassDeclarationSyntax classDeclaration, string attributeName, CancellationToken c)
        {
            // 如果attributeName以Attribute结尾, 则去掉Attribute
            if (attributeName.EndsWith("Attribute"))
            {
                attributeName = attributeName.Substring(0, attributeName.Length - 9);
            }

            // 构造新的Attribute,省略Attribute后缀, 添加GameServiceAttributeAnalyzer.GameServiceAttributeDefaultParam参数
            var attributeArgument = SyntaxFactory.ParseExpression(GameServiceAttributeAnalyzer.GameServiceAttributeDefaultParam);
            var attribute = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attributeName))
                .WithArgumentList(SyntaxFactory.AttributeArgumentList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.AttributeArgument(attributeArgument))));

            var attributes = classDeclaration.AttributeLists.Add(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(attribute)).NormalizeWhitespace());

            // 构造新的Class
            var newClassDeclaration = classDeclaration?.WithAttributeLists(attributes).WithAdditionalAnnotations(Formatter.Annotation);

            // 获取Document的SyntaxRoot
            var root = await document.GetSyntaxRootAsync(c);

            // 替换原有的Class
            var newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);

            // 返回新的Document
            return document.WithSyntaxRoot(newRoot);

        }
    }
}
