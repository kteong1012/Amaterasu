namespace Game
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class GameServiceDomainParentAttribute : System.Attribute
    {
        public GameServiceDomainParentAttribute(GameServiceDomain parent)
        {
            Parent = parent;
        }
        public GameServiceDomain Parent { get; }
    }

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
        [GameServiceDomainParent(Game)]
        Account,
        /// <summary>
        /// 战斗，战斗开始时创建，战斗结束时销毁。
        /// </summary>
        [GameServiceDomainParent(Account)]
        Battle
    }
}
