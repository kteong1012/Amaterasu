using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class GameServicesGenerator : ISourceGenerator
    {
        private const string _gameServiceBaseType = "Game.GameService";
        private const string _gameServiceAttributeType = "Game.GameServiceAttribute";
        private const string _domainEnumType = "Game.GameServiceDomain";
        private const string _invalidDomain = "Game.GameServiceDomain.None";

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => GameServicesSyntaxContextReceiver.Create());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver == null)
            {
                return;
            }
            if (context.SyntaxContextReceiver is not GameServicesSyntaxContextReceiver receiver)
            {
                return;
            }
            if (receiver.serviceTypeNames.Count == 0)
            {
                return;
            }

            var sb = new IntentStringBuilder(0);
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine($"using Cysharp.Threading.Tasks;");
            sb.AppendLine("namespace Game");
            sb.AppendLine("{");
            sb++;
            sb.AppendLine("public static class SSS");
            sb.AppendLine("{");
            sb++;
            foreach (var kvp in receiver.serviceTypeNames)
            {
                var domain = kvp.Key;
                var serviceNames = kvp.Value;
                foreach (var (name, fullName) in serviceNames)
                {
                    var fieldName = "_" + name;
                    sb.AppendLine($"private static {fullName} {fieldName};");
                    sb.AppendLine($"public static {fullName} {name} => {fieldName} != null ? {fieldName} : throw new System.Exception(\"服务{fullName}未启动，Domain:{domain}\");");

                }
                sb.AppendLine($"public static List<GameService> {domain}DomainServices => new List<GameService>()");
                sb.AppendLine("{");
                sb++;
                foreach (var (name, fullName) in serviceNames)
                {
                    sb.AppendLine($"{name},");
                }
                sb--;
                sb.AppendLine("};");
                sb.AppendLine($"private static async UniTask Start{domain}Services()");
                sb.AppendLine("{");
                sb++;
                foreach (var (name, fullName) in serviceNames)
                {
                    var fieldName = "_" + name;
                    sb.AppendLine($"{fieldName} = new {fullName}();");
                    sb.AppendLine($"await {fieldName}.__Init();");
                }
                foreach (var (name, fullName) in serviceNames)
                {
                    var fieldName = "_" + name;
                    sb.AppendLine($"await {fieldName}.__PostInit();");
                }

                sb--;
                sb.AppendLine("}");
                sb.AppendLine($"private static void Stop{domain}Services()");
                sb.AppendLine("{");
                sb++;
                foreach (var (name, fullName) in serviceNames)
                {
                    var fieldName = "_" + name;
                    sb.AppendLine($"if ({fieldName} != null)");
                    sb.AppendLine($"{{");
                    sb++;
                    sb.AppendLine($"{fieldName}.__Dispose();");
                    sb.AppendLine($"{fieldName} = null;");
                    sb--;
                    sb.AppendLine($"}}");
                }
                sb--;
                sb.AppendLine("}");
            }


            sb.AppendLine("public static void Update()");
            sb.AppendLine("{");
            sb++;
            foreach (var kvp in receiver.serviceTypeNames)
            {
                var serviceNames = kvp.Value;
                foreach (var (name, fullName) in serviceNames)
                {
                    var fieldName = "_" + name;
                    sb.AppendLine($"{fieldName}?.Update();");
                }
            }
            sb--;
            sb.AppendLine("}");



            sb.AppendLine($"public static async UniTask StartServices(Game.GameServiceDomain domain)");
            sb.AppendLine("{");
            sb++;
            sb.AppendLine("switch (domain)");
            sb.AppendLine("{");
            sb++;
            foreach (var kvp in receiver.serviceTypeNames)
            {
                var domain = kvp.Key;
                var serviceNames = kvp.Value;
                sb.AppendLine($"case Game.GameServiceDomain.{domain}:");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"await Start{domain}Services();");
                sb.AppendLine("break;");
                sb--;
                sb.AppendLine("}");
            }
            sb--;
            sb.AppendLine("}");
            sb--;
            sb.AppendLine("}");

            sb.AppendLine($"public static void StopServices(Game.GameServiceDomain domain)");
            sb.AppendLine("{");
            sb++;
            sb.AppendLine("switch (domain)");
            sb.AppendLine("{");
            sb++;
            foreach (var kvp in receiver.serviceTypeNames)
            {
                var domain = kvp.Key;
                var serviceNames = kvp.Value;
                sb.AppendLine($"case Game.GameServiceDomain.{domain}:");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"Stop{domain}Services();");
                sb.AppendLine("break;");
                sb--;
                sb.AppendLine("}");
            }
            sb--;
            sb.AppendLine("}");
            sb--;
            sb.AppendLine("}");


            sb.AppendLine("public static void StopAll()");
            sb.AppendLine("{");
            sb++;
            foreach (var kvp in receiver.serviceTypeNames)
            {
                var domain = kvp.Key;
                sb.AppendLine($"Stop{domain}Services();");
            }
            sb--;
            sb.AppendLine("}");

            sb--;
            sb.AppendLine("}");
            sb--;
            sb.AppendLine("}");

            context.AddSource("GameServices.g.cs", sb.ToString());
        }


        class GameServicesSyntaxContextReceiver : ISyntaxContextReceiver
        {

            public static GameServicesSyntaxContextReceiver Create()
            {
                return new GameServicesSyntaxContextReceiver();
            }

            public Dictionary<string, HashSet<(string fullName, string name)>> serviceTypeNames = new Dictionary<string, HashSet<(string fullName, string name)>>();

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

                var baseType = classTypeSymbol.BaseType?.ToString();

                // 筛选出实体类
                if (baseType != _gameServiceBaseType)
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

                if (!serviceTypeNames.TryGetValue(domain, out var serviceNames))
                {
                    serviceNames = new HashSet<(string name, string fullName)>();
                    serviceTypeNames[domain] = serviceNames;
                }

                if (string.IsNullOrEmpty(classTypeSymbol.GetNameSpace()))
                {
                    serviceNames.Add((classTypeSymbol.Name, classTypeSymbol.Name));
                }
                else
                {
                    serviceNames.Add((classTypeSymbol.Name, classTypeSymbol.GetNameSpace() + "." + classTypeSymbol.Name));
                }
            }
        }
    }
}
