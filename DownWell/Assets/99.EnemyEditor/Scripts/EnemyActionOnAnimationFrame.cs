using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyActionOnAnimationFrame : EnemyAct
{
    public UnityEvent onEvent;

    private bool onActive = false;

    public override bool Act(Rigidbody2D rigidbody)
    {
        return onActive;
    }

    public void OnEvent()
    {
        onActive = true;
    }
}
