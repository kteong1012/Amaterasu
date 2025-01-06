using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace CSharpTranslatePicker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CSharpTranslatePicker <slnPath> <outputFile>");
                return;
            }
            var path = args[0];
            var output = args[1];
            await Analyze(path, output);
        }

        private async static Task Analyze(string slnPath, string outputFile)
        {
            Console.WriteLine("正在打开解决方案");
            MSBuildLocator.RegisterDefaults();
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(slnPath);

            var translateCalls = new HashSet<string>();

            foreach (var project in solution.Projects)
            {
                if (project.Name != "HotUpdate")
                {
                    continue;
                }
                foreach (var document in project.Documents)
                {
                    var root = await document.GetSyntaxRootAsync();
                    var semanticModel = await document.GetSemanticModelAsync();

                    var invocations = root.DescendantNodes().OfType<InvocationExpressionSyntax>()
                        .Where(invocation => IsTranslateMethod(invocation, semanticModel));

                    foreach (var invocation in invocations)
                    {
                        // 如果该分析语句是在 Game.Cfg 命名空间下，则跳过
                        var namespaceDeclaration = invocation.FirstAncestorOrSelf<NamespaceDeclarationSyntax>();
                        if (namespaceDeclaration != null && namespaceDeclaration.Name.ToString() == "Game.Cfg")
                        {
                            continue;
                        }

                        Console.WriteLine($"分析语句 {invocation.ToFullString()}");

                        // 如果是常量字符串调用 Translate 扩展方法，则提取，
                        // 扩展方法声明： public static string Translate(this string text)
                        // 代码进入这里时， invocation.ToFullString() = "\"testConst1\".Translate()"
                        if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)

                        {
                            var caller = memberAccess.Expression;
                            var constantValue = semanticModel.GetConstantValue(caller);
                            if (constantValue.HasValue && constantValue.Value is string text)
                            {
                                translateCalls.Add(text);
                                continue;
                            }
                        }

                        // 如果是用正常的静态方法调用 Translate 扩展方法，则提取第一个参数的常量
                        // 扩展方法声明： public static string Translate(this string text)
                        // 代码进入这里时， invocation.ToFullString() = "            Game.StringExtensions.Translate(testConst2)"
                        // 如果是静态方法调用
                        if (semanticModel.GetSymbolInfo(invocation.Expression).Symbol is IMethodSymbol methodSymbol && methodSymbol.IsStatic)
                        {
                            var arguments = invocation.ArgumentList.Arguments;
                            if (arguments.Count > 0)
                            {
                                var argument = arguments[0];
                                var constantValue = semanticModel.GetConstantValue(argument.Expression);
                                if (constantValue.HasValue && constantValue.Value is string text)
                                {
                                    translateCalls.Add(text);
                                    continue;
                                }
                            }
                        }

                    }
                }
            }

            File.WriteAllLines(outputFile, translateCalls);
        }

        private static bool IsTranslateMethod(InvocationExpressionSyntax invocation, SemanticModel semanticModel)
        {
            var symbolInfo = semanticModel.GetSymbolInfo(invocation);
            var methodSymbol = symbolInfo.Symbol as IMethodSymbol;

            if (methodSymbol == null)
            {
                return false;
            }

            // 检查方法名称和包含类型
            if (methodSymbol.Name != "Translate" || methodSymbol.ContainingType.ToString() != "Game.StringExtensions")
            {
                return false;
            }

            // 检查是否为扩展方法
            if (!methodSymbol.IsExtensionMethod)
            {
                return false;
            }

            // 检查第一个参数是否为 string 类型
            var firstParameter = methodSymbol.Parameters.FirstOrDefault();
            if (firstParameter == null || firstParameter.Type.SpecialType != SpecialType.System_String)
            {
                return false;
            }

            return true;
        }
    }
}
