using Game;
using Game.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UI2D("UIHomePanel")]
public partial class UIHomePanel : UI2DPanel
{
    public override UI2DPanelLayer Layer => UI2DPanelLayer.Normal;

    public override UI2DPanelOptions Options => UI2DPanelOptions.None;

    public Button btnStartBattle;

    protected override void OnCreate()
    {
        btnStartBattle.onClick.AddListener(OnClickBtnStartBattle);

        var roleData = SSS.PlayerDataService.GetPlayerData<RoleData>();
    }

    private async void OnClickBtnStartBattle()
    {
        await SSS.SceneService.ChangeToBattleScene();
        Close();
    }
}
