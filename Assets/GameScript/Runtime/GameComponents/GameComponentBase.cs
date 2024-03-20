using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameComponentBase : MonoBehaviour
{
    public virtual UniTask Init() => UniTask.CompletedTask;
}
