using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitForTime : EnemyDecision
{
    public float time = 3.0f;

    private float current = 0;

    public override bool Decide()
    {
        //Debug.Log("EnemyWaitForTime Act()");

        current += Time.deltaTime;

        if(current > time)
        {
            current = 0;
            return true;
        }

        return false;
    }
}
