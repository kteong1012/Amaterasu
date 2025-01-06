using Analyzer.Config;
using Analyzer.Extension;
using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace Analyzer.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DontInvokeRoleDataObjectConstructorAnalyzer : DiagnosticAnalyzer
    {
        // 如果调用的方法所在的类型是在 HotUpdate_Nino 命名空间下，不会报错
        private const string _ninoNamespace = "HotUpdate_Nino";
        private const string _roleDataObject = "RoleDataObject";

        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.DontInvokeRoleDataObjectConstructorRule.Rule);

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
            // 搜索所有 ObjectCreationExpressionSyntax
            var objectCreationExpressions = context.SemanticModel.SyntaxTree.GetRoot().DescendantNodes().OfType<ObjectCreationExpressionSyntax>();

            foreach (var objectCreationExpression in objectCreationExpressions)
            {
                var typeSymbol = context.SemanticModel.GetTypeInfo(objectCreationExpression).Type as INamedTypeSymbol;
                if (typeSymbol == null)
                {
                    continue;
                }
                if (!typeSymbol.HasBaseType(_roleDataObject))
                {
                    continue;
                }
                // 获取表达式所在的方法的类的命名空间
                var methodDeclaration = objectCreationExpression.FirstAncestorOrSelf<MethodDeclarationSyntax>();
                if (methodDeclaration == null)
                {
                    continue;
                }
                var classDeclaration = methodDeclaration.FirstAncestorOrSelf<ClassDeclarationSyntax>();
                if (classDeclaration == null)
                {
                    continue;
                }
                var namespaceDeclaration = classDeclaration.FirstAncestorOrSelf<NamespaceDeclarationSyntax>();
                if (namespaceDeclaration == null)
                {
                    continue;
                }
                var namespaceName = namespaceDeclaration.Name.ToString();
                if (namespaceName == _ninoNamespace)
                {
                    continue;
                }
                var className = typeSymbol.Name;
                var diagnostic = Diagnostic.Create(DiagnosticRules.DontInvokeRoleDataObjectConstructorRule.Rule, objectCreationExpression.GetLocation(), className);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
