using Cysharp.Threading.Tasks;
using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[UI2D("UIBattleEndPanel")]
public class UIBattleEndPanel : UI2DPanel
{
    public override UI2DPanelLayer Layer => UI2DPanelLayer.Normal;

    public override UI2DPanelOptions Options => UI2DPanelOptions.None;

    public Button _restartButton;
    public Button _backToHomeButton;
    public TextMeshProUGUI _textResult;

    public override void OnCreate()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _backToHomeButton.onClick.AddListener(OnClickBackToHomeSceneButton);
    }

    public void SetWinnerCamp(UnitCamp winnerCamp)
    {
        if (winnerCamp == UnitCamp.Player)
        {
            _textResult.text = "WIN";
        }
        else
        {
            _textResult.text = "LOSE";
        }
    }
    private void OnClickBackToHomeSceneButton()
    {
        Close();
        var sceneService = GameEntry.Ins.GetService<SceneService>();
        sceneService.ChangeToHomeScene().Forget();
    }

    private void OnClickRestartButton()
    {
        var battleRoomService = GameEntry.Ins.GetService<BattleRoomService>();
        battleRoomService.CreateDemo();
        Close();
    }
}