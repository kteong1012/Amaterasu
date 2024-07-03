using Game;
using Game.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UI2D("UIHomePanel")]
public class UIHomePanel : UI2DPanel
{
    public override UI2DPanelLayer Layer => UI2DPanelLayer.Normal;

    public override UI2DPanelOptions Options => UI2DPanelOptions.None;

    public Button btnStartBattle;

    public override void OnCreate()
    {
        btnStartBattle.onClick.AddListener(OnClickBtnStartBattle);

        var playerDataService = GameService<PlayerDataService>.Instance;
        var roleData = playerDataService.GetPlayerData<RoleData>();
    }

    private async void OnClickBtnStartBattle()
    {
        var sceneService = GameService<SceneService>.Instance;
        await sceneService.ChangeToBattleScene();
        Close();
    }
}
