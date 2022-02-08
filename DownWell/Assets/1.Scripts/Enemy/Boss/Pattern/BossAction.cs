using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BossAction : MonoBehaviour
{
    public static bool ready = true;
    public static float interval;
    [SerializeField] protected float addedInterval = 0f;

    public UnityEvent onEvent;

    public void TakeAction()
    {
        BossAction.ready = false;

        //Take();
        Invoke("Take", interval);
    }

    public abstract void Take();

    protected static void Cut()
    {
        //onCut?.Invoke();
        BossAction.ready = true;

        //Invoke("ReadyForAction", interval + addedInterval);
    }

    private void ReadyForAction()
    {
        Debug.Log("Ready");
        BossAction.ready = true;

        //var action = GetRandomAction();
        //action.TakeAction();
    }

    //public virtual void StartAction() { }
    //public virtual void UpdateAction() { }
}
