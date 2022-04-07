using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatDown
{

    [System.Serializable]
    public class EnemyState
    {
        public string name = "";

        public List<CatDown.EnemyAction> actions;
        public List<CatDown.EnemyTransition> transitions;

        private CatDown.EnemyActionHandler handler;
        public CatDown.EnemyAction currentAction
        {
            get { return handler.Current; }
        }

        public void Init(CatDown.EnemyBrain brain)
        {
            foreach(var t in transitions)
            {
                t.Init(brain);
            }

            handler = new CatDown.EnemyActionHandler(actions);
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
            handler.Current.StopAction();

            // transition
            foreach(var t in transitions)
            {
                t.StopCheckDecision();
            }
        }
    }
}
