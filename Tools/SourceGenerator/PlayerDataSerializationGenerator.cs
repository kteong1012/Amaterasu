using Microsoft.CodeAnalysis;
using System;

namespace SourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class PlayerDataSerializationGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => SyntaxContexReceiver.Create());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if(context.SyntaxReceiver is not SyntaxContexReceiver receiver)
            {
                return;
            }
        }

        class SyntaxContexReceiver : ISyntaxContextReceiver
        {
            public static SyntaxContexReceiver Create()
            {
                return new SyntaxContexReceiver();
            }
            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                var node = context.Node;

            }
        }
    }
}
