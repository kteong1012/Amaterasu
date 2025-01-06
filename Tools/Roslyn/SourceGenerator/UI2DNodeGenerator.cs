using Analyzer.src.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class UI2DNodeGenerator : ISourceGenerator
    {
        private const string _ui2DNodeBaseType = "UI2DNode";
        private const string _ui2DNodeAttributeType = "UI2DAttribute";


        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new UI2DNodeSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not UI2DNodeSyntaxReceiver receiver)
            {
                return;
            }
            context.AddSourceOrWriteFile("UIServices.ApiOpenNode.g.cs", GenerateUIServiceApiOpen(receiver.openInfos));
            context.AddSourceOrWriteFile("UIServices.InitNodeInfo.g.cs", GenerateUIServiceInitInfo(receiver.openInfos));

            foreach (var info in receiver.openInfos)
            {
                context.AddSourceOrWriteFile($"{info.FullName}.g.cs", GenerateNodeClass(info));
            }
        }

        private string GenerateUIServiceApiOpen(List<UI2DNodeOpenInfo> infos)
        {
            var sb = new IntentStringBuilder(0);
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Cysharp.Threading.Tasks;");
            sb.AppendEmptyLine();
            sb.AppendLine("namespace Game");
            sb.AppendLine("{");//namespace start
            sb++;
            sb.AppendLine("public partial class UIService");
            sb.AppendLine("{");//class start
            sb++;

            sb.AppendLine("private string __GetNodeClassName<T>() where T : UI2DNode");
            sb.AppendLine("{");//method start __CreateNode
            sb++;

            foreach (var info in infos)
            {
                sb.AppendLine($"if (typeof(T) == typeof({info.FullName}))");
                sb.AppendLine("{"); //if start
                sb++;
                sb.AppendLine($"return \"{info.FullName}\";");
                sb--;
                sb.AppendLine("}"); //if end
            }
            sb.AppendLine("return null;");

            sb--;
            sb.AppendLine("}");//method end __CreateNode

            sb--;
            sb.AppendLine("}");//class end
            sb--;
            sb.AppendLine("}");//namespace end
            return sb.ToString();
        }

        private string GenerateUIServiceInitInfo(List<UI2DNodeOpenInfo> infos)
        {
            var sb = new IntentStringBuilder(0);
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Cysharp.Threading.Tasks;");
            sb.AppendEmptyLine();
            sb.AppendLine("namespace Game");
            sb.AppendLine("{");
            sb++;
            sb.AppendLine("public partial class UIService");
            sb.AppendLine("{");
            sb++;

            sb.AppendLine($"public bool TryGetNodeInfo(string nodeName, out UI2DNodeInfo nodeInfo)");
            sb.AppendLine("{");
            sb++;
            sb.AppendLine("switch (nodeName)");
            sb.AppendLine("{");
            sb++;
            foreach (var info in infos)
            {
                sb.AppendLine($"case \"{info.FullName}\":");
                sb++;
                sb.AppendLine($"nodeInfo = new UI2DNodeInfo()");
                sb.AppendLine("{");
                sb++;
                sb.AppendLine($"prefabPath = {info.prefabPath},");
                sb--;
                sb.AppendLine("};");
                sb.AppendLine("return true;");
                sb--;
            }
            sb.AppendLine("default:");
            sb++;
            sb.AppendLine("nodeInfo = default;");
            sb.AppendLine("return false;");
            sb--;
            sb.AppendLine("}");
            sb--;
            sb.AppendLine("}");

            sb--;
            sb.AppendLine("}");
            sb--;
            sb.AppendLine("}");

            return sb.ToString();
        }

        private string GenerateNodeClass(UI2DNodeOpenInfo info)
        {
            var sb = new IntentStringBuilder(0);

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Cysharp.Threading.Tasks;");
            sb.AppendEmptyLine();
            if (!string.IsNullOrEmpty(info.namespaceName))
            {
                sb.AppendLine($"namespace {info.namespaceName}");
                sb.AppendLine("{");
                sb++;
            }
            sb.AppendLine($"public partial class {info.className}");
            sb.AppendLine("{");
            sb++;

            sb.AppendLine($"public override string ClassName => \"{info.FullName}\";");

            sb--;
            sb.AppendLine("}");
            if (!string.IsNullOrEmpty(info.namespaceName))
            {
                sb--;
                sb.AppendLine("}");
            }
            return sb.ToString();
        }

        class UI2DNodeOpenInfo
        {
            public string namespaceName;
            public string className;
            public string FullName
            {
                get
                {
                    if (string.IsNullOrEmpty(namespaceName))
                    {
                        return className;
                    }
                    else
                    {
                        return $"{namespaceName}.{className}";
                    }
                }
            }
            public string prefabPath;

            public UI2DNodeOpenInfo(string namespaceName, string className)
            {
                this.namespaceName = namespaceName;
                this.className = className;
            }
        }

        class UI2DNodeSyntaxReceiver : ISyntaxContextReceiver
        {
            public List<UI2DNodeOpenInfo> openInfos = new List<UI2DNodeOpenInfo>();

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

                if (!classTypeSymbol.HasBaseType(_ui2DNodeBaseType))
                {
                    return;
                }

                if (classTypeSymbol.IsAbstract)
                {
                    return;
                }

                var attributes = classTypeSymbol.GetAttributes();

                var attribute = attributes.FirstOrDefault(a => a.AttributeClass.Name.Equals(_ui2DNodeAttributeType));

                if (attribute == null)
                {
                    return;
                }

                var ns = classTypeSymbol.GetNameSpace();

                var info = new UI2DNodeOpenInfo(ns, classTypeSymbol.Name);
                if (openInfos.Any(x => x.FullName == info.FullName))
                {
                    return;
                }

                info.prefabPath = attribute.ConstructorArguments[0].ToCSharpString();

                openInfos.Add(info);
            }
        }
    }
}
