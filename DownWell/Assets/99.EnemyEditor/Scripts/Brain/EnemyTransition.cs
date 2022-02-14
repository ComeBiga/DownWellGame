using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    [System.Serializable]
    public class EnemyTransition
    {
        private CatDown.EnemyBrain brain;

        public CatDown.EnemyDecision decision;
        public string toState = "";

        public void Init(CatDown.EnemyBrain brain)
        {
            this.brain = brain;

            decision.OnDecide += OnChangeState;
        }

        public void CheckDecision()
        {
            decision.Check();
        }

        public void OnChangeState()
        {
            brain.ChangeState(toState);
        }

        public void StopCheckDecision()
        {
            decision.StopCheck();
        }
    }
}
