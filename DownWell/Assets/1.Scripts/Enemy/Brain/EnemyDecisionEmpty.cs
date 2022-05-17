using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyDecisionEmpty : EnemyDecision
    {
        protected override void EnterExamine()
        {
            DecideAfterOneFrame(this.ToString());
        }
    }
}