using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game.Cfg
{
    public class SwitchLanguageEvent : IEventMessage
    {
        public Language Language { get; set; }

        public static void SendMsg(Language language)
        {
            var e = new SwitchLanguageEvent();
            e.Language = language;
            UniEvent.SendMessage(e);
        }
    }

    [GameService(GameServiceDomain.Game)]
    public partial class ConfigService : GameService, ILocalizationHandler
    {
        private const string _playerLanguageKey = "PlayerLanguage";
        private Dictionary<string, string> _textMap = new Dictionary<string, string>();
        private Dictionary<Language, string> _languageFontPathMap = new Dictionary<Language, string>();
        private AssetHandle _fontAssetHandle;
        private TMP_FontAsset _fontAsset;
        public Language Language { get; private set; }

        protected override UniTask Awake()
        {
            Localization.LocalizationHandler = this;
            InitLanguageFontPathMap();
            return base.Awake();
        }
        protected override async UniTask Start()
        {
            await InitLanguage();
            await LoadConfigs();
        }

        protected override void OnDestroy()
        {
            if (Localization.LocalizationHandler == this)
            {
                Localization.LocalizationHandler = null;
            }
            base.OnDestroy();
        }

        private void InitLanguageFontPathMap()
        {
            foreach (Language lang in Enum.GetValues(typeof(Language)))
            {
                _languageFontPathMap.Add(lang, $"font_{lang}");
            }
        }

        private async Task LoadConfigs()
        {
            GameLog.Info("====初始化配置====");
            await LoadAll(Loader);
            GameLog.Info("====初始化配置完成====");
        }

        public async UniTask SetLanguage(Language language)
        {
            Language = language;
            PlayerPrefs.SetString(_playerLanguageKey, Language.ToString());

            _textMap.Clear();
            GameLog.Info("====开始加载多语言数据====");
            await LoadAllText();
            GameLog.Info("====多语言数据加载完成====");

            GameLog.Info("====开始加载字体====");
            var fontPath = _languageFontPathMap[Language];
            var handle = SSS.Get<ResourceService>().LoadAssetAsync<TMP_FontAsset>(fontPath);
            await handle.ToUniTask();
            var fontAsset = handle.GetAssetObject<TMP_FontAsset>();
            _fontAssetHandle?.Release();
            _fontAssetHandle = handle;
            _fontAsset = fontAsset;
            GameLog.Info("====字体加载完成====");

            SwitchLanguageEvent.SendMsg(Language);
        }

        private async UniTask InitLanguage()
        {
            var languageStr = PlayerPrefs.GetString(_playerLanguageKey);
            var lang = Language.zh_CN;
            if (!string.IsNullOrEmpty(languageStr))
            {
                if (Enum.TryParse<Language>(languageStr, out var language))
                {
                    lang = language;
                }
            }
            else
            {
                var sysLang = Application.systemLanguage;
                switch (sysLang)
                {
                    case SystemLanguage.ChineseSimplified:
                        {
                            lang = Language.zh_CN;
                        }
                        break;
                    case SystemLanguage.English:
                        {
                            lang = Language.en_US;
                        }
                        break;
                    case SystemLanguage.Japanese:
                        {
                            lang = Language.ja_JP;
                        }
                        break;
                    default:
                        {
                            lang = Language.zh_CN;
                        }
                        break;
                }
            }

            await SetLanguage(lang);
        }

        private async UniTask<ByteBuf> Loader(string tableName)
        {
            GameLog.Debug($"加载配置表 {tableName}");
            var bytes = await SSS.Get<ResourceService>().LoadRawFileAsync(tableName);
            return new ByteBuf(bytes);
        }

        private async UniTask LoadAllText()
        {
            var bytes = await SSS.Get<ResourceService>().LoadRawFileAsync(Language.ToString());
            var buf = new ByteBuf(bytes);

            // count
            var count = buf.ReadSize();
            for (int i = 0; i < count; i++)
            {
                var key = buf.ReadString();
                var value = buf.ReadString();
                _textMap[key] = value;
            }
        }

        string ILocalizationHandler.GetText(string key, string defaultText)
        {
            if (!_textMap.TryGetValue(key, out var value))
            {
                if (string.IsNullOrEmpty(defaultText))
                {
                    return $"(tbt:{key})";
                }
                else
                {
                    return $"(tbt:{key}){defaultText}";
                }
            }
            return value;
        }

        TMP_FontAsset ILocalizationHandler.GetFont() => _fontAsset;
    }
}
