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

        public static class NamingRule
        {
            // public成员字段应为小驼峰命名
            public static readonly DiagnosticDescriptor PublicFieldMemberRule = new DiagnosticDescriptor(
                id: DiagnosticIds.PublicFieldMember,
                title: _title,
                messageFormat: "成员字段名'{0}'应为小驼峰命名, 请修改为'{1}'",
                category: DiagnosticCategories.Standard,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: "成员字段名应为小驼峰命名"
            );

            // private 和 protected 成员字段应为以下划线开头的小驼峰命名
            public static readonly DiagnosticDescriptor PrivateFieldMemberRule = new DiagnosticDescriptor(
                id: DiagnosticIds.PrivateFieldMember,
                title: _title,
                messageFormat: "成员字段名'{0}'应为以下划线开头的小驼峰命名, 请修改为'{1}'",
                category: DiagnosticCategories.Standard,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: "成员字段名应为以下划线开头的小驼峰命名"
            );

            // 成员属性名应为大驼峰命名
            public static readonly DiagnosticDescriptor PropertyMemberRule = new DiagnosticDescriptor(
                id: DiagnosticIds.PropertyMember,
                title: _title,
                messageFormat: "成员属性名'{0}'应为大驼峰命名, 请修改为'{1}'",
                category: DiagnosticCategories.Standard,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: "成员属性名应为大驼峰命名"
            );

            // 属性的访问修饰符只允许用public
            public static readonly DiagnosticDescriptor PropertyAccessRule = new DiagnosticDescriptor(
                id: DiagnosticIds.PropertyAccess,
                title: _title,
                messageFormat: "成员属性'{0}'的访问修饰符只允许用public",
                category: DiagnosticCategories.Standard,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: "成员属性的访问修饰符只允许用public"
            );

            private const string _title = "命名规范错误";
        }

        public static class GameServiceAttributeRule
        {
            public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                id: DiagnosticIds.GameServiceAttribute,
                title: _title,
                messageFormat: _message,
                category: DiagnosticCategories.Logic,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: _description
            );
            private const string _title = "GameService特性缺失";
            private const string _message = "GameService的子类必须包含GameServiceAttribute特性";
            private const string _description = "GameService的子类必须包含GameServiceAttribute特性.";

            public static DiagnosticDescriptor Test(string testContent)
            {
                return new DiagnosticDescriptor(
                id: DiagnosticIds.GameServiceAttribute,
                title: testContent,
                messageFormat: testContent,
                category: DiagnosticCategories.Logic,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: testContent
               );
            }
        }

        public static class GameServiceAttributeNone
        {
            public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                id: DiagnosticIds.GameServiceAttributeNone,
                title: _title,
                messageFormat: _message,
                category: DiagnosticCategories.Logic,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: _description
            );
            private const string _title = "GameService特性参数错误";
            private const string _message = "GameServiceAttribute特性的参数不能为GameServiceDomain.None";
            private const string _description = "GameServiceAttribute特性的参数不能为GameServiceDomain.None.";
        }

        public static class LoadSceneAsyncRule
        {
            public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                id: DiagnosticIds.LoadSceneAsync,
                title: _title,
                messageFormat: _message,
                category: DiagnosticCategories.Logic,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: _description
            );
            private const string _title = "禁止使用YooAssets.LoadSceneAsync";
            private const string _message = "禁止使用YooAssets.LoadSceneAsync接口，切换场景只允许在SceneService内部调用";
            private const string _description = "禁止使用YooAssets.LoadSceneAsync接口，切换场景只允许在SceneService内部调用.";
        }

        public static class UseGameLogRule
        {
            public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                id: DiagnosticIds.UseGameLog,
                title: _title,
                messageFormat: _message,
                category: DiagnosticCategories.Logic,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: _description
            );
            private const string _title = "禁止使用UnityEngine.Debug";
            private const string _message = "禁止使用UnityEngine.Debug类，使用Game.Log.GameLog代替";
            private const string _description = "禁止使用UnityEngine.Debug类，使用Game.Log.GameLog代替.";
        }

        public static class DontUseMagicNumberRule
        {
            public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                id: DiagnosticIds.DontUseMagicNumber,
                title: _title,
                messageFormat: _message,
                category: DiagnosticCategories.Standard,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true,
                description: _description
            );
            private const string _title = "禁止使用魔法数字 {0}";
            private const string _message = "禁止使用魔法数字 {0}";
            private const string _description = "禁止使用魔法数字.";
        }
    }
}