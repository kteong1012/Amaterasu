using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    protected bool IsInitialized { get; private set; }

    public async UniTask Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        await OnInitialize();
        IsInitialized = true;
    }

    protected virtual UniTask OnInitialize()
    {
        return UniTask.CompletedTask;
    }
}
