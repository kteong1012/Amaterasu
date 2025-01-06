using Game.Log;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererSpriteLoader : MonoBehaviour
    {
        [SerializeField]
        private bool _disableImageIfNull = true;

        public bool DisableImageIfNull => _disableImageIfNull;

        public SpriteRenderer Image { get; private set; }

        private string _spritePath = "";
        public string SpritePath
        {
            get => _spritePath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "";
                }
                if (_spritePath == value)
                {
                    return;
                }
                _spritePath = value;
                LoadSprite();
            }
        }

        private Sprite _Sprite
        {
            get => Image.sprite;
            set
            {
                Image.sprite = value;
                if (_disableImageIfNull)
                {
                    Image.enabled = value != null;
                }
            }
        }

        private AssetHandle _handle;

        private void Awake()
        {
            Image = GetComponent<SpriteRenderer>();
        }

        private void LoadSprite()
        {
            if (string.IsNullOrWhiteSpace(_spritePath))
            {
                _Sprite = null;
                return;
            }
            if (_handle != null)
            {
                _handle.Release();
            }
            _handle = YooAssets.LoadAssetSync<Sprite>(_spritePath);
            if (_handle == null)
            {
                GameLog.Error($"加载图片失败：{_spritePath}");
                return;
            }
            _Sprite = _handle.GetAssetObject<Sprite>();
        }

        private void OnDestroy()
        {
            if (_handle != null)
            {
                _handle.Release();
            }
        }
    }
}
