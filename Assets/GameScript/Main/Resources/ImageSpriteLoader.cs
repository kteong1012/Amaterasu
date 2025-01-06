using Game.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace Game
{
    [RequireComponent(typeof(Image))]
    public class ImageSpriteLoader : MonoBehaviour
    {
        [SerializeField]
        private bool _enableImageIfNull = true;

        public bool EnableImageIfNull => _enableImageIfNull;

        private Image _image;
        public Image Image
        {
            get
            {
                if (_image == null)
                {
                    _image = GetComponent<Image>();
                }
                return _image;
            }
        }

        private string _spritePath = "";
        public string SpritePath
        {
            get => _spritePath;
            set
            {
                var name = value;
                if (string.IsNullOrWhiteSpace(name))
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
                if (_enableImageIfNull)
                {
                    Image.enabled = value != null;
                }
            }
        }

        private AssetHandle _handle;

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
