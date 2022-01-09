using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossAction : MonoBehaviour
{
    public UnityEvent onEvent;

    public abstract void Take();

    //public virtual void StartAction() { }
    //public virtual void UpdateAction() { }
}
