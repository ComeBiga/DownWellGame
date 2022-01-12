using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossAction : MonoBehaviour
{
    public static bool ended = true;

    public UnityEvent onEvent;

    public abstract void Take();

    protected static void Cut()
    {
        //onCut?.Invoke();
        BossAction.ended = true;
    }

    //public virtual void StartAction() { }
    //public virtual void UpdateAction() { }
}
