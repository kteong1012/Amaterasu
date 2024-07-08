using Cysharp.Threading.Tasks;
using Game.Cfg;
using Game.Log;
using UniFramework.Event;
using UnityEngine;

namespace Game
{
    [GameService(GameServiceDomain.Battle)]
    public partial class BattleRoomService : GameService
    {
        #region Fields & Properties
        public YooAssetGameObjectPool DamageTextPool { get; } = new YooAssetGameObjectPool("UI3D_DamageText");
        #endregion

        #region Override Methods
        protected override async UniTask Awake()
        {
            AddEventListener<BattleEvent.BattleEndEvent>(OnBattleEnd);

            await UniTask.CompletedTask;
        }

        protected override UniTask Start()
        {
            CreateDemo();
            return UniTask.CompletedTask;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        public void CreateDemo()
        {
            SSS.BattleUnitService.DestroyAllUnits();
            var unit1 = SSS.BattleUnitService.CreateUnit(1001, 10, UnitCamp.Enemy);
            unit1.transform.position = new Vector3(20, unit1.transform.position.y, 20);
            var unit2 = SSS.BattleUnitService.CreateUnit(1001, 10, UnitCamp.Player);
            unit2.transform.position = new Vector3(-20, unit2.transform.position.y, -20);
            unit2.GetUnitComponent<UnitStatsComponent>().LinearAdd(NumericId.ACTSPD, NumericValueType.BaseAdd, NumberX1000.One * 50);
        }
        #endregion

        #region Event Methods
        private async void OnBattleEnd(IEventMessage message)
        {
            var battleEndEvent = message as BattleEvent.BattleEndEvent;
            GameLog.Info("战斗结束, 胜利方: " + battleEndEvent.winnerCamp);
            await UniTask.Delay(3000);
            var uiBattleEndPanel = UIService.OpenPanel<UIBattleEndPanel>();
            uiBattleEndPanel.SetWinnerCamp(battleEndEvent.winnerCamp);
        }
        #endregion
    }
}
