using Analyzer.Extension;
using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class GameServiceGenerator : ISourceGenerator
    {
        private const string _gameServiceBaseType = "Game.GameService";
        private const string _gameServiceAttributeType = "Game.GameServiceAttribute";
        private const string _domainEnumType = "Game.GameServiceDomain";
        private const string _invalidDomain = "Game.GameServiceDomain.None";

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => GameServiceSyntaxContextReceiver.Create());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {

                if (context.SyntaxContextReceiver == null)
                {
                    Print("context.SyntaxContextReceiver == null");
                    return;
                }
                if (context.SyntaxContextReceiver is not GameServiceSyntaxContextReceiver receiver)
                {
                    Print("context.SyntaxContextReceiver is not GameServiceSyntaxContextReceiver receiver");
                    return;
                }
                if (receiver.names.Count == 0)
                {
                    Print("receiver.names.Count == 0");
                    return;
                }

                Print("receiver.names.Count: " + receiver.names.Count);

                foreach (var (nameSpace, className, domain) in receiver.names)
                {
                    var sb = new IntentStringBuilder(0);
                    sb.AppendLine($"using Cysharp.Threading.Tasks;");
                    sb.AppendLine($"using Game.Log;");
                    sb.AppendLine($"using System;");
                    sb.ApplineEmptyLine();
                    if (!string.IsNullOrEmpty(nameSpace))
                    {
                        sb.AppendLine($"namespace {nameSpace}");
                    }
                    sb.AppendLine("{");
                    sb++;
                    sb.AppendLine($"public partial class {className}");
                    sb.AppendLine("{");
                    sb++;
                    sb.AppendLine($"public static Game.GameServiceDomain __Domain => Game.GameServiceDomain.{domain};");
                    sb.AppendLine($"public Game.GameServiceDomain Domain => __Domain;");
                    sb.ApplineEmptyLine();
                    sb.AppendLine($"public UniTask __Init()");
                    sb.AppendLine("{");
                    sb++;
                    sb.AppendLine($"GameLog.Debug($\"{className}:__Init, Domain: {domain}\");");
                    sb.AppendLine($"return Awake();");
                    sb--;
                    sb.AppendLine("}");
                    sb.ApplineEmptyLine();
                    sb.AppendLine($"public UniTask __PostInit()");
                    sb.AppendLine("{");
                    sb++;
                    sb.AppendLine($"GameLog.Debug($\"{className}:__PostInit, Domain: {domain}\");");
                    sb.AppendLine($"return Start();");
                    sb--;
                    sb.AppendLine("}");
                    sb.ApplineEmptyLine();
                    sb.AppendLine($"public void __Dispose()");
                    sb.AppendLine("{");
                    sb++;
                    sb.AppendLine($"GameLog.Debug($\"{className}:__Dispose, Domain: {domain}\");");
                    sb.AppendLine($"OnDestroy();");
                    sb--;
                    sb.AppendLine("}");
                    sb--;
                    sb.AppendLine("}");
                    sb--;
                    sb.AppendLine("}");

                    var source = sb.ToString();
                    Print("source\n" + source);
                    var test = false;
                    if (test)
                    {
                        var folder = "E:\\OpenProjects\\Amaterasu\\Assets\\GameScript\\HotUpdate\\Services";
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        var path = Path.Combine(folder, $"{className}.g.cs");
                        File.WriteAllText(path, source);
                    }
                    else
                    {
                        context.AddSource($"{className}.g.cs", source);
                        Print("AddSource: " + className);
                    }
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
                var stackTrace = e.StackTrace;
                Print(message + "\n" + stackTrace);

                throw;
            }
        }


        public static void Print(string message)
        {
            var logPath = "D:\\log.log";
            File.AppendAllText(logPath, message + "\n");
        }

        class GameServiceSyntaxContextReceiver : ISyntaxContextReceiver
        {

            public static GameServiceSyntaxContextReceiver Create()
            {
                return new GameServiceSyntaxContextReceiver();
            }

            public List<(string nameSpace, string className, string domain)> names = new List<(string nameSpace, string className, string domain)>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                try
                {
                    if (!AnalyzerHelper.IsAssemblyNeedAnalyze(context.SemanticModel.Compilation.AssemblyName, Definitions.TargetAssemblyName))
                    {
                        return;
                    }

                    if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax)
                    {
                        return;
                    }


                    var classTypeSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
                    if (classTypeSymbol == null)
                    {
                        return;
                    }

                    var baseType = classTypeSymbol.BaseType?.ToString();

                    // 筛选出实体类
                    if (baseType != _gameServiceBaseType)
                    {
                        return;
                    }

                    Print("==========" + classTypeSymbol.ToString());


                    if (!classTypeSymbol.HasAttribute(_gameServiceAttributeType))
                    {
                        return;
                    }

                    var attrTypeNameWithoutNamespace = _gameServiceAttributeType.Split('.').Last();
                    var attributeData = classTypeSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.Name.Equals(attrTypeNameWithoutNamespace));

                    // 枚举值
                    var domain = attributeData.ConstructorArguments.FirstOrDefault().ToCSharpString();
                    domain = domain.Split('.').Last();

                    if (domain == _invalidDomain)
                    {
                        return;
                    }

                    var nameSpace = classTypeSymbol.ContainingNamespace.ToString();
                    var className = classTypeSymbol.Name;

                    if (names.Any(x => nameSpace == x.nameSpace && className == x.className))
                    {
                        return;
                    }
                    names.Add((nameSpace, className, domain));
                    Print("成功  " + nameSpace + " " + className + " " + domain);
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    var stackTrace = e.StackTrace;
                    Print(message + "\n" + stackTrace);
                    throw;
                }
            }
        }
    }
}
