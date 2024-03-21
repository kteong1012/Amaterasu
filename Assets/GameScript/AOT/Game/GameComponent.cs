using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameComponent : MonoBehaviour,IDisposable
{
    protected bool IsInitialized { get; private set; }

    public virtual void Dispose()
    {
    }

    public async UniTask Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        await OnInitialize();
        IsInitialized = true;
    }

    public void Release()
    {
        Dispose();
    }

    protected virtual UniTask OnInitialize()
    {
        return UniTask.CompletedTask;
    }
}
