namespace Game
{
    /// <summary>
    /// 服务的生命周期。
    /// </summary>
    public enum GameServiceLifeSpan
    {
        /// <summary>
        /// 游戏生命周期，游戏开始时创建，游戏结束时销毁。
        /// </summary>
        Game,
        /// <summary>
        /// 登陆生命周期，登陆时创建，登出时销毁。
        /// </summary>
        Login,
        /// <summary>
        /// 战斗生命周期，战斗开始时创建，战斗结束时销毁。
        /// </summary>
        Battle
    }
}
