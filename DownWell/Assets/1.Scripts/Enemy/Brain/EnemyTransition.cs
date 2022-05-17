using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    [System.Serializable]
    public class EnemyTransition
    {
        private CatDown.EnemyBrain brain;
        public CatDown.EnemyBrain Brain { get { return brain; } }

        public CatDown.EnemyDecision decision;
        public string toState = "";

        public void Init(CatDown.EnemyBrain brain)
        {
            this.brain = brain;

            decision.Init(this);
            //Debug.Log($"{brain.gameObject.name} / {decision}");
            decision.OnDecide += OnChangeState;

            //Debug.Log($"Change to : {toState}");
        }

        public void CheckDecision()
        {
            Debug.Log($"Current state : {brain.Current.name}, Change to : {toState} (CheckDecision / EnemyTransition.cs)");
            if (decision != null) decision.Check();
        }

        public void OnChangeState()
        {
            Debug.Log($"Current state : {brain.Current.name}, Change to : {toState} (OnChangeState / EnemyTransition.cs)");
            brain.ChangeState(toState);
        }

        public void StopCheckDecision()
        {
            if (decision != null) decision.StopCheck();
        }

        public void OnActionEnd()
        {
            decision.OnActionEnd();
        }
    }
}
