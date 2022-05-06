using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyDecisionActionEnd : EnemyDecision
    {
        public override void OnActionEnd()
        {
            base.Decide();
        }
    }
}