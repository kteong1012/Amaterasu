using Cysharp.Threading.Tasks;
using Game.Cfg;
using Jing.TurbochargedScrollList;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UISelectLanguagePanel")]
    public partial class UISelectLanguagePanel : UI2DPanel
    {
        public override UI2DPanelLayer Layer => UI2DPanelLayer.Popup;

        public override UI2DPanelOptions Options => UI2DPanelOptions.None;

        [SerializeField]
        private ScrollList _scrollList;

        protected override void OnCreate()
        {
            _scrollList.SetAsVertical(OnRenderItem);
        }

        protected override void OnShow()
        {
            UpdateView();
        }

        protected override void UpdateView()
        {
            _scrollList.UpdateData(AppInfo.AppConfig.languages);
        }

        private void ChangeLanguage(Language language)
        {
            Task(language).Forget();
            return;
            async UniTaskVoid Task(Language lang)
            {
                var result = await MessageBox.ShowOkCancel("是否切换语言？".Translate(), "切换语言".Translate());
                if (result != OkCancelResult.Ok)
                {
                    return;
                }
                await SSS.Get<ConfigService>().SetLanguage(lang);
                await MessageBox.ShowOk("切换语言成功".Translate(), "切换语言".Translate());
                Close();
            }
        }

        private void OnRenderItem(ScrollListItem item, object data, bool isFresh)
        {
            if (item is UISelectLanguageScrollCell cell)
            {
                void Callback(Language language) => ChangeLanguage(language);

                var language = (Language)data;
                var currentLanguate = SSS.Get<Game.Cfg.ConfigService>().Language;
                var interactable = language != currentLanguate;
                cell.SetData(language, interactable, Callback);
            }
        }
    }
}
