using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [UI2D("UILoadingPanel")]
    public partial class UILoadingPanel : UI2DPanel
    {
        [SerializeField]
        private Slider _progressSlider;
        [SerializeField]
        private TMPro.TextMeshProUGUI _progressText;

        public override UI2DPanelLayer Layer => UI2DPanelLayer.Loading;
        public override UI2DPanelOptions Options => UI2DPanelOptions.None;
        public override bool IsAutoRelease => false;

        protected override void OnCreate()
        {
            _progressSlider.value = 0;
            _progressText.text = "0%";
        }

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
            _progressText.text = $"{progress * 100:F2}%";
        }
    }
}
