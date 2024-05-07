//using Analyzer.Config;
//using Analyzer.Extension;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Diagnostics;
//using System;
//using System.Collections.Immutable;
//using System.Linq;

//namespace Analyzer.Analyzer
//{
//    [DiagnosticAnalyzer(LanguageNames.CSharp)]
//    public class NamingAnalyzer : DiagnosticAnalyzer
//    {
//        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
//        {
//            get
//            {
//                return ImmutableArray.Create(
//                    DiagnosticRules.NamingRule.PrivateFieldMemberRule,
//                    DiagnosticRules.NamingRule.PublicFieldMemberRule,
//                    DiagnosticRules.NamingRule.PropertyMemberRule,
//                    DiagnosticRules.NamingRule.PropertyAccessRule
//                    );
//            }
//        }

//        public sealed override void Initialize(AnalysisContext context)
//        {
//            context.EnableConcurrentExecution();
//            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

//            context.RegisterCompilationStartAction(analysisContext =>
//            {
//                if (AnalyzerHelper.IsAssemblyNeedAnalyze(analysisContext.Compilation.AssemblyName, Definitions.TargetAssemblyName))
//                {
//                    analysisContext.RegisterSyntaxTreeAction(AnalyzeAction);
//                }
//            });
//        }

//        private void AnalyzeAction(SyntaxTreeAnalysisContext context)
//        {
//            var root = context.Tree.GetRoot(context.CancellationToken);
//            foreach (var fieldDeclaration in root.DescendantNodes().OfType<FieldDeclarationSyntax>())
//            {
//                if (fieldDeclaration.Parent is not ClassDeclarationSyntax)
//                {
//                    continue;
//                }
//                if (fieldDeclaration.Modifiers.Any(SyntaxKind.PublicKeyword))
//                {
//                    foreach (var variable in fieldDeclaration.Declaration.Variables)
//                    {
//                        var variableName = variable.Identifier.Text;
//                        if (!char.IsLower(variableName[0]))
//                        {
//                            var location = variable.GetLocation();
//                            var diagnostic = Diagnostic.Create(DiagnosticRules.NamingRule.PublicFieldMemberRule, location, variableName, char.ToLower(variableName[0]) + variableName.Substring(1));
//                            context.ReportDiagnostic(diagnostic);
//                        }
//                    }
//                }
//                else
//                {
//                    foreach (var variable in fieldDeclaration.Declaration.Variables)
//                    {
//                        var variableName = variable.Identifier.Text;
//                        if (!variableName.StartsWith("_") || !char.IsLower(variableName[1]))
//                        {
//                            var location = variable.GetLocation();
//                            var diagnostic = Diagnostic.Create(DiagnosticRules.NamingRule.PrivateFieldMemberRule, location, variableName, "_" + char.ToLower(variableName[0]) + variableName.Substring(1));
//                            context.ReportDiagnostic(diagnostic);
//                        }
//                    }
//                }
//            }

//            foreach (var propertyDeclaration in root.DescendantNodes().OfType<PropertyDeclarationSyntax>())
//            {
//                if (propertyDeclaration.Parent is not ClassDeclarationSyntax)
//                {
//                    continue;
//                }
//                if (propertyDeclaration.Modifiers.Any(SyntaxKind.PublicKeyword))
//                {
//                    var propertyName = propertyDeclaration.Identifier.Text;
//                    if (!char.IsUpper(propertyName[0]))
//                    {
//                        var location = propertyDeclaration.Identifier.GetLocation();
//                        var diagnostic = Diagnostic.Create(DiagnosticRules.NamingRule.PropertyMemberRule, location, propertyName, char.ToUpper(propertyName[0]) + propertyName.Substring(1));
//                        context.ReportDiagnostic(diagnostic);
//                    }
//                }
//                else
//                {
//                    var location = propertyDeclaration.GetLocation();
//                    var diagnostic = Diagnostic.Create(DiagnosticRules.NamingRule.PropertyAccessRule, location, propertyDeclaration.Identifier.Text);
//                    context.ReportDiagnostic(diagnostic);
//                }
//            }
//        }
//    }
//}
