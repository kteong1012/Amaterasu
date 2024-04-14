using Analyzer.Config;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer.CodeFix
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ForceBraceCodeFixProvider)), Shared]
    public class NamingCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get
            {
                return ImmutableArray.Create(
                    DiagnosticRules.NamingRule.PrivateFieldMemberRule.Id,
                    DiagnosticRules.NamingRule.PublicFieldMemberRule.Id,
                    DiagnosticRules.NamingRule.PropertyMemberRule.Id,
                    DiagnosticRules.NamingRule.PropertyAccessRule.Id
                    );
            }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public async override Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();

            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var variableDeclaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<VariableDeclarationSyntax>().FirstOrDefault();

            var propertyDeclaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<PropertyDeclarationSyntax>().FirstOrDefault();

            if (variableDeclaration == null && propertyDeclaration == null)
            {
                return;
            }

            switch (diagnostic.Id)
            {
                case DiagnosticIds.PublicFieldMember:
                    context.RegisterCodeFix(CodeAction.Create("Change variable name", c => ChangeToCamelCaseAsync(context.Document, variableDeclaration, c), nameof(NamingCodeFixProvider)), diagnostic);
                    break;
                case DiagnosticIds.PrivateFieldMember:
                    context.RegisterCodeFix(CodeAction.Create("Change variable name", c => ChangeToCamelCaseWithUnderscoreAsync(context.Document, variableDeclaration, c), nameof(NamingCodeFixProvider)), diagnostic);
                    break;
                case DiagnosticIds.PropertyMember:
                    context.RegisterCodeFix(CodeAction.Create("Change variable name", c => ChangeToPascalAsync(context.Document, propertyDeclaration, c), nameof(NamingCodeFixProvider)), diagnostic);
                    break;
                case DiagnosticIds.PropertyAccess:
                    context.RegisterCodeFix(CodeAction.Create("Change to public", c => ChangeToPublicAysnc(context.Document, propertyDeclaration, c), nameof(NamingCodeFixProvider)), diagnostic);
                    break;
            }


        }

        private Task<Document> ChangeToCamelCaseAsync(Document document, VariableDeclarationSyntax variableDeclaration, CancellationToken c)
        {
            var variableName = variableDeclaration.Variables.First().Identifier.Text;
            var newVariableName = char.ToLower(variableName[0]) + variableName.Substring(1);
            var newVariable = variableDeclaration.Variables.First().WithIdentifier(SyntaxFactory.Identifier(newVariableName));
            var newVariableDeclaration = variableDeclaration.WithVariables(SyntaxFactory.SingletonSeparatedList(newVariable));
            var root = document.GetSyntaxRootAsync(c).Result;
            var newRoot = root.ReplaceNode(variableDeclaration, newVariableDeclaration);
            return Task.FromResult(document.WithSyntaxRoot(newRoot));
        }

        private Task<Document> ChangeToCamelCaseWithUnderscoreAsync(Document document, VariableDeclarationSyntax variableDeclaration, CancellationToken c)
        {
            var variableName = variableDeclaration.Variables.First().Identifier.Text;
            var newVariableName = "_" + char.ToLower(variableName[0]) + variableName.Substring(1);
            var newVariable = variableDeclaration.Variables.First().WithIdentifier(SyntaxFactory.Identifier(newVariableName));
            var newVariableDeclaration = variableDeclaration.WithVariables(SyntaxFactory.SingletonSeparatedList(newVariable));
            var root = document.GetSyntaxRootAsync(c).Result;
            var newRoot = root.ReplaceNode(variableDeclaration, newVariableDeclaration);
            return Task.FromResult(document.WithSyntaxRoot(newRoot));
        }

        private Task<Document> ChangeToPascalAsync(Document document, PropertyDeclarationSyntax propertyDeclaration, CancellationToken c)
        {
            var name = propertyDeclaration.Identifier.Text;
            var newName = char.ToUpper(name[0]) + name.Substring(1);
            var newPropertyDeclaration = propertyDeclaration.WithIdentifier(SyntaxFactory.Identifier(newName));
            var root = document.GetSyntaxRootAsync(c).Result;
            var newRoot = root.ReplaceNode(propertyDeclaration, newPropertyDeclaration);
            return Task.FromResult(document.WithSyntaxRoot(newRoot));
        }

        private Task<Document> ChangeToPublicAysnc(Document document, PropertyDeclarationSyntax propertyDeclaration, CancellationToken c)
        {
            var newPropertyDeclaration = propertyDeclaration.WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
            var root = document.GetSyntaxRootAsync(c).Result;
            var newRoot = root.ReplaceNode(propertyDeclaration, newPropertyDeclaration);
            return Task.FromResult(document.WithSyntaxRoot(newRoot));
        }
    }
}
