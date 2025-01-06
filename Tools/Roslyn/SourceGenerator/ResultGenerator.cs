//using Analyzer.src.Extension;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace SourceGenerator
//{
//    [Generator(LanguageNames.CSharp)]
//    public class ResultGenerator : ISourceGenerator
//    {
//        private const string _resultExecutorBaseType = "ResultExecutor";
//        public void Initialize(GeneratorInitializationContext context)
//        {
//            context.RegisterForSyntaxNotifications(() => new ResultSyntaxReceiver());
//        }
//        public void Execute(GeneratorExecutionContext context)
//        {
//            if (context.SyntaxContextReceiver == null)
//            {
//                return;
//            }
//            if (context.SyntaxContextReceiver is not ResultSyntaxReceiver receiver)
//            {
//                return;
//            }

//            var sb = new IntentStringBuilder(0);
//            sb.AppendLine("using System;");
//            sb.AppendLine("using System.Collections.Generic;");
//            sb.AppendLine("using Cysharp.Threading.Tasks;");
//            sb.AppendLine("using Game.Cfg;");
//            sb.AppendLine("using Game.Cfg.Result;");
//            sb.ApplineEmptyLine();
//            sb.AppendLine("namespace Game");
//            sb.AppendLine("{");//namespace start
//            sb++;
//            sb.AppendLine("public static class ResultExecutorHandler");
//            sb.AppendLine("{");//class start
//            sb++;
//            sb.AppendLine("public static async UniTask Execute(Game.CharacterData caster, List<Game.CharacterData> targets, Game.Cfg.ResultConfig result, params object[] args)");
//            sb.AppendLine("{");//method start Execute
//            sb++;
//            sb.AppendLine("switch (result.ResultParam.GetTypeId())");
//            sb.AppendLine("{");//switch start
//            sb++;
//            foreach (var info in receiver.infos)
//            {
//                sb.AppendLine($"case {info.targetClassName}.__ID__:");
//                sb.AppendLine("{");//case start
//                sb++;
//                sb.AppendLine($"var resultParam = result.ResultParam as {info.targetClassName};");
//                sb.AppendLine($"var executor = new {info.className}();");
//                sb.AppendLine("await executor.Execute(caster, targets, result, resultParam, args);");
//                sb.AppendLine("break;");
//                sb--;
//                sb.AppendLine("}");//case end
//            }
//            sb.AppendLine("default:");
//            sb++;
//            sb.AppendLine("break;");
//            sb--;
//            sb.AppendLine("}");//switch end
//            sb--;
//            sb.AppendLine("}");//method end Execute
//            sb--;
//            sb.AppendLine("}");//class end
//            sb--;
//            sb.AppendLine("}");//namespace end

//            context.AddSourceOrWriteFile("ResultExecutorHandler.g.cs", sb.ToString());
//        }


//        class ResultSyntaxReceiver : ISyntaxContextReceiver
//        {
//            public readonly List<Info> infos = new List<Info>();
//            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
//            {
//                // 扫描目标： 继承自 ResultExecutor<T> 的非抽象类。Info.className是这个类的全名，targetClassName是泛型参数类的全名
//                if (!AnalyzerHelper.IsAssemblyNeedAnalyze(context.SemanticModel.Compilation.AssemblyName, Definitions.TargetAssemblyName))
//                {
//                    return;
//                }

//                if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax)
//                {
//                    return;
//                }

//                var classTypeSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
//                if (classTypeSymbol == null)
//                {
//                    return;
//                }
//                if (classTypeSymbol.IsAbstract)
//                {
//                    return;
//                }

//                // 继承自 ResultExecutor<T>
//                if (!classTypeSymbol.HasBaseType(_resultExecutorBaseType))
//                {
//                    return;
//                }

//                var typeArgument = classTypeSymbol.BaseType.TypeArguments.FirstOrDefault();
//                if (typeArgument == null)
//                {
//                    return;
//                }

//                var fullName = GetFullName(classTypeSymbol);
//                var targetFullName = GetFullName(typeArgument);
//                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(targetFullName))
//                {
//                    return;
//                }

//                if (infos.Any(x => x.className == fullName))
//                {
//                    return;
//                }

//                infos.Add(new Info(fullName, targetFullName));
//            }
//        }

//        private static string GetFullName(ITypeSymbol symbol)
//        {
//            if (symbol.ContainingNamespace.IsGlobalNamespace)
//            {
//                return symbol.Name;
//            }
//            else
//            {
//                return $"{symbol.ContainingNamespace}.{symbol.Name}";
//            }
//        }

//        class Info
//        {
//            public string className;
//            public string targetClassName;

//            public Info(string className, string id)
//            {
//                this.className = className;
//                this.targetClassName = id;
//            }
//        }
//    }
//}
