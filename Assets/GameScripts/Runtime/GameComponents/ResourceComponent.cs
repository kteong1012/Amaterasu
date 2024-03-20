using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class ResourceComponent : GameComponentBase
{
    public EPlayMode PlayMode;

    public override async UniTask Init()
    {
        YooAssets.Initialize();

    }
}
