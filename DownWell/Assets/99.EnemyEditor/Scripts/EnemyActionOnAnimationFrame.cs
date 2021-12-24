using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class EnemyActionOnAnimationFrame : EnemyAct
{
    //public UnityEvent onEvent;

    private bool onActive = false;

    public override void Init()
    {
        base.Init();

        onActive = false;
    }

    protected override void StartAct()
    {
        onActive = false;
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        //Debug.Log(onActive);
        return onActive;
    }

    public void OnEvent()
    {
        onActive = true;
    }
}
