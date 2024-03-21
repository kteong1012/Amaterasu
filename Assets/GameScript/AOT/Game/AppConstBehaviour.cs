using System.Diagnostics;
using UnityEngine;
using YooAsset;

namespace Game
{
    //优先级最高
    public class AppConstBehaviour : MonoBehaviour
    {
        public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

        public LoadDllMode LoadDllMode = LoadDllMode.EditorMode;

        private void Awake()
        {
            SetAppConst();
        }

        [Conditional("UNITY_EDITOR")]
        private void SetAppConst()
        {
            AppConst.PlayMode = PlayMode;
            AppConst.LoadDllMode = LoadDllMode;
        }
    }
}