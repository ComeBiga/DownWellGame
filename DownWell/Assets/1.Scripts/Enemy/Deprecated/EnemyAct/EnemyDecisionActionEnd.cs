using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionActionEnd : EnemyDecision
{
    private bool ended = false;

    public override bool Decide()
    {
        if (ended) return true;

        return false;
    }

    public void ChangeState()
    {
        ended = true;
    }
}
