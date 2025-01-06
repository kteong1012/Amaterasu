//using Analyzer.Config;
//using Analyzer.Extension;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Diagnostics;
//using System.Collections.Immutable;
//using System.Linq;

//namespace Analyzer.Analyzer
//{
//    [DiagnosticAnalyzer(LanguageNames.CSharp)]
//    public class DontUseMaigcNumberAnalyzer : DiagnosticAnalyzer
//    {
//        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.DontUseMagicNumberRule.Rule);

//        public sealed override void Initialize(AnalysisContext context)
//        {
//            context.EnableConcurrentExecution();
//            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

//            context.RegisterCompilationStartAction(analysisContext =>
//            {
//                if (AnalyzerHelper.IsAssemblyNeedAnalyze(analysisContext.Compilation.AssemblyName, Definitions.TargetAssemblyName))
//                {
//                    analysisContext.RegisterSemanticModelAction(AnalyzeAction);
//                }
//            });
//        }

//        private void AnalyzeAction(SemanticModelAnalysisContext context)
//        {
//            // 检查对象： 表达式语句，赋值语句，返回语句，if语句，switch语句，while语句，do-while语句，for语句，foreach语句，case语句，default语句
//            // 如果在方法体里出现非 -1,0,1 的魔法数字，就报错
//            var root = context.SemanticModel.SyntaxTree.GetRoot();
//            var methodBodys = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

//            foreach (var methodBody in methodBodys)
//            {
//                var statements = methodBody.DescendantNodes().OfType<StatementSyntax>();
//                foreach (var statement in statements)
//                {
//                    if (statement is ExpressionStatementSyntax expressionStatement)
//                    {
//                        CheckMagicNumber(context, expressionStatement.Expression);
//                    }
//                    else if (statement is LocalDeclarationStatementSyntax localDeclarationStatement)
//                    {
//                        CheckMagicNumber(context, localDeclarationStatement.Declaration);
//                    }
//                    else if (statement is ReturnStatementSyntax returnStatement)
//                    {
//                        CheckMagicNumber(context, returnStatement.Expression);
//                    }
//                    else if (statement is IfStatementSyntax ifStatement)
//                    {
//                        CheckMagicNumber(context, ifStatement.Condition);
//                    }
//                    else if (statement is SwitchStatementSyntax switchStatement)
//                    {
//                        CheckMagicNumber(context, switchStatement.Expression);
//                    }
//                    else if (statement is WhileStatementSyntax whileStatement)
//                    {
//                        CheckMagicNumber(context, whileStatement.Condition);
//                    }
//                    else if (statement is DoStatementSyntax doStatement)
//                    {
//                        CheckMagicNumber(context, doStatement.Condition);
//                    }
//                    else if (statement is ForStatementSyntax forStatement)
//                    {
//                        CheckMagicNumber(context, forStatement.Condition);
//                    }
//                    else if (statement is ForEachStatementSyntax forEachStatement)
//                    {
//                        CheckMagicNumber(context, forEachStatement.Expression);
//                    }
//                }

//                var switchSections = methodBody.DescendantNodes().OfType<SwitchSectionSyntax>();
//                foreach (var switchSection in switchSections)
//                {
//                    foreach (var label in switchSection.Labels)
//                    {
//                        if (label is CaseSwitchLabelSyntax caseSwitchLabel)
//                        {
//                            CheckMagicNumber(context, caseSwitchLabel.Value);
//                        }
//                    }
//                }

//                var defaultLabels = methodBody.DescendantNodes().OfType<DefaultSwitchLabelSyntax>();
//                foreach (var defaultLabel in defaultLabels)
//                {
//                    CheckMagicNumber(context, defaultLabel);
//                }
//            }
//        }

//        private void CheckMagicNumber(SemanticModelAnalysisContext context, SyntaxNode node)
//        {
//            if (node == null)
//            {
//                return;
//            }

//            var expressions = node.DescendantNodes().OfType<LiteralExpressionSyntax>();
//            foreach (var expression in expressions)
//            {
//                if (!expression.IsKind(SyntaxKind.NumericLiteralExpression))
//                {
//                    continue;
//                }

//                var number = expression.Token.ValueText;
//                if (!Definitions.MagicNumbers.Contains(number))
//                {
//                    var diagnostic = Diagnostic.Create(DiagnosticRules.DontUseMagicNumberRule.Rule, expression.GetLocation(), number);
//                    context.ReportDiagnostic(diagnostic);
//                }
//            }
//        }
//    }
//}
