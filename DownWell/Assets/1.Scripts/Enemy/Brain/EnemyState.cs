using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatDown
{

    [System.Serializable]
    public class EnemyState
    {
        public string name = "";
        public bool actionLoop = true;

        public List<CatDown.EnemyAction> actions;
        public List<CatDown.EnemyTransition> transitions;

        private CatDown.EnemyActionHandler handler;
        public CatDown.EnemyAction currentAction
        {
            get { return handler.Current; }
        }

        public void Init(CatDown.EnemyBrain brain)
        {
            handler = new CatDown.EnemyActionHandler(actions, actionLoop);
            
            foreach(var t in transitions)
            {
                t.Init(brain);
                handler.OnActionEnd += t.OnActionEnd;
            }

        }

        public void EnterState()
        {
            handler.SetFirstAction();
        }

        public void Handle()
        {
            // action
            if(handler.Current != null) handler.Current.Take(handler);

            // transition
            foreach(var t in transitions)
            {
                t.CheckDecision();
            }
        }

        public void Stop()
        {
            // action
            if (handler.Current != null) handler.Current.StopAction();

            // transition
            foreach(var t in transitions)
            {
                t.StopCheckDecision();
            }
        }
    }
}
