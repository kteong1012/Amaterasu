namespace Game
{
    public static class BattleConstants
    {
        /// <summary>
        /// 逻辑坐标到场景坐标的缩放比例
        /// </summary>
        public static NumberX1000 LogicToSceneScale = NumberX1000.CreateFromX1000Value(5000);

        /// <summary>
        /// 最大攻击速度
        /// </summary>
        public static NumberX1000 MaxACTSPED = NumberX1000.CreateFromX1000Value(700000);

        /// <summary>
        /// 最小攻击速度
        /// </summary>
        public static NumberX1000 MinACTSPED = NumberX1000.CreateFromX1000Value(10000);
    }
}
