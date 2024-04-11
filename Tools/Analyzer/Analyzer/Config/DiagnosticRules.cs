using Microsoft.CodeAnalysis;

namespace Analyzer.Config
{
    public static class DiagnosticRules
    {
        public static class ForceBraceRule
        {
            public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                id: DiagnosticIds.ForceBrace,
                title: _title,
                messageFormat: _message,
                category: DiagnosticCategories.Standard,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: _description
            );

            private const string _description = "禁止省略大括号.";
            private const string _message = "禁止省略大括号";
            private const string _title = "禁止省略大括号";
        }
    }
}