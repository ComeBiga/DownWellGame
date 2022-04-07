using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAct : MonoBehaviour
{
    //public bool onAnimationEvent = false;

    private bool onStart = true;
    private bool doNextAct = false;

    protected static int direction = 1;

    public virtual void Init()
    {
        onStart = true;
        doNextAct = false;
    }

    public bool UpdateAct()
    {
        //if (!GameManager.instance.CheckTargetRange(transform)) return false;

        // Start
        if(onStart)
        {
            StartAct();
            //StartCoroutine(CoroutineFixedAct());
            onStart = false;
        }

        //StartCoroutine(ActLoop());
        doNextAct = Act(null);

        // End
        if(doNextAct)
        {
            EndAct();
        }

        return doNextAct;
    }

    IEnumerator CoroutineFixedAct()
    {
        while(true)
        {
            if (doNextAct) break;

            if (!FixedAct()) break;

            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual void StartAct() { }
    public abstract bool Act(Rigidbody2D rigidbody);
    protected virtual bool FixedAct() { return false; }
    protected virtual void EndAct() { }

    protected static void SetDirection(int dir)
    {
        direction = dir;
    }
}
