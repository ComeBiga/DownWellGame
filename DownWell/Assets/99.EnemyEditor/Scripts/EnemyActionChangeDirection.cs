using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDecisionActionEnd))]
public class EnemyActionChangeDirection : EnemyAct
{
    [SerializeField] private EnemyDecisionActionEnd actionEnd;

    protected override void StartAct()
    {
        EnemyAct.direction *= -1;
        actionEnd.ChangeState();
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        return false;
    }
}
