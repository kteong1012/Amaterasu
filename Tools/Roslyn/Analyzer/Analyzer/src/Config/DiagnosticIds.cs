﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Analyzer.Config
{
    public static class DiagnosticIds
    {
        #region Standard
        #region Error
        public const string ForceBrace = "STD001"; // 禁止省略大括号
        public const string PublicFieldMember = "STD002"; // 成员字段名应为小驼峰命名
        public const string PrivateFieldMember = "STD003";    // 成员字段名应为以下划线开头的小驼峰命名
        public const string PropertyMember = "STD004";    // 成员属性名应为大驼峰命名
        public const string PropertyAccess = "STD005";    // 属性的访问修饰符只允许用public
        public const string DontUseMagicNumber = "STD006";    // 禁止使用魔法数字
        public const string DontInvokeRoleDataObjectConstructor = "STD007";    // 禁止调用 RoleDataObject 构造函数
        #endregion
        #endregion

        #region Logic
        #region Error
        public const string GameServiceAttribute = "LOG001";    // GameService的子类必须包含GameServiceAttribute特性
        public const string GameServiceAttributeNone = "LOG002";    // GameService的子类必须包含GameServiceAttribute特性
        public const string LoadSceneAsync = "LOG003";  // 禁止使用YooAssets.LoadSceneAsync接口，切换场景只允许在SceneService内部调用
        public const string UseGameLog = "LOG004";  //  禁止使用UnityEngine.Debug类，使用Game.Log.GameLog代替
        public const string UI2DAttribute = "LOG005";  //  UI2D的子类必须包含UI2DAttribute特性
        public const string InokeYooAssets = "LOG006";  //  禁止直接调用YooAssets类
        public const string RequirePartialRule = "LOG007";  //  类必须包含 partial 修饰符
        #endregion
        #endregion
    }
}
