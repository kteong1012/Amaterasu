using Cysharp.Threading.Tasks;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

public class UIHomeWindow : MonoBehaviour
{
    private Text _version;
    private GameObject _aboutView;

    private void Awake()
    {
        _version = this.transform.Find("version").GetComponent<Text>();
        _aboutView = this.transform.Find("AboutView").gameObject;


        var loginBtn = this.transform.Find("Start").GetComponent<Button>();
        loginBtn.onClick.AddListener(OnClickLoginBtn);

        var aboutBtn = this.transform.Find("About").GetComponent<Button>();
        aboutBtn.onClick.AddListener(OnClicAboutBtn);

        var maskBtn = this.transform.Find("AboutView/mask").GetComponent<Button>();
        maskBtn.onClick.AddListener(OnClickMaskBtn);
    }
    private void Start()
    {
        var package = YooAsset.YooAssets.GetPackage("DefaultPackage");
        _version.text = "Ver : " + package.GetPackageVersion();
    }

    private async void OnClickLoginBtn()
    {
        await YooAssets.LoadSceneAsync("scene_battle");
        GameEntryEventsDefine.Login.SendEventMessage();
    }
    private void OnClicAboutBtn()
    {
        _aboutView.SetActive(true);
    }
    private void OnClickMaskBtn()
    {
        _aboutView.SetActive(false);
    }
}