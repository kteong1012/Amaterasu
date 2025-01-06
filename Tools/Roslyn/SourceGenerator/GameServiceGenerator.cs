using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class GameServiceGenerator : ISourceGenerator
    {
        private const string _gameServiceBaseType = "GameService";
        private const string _gameServiceAttributeType = "Game.GameServiceAttribute";
        private const string _domainEnumType = "Game.GameServiceDomain";
        private const string _invalidDomain = "Game.GameServiceDomain.None";

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => GameServiceSyntaxContextReceiver.Create());
        }

        public void Execute(GeneratorExecutionContext context)
        {

            if (context.SyntaxContextReceiver == null)
            {
                return;
            }
            if (context.SyntaxContextReceiver is not GameServiceSyntaxContextReceiver receiver)
            {
                return;
            }
            if (receiver.names.Count == 0)
            {
                return;
            }


            foreach (var (nameSpace, className, domain) in receiver.names)
            {
                var sb = new IntentStringBuilder(0);
                sb.AppendLine($"using Cysharp.Threading.Tasks;");
                sb.AppendLine($"using Game.Log;");
                sb.AppendLine($"using System;");
                sb.AppendEmptyLine();
                if (!string.IsNullOrEmpty(nameSpace))
                {
                    sb.AppendLine($"namespace {nameSpace}");
                    sb.AppendLine("{");
                    sb++;
                }
                sb.AppendLine($"public partial class {className}");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"public static Game.GameServiceDomain __Domain => Game.GameServiceDomain.{domain};");
                sb.AppendLine($"public Game.GameServiceDomain Domain => __Domain;");
                sb.AppendEmptyLine();
                sb.AppendLine($"public UniTask __Init()");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"GameLog.Debug($\"{className}:__Init, Domain: {domain}\");");
                sb.AppendLine($"return Awake();");
                sb--;
                sb.AppendLine("}");
                sb.AppendEmptyLine();
                sb.AppendLine($"public UniTask __PostInit()");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"GameLog.Debug($\"{className}:__PostInit, Domain: {domain}\");");
                sb.AppendLine($"return Start();");
                sb--;
                sb.AppendLine("}");
                sb.AppendEmptyLine();
                sb.AppendLine($"public void __Dispose()");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"GameLog.Debug($\"{className}:__Dispose, Domain: {domain}\");");
                sb.AppendLine($"RemoveAllListener();");
                sb.AppendLine($"OnDestroy();");
                sb--;
                sb.AppendLine("}");
                sb--;
                sb.AppendLine("}");
                if (!string.IsNullOrEmpty(nameSpace))
                {
                    sb--;
                    sb.AppendLine("}");
                }

                var source = sb.ToString();
                context.AddSourceOrWriteFile($"{className}.g.cs", source);
            }
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

                if (!classTypeSymbol.HasBaseType(_gameServiceBaseType))
                {
                    return;
                }

                if (classTypeSymbol.IsAbstract)
                {
                    return;
                }


                if (!classTypeSymbol.HasAttribute(_gameServiceAttributeType))
                {
                    return;
                }

                var attrTypeNameWithoutNamespace = _gameServiceAttributeType.Split('.').Last();
                var attributeData = classTypeSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.Name.Equals(attrTypeNameWithoutNamespace));

                if (attributeData == null)
                {
                    return;
                }

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
            }
        }
    }
}
