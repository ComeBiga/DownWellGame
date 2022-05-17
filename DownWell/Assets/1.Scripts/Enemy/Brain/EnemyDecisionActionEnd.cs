using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyDecisionActionEnd : EnemyDecision
    {

        public override void OnActionEnd()
        {
            Debug.Log($"current state : {transition.Brain.Current.name} (OnActionEnd / EnemyDecisionActionEnd)");
            base.Decide(this.ToString());
        }
    }
}