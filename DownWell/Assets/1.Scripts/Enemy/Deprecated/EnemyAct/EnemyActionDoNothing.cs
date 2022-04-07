using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionDoNothing : EnemyAct
{
    public override bool Act(Rigidbody2D rigidbody)
    {
        //Debug.Log("Do Nothing");
        return false;
    }
}
