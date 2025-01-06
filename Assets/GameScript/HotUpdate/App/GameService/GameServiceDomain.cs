namespace Game
{
    /// <summary>
    /// 游戏服务作用域
    /// </summary>
    public enum GameServiceDomain
    {
        None,
        /// <summary>
        /// 游戏，游戏开始时创建，游戏结束时销毁。
        /// </summary>
        Game,
        /// <summary>
        /// 账号，登陆时创建，登出时销毁。
        /// </summary>
        Account,
        /// <summary>
        /// 选择存档时创建，退出存档时销毁。
        /// </summary>
        Player,
        /// <summary>
        /// 选择角色创建，退出角色时销毁。
        /// </summary>
        Role,
        /// <summary>
        /// 战斗，战斗开始时创建，战斗结束时销毁。
        /// </summary>
        Room
    }
}
