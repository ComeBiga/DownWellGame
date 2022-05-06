using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionHandler
    {
        private List<CatDown.EnemyAction> actions;
        private bool loop;
        private bool actionEnd = false;

        private int index = -1;

        public event System.Action OnActionEnd;

        public int Index
        {
            get { return index; }
        }

        public CatDown.EnemyAction Current
        {
            get { return actions[index]; }
        }

        public bool IsEnded
        {
            get { return actionEnd; }
        }

        public EnemyActionHandler(List<CatDown.EnemyAction> actions, bool loop = true)
        {
            Init(actions, loop);
        }

        public void Init(List<CatDown.EnemyAction> actions, bool loop)
        {
            this.actions = actions;
            index = 0;

            this.loop = loop;
            this.actions.Add(null);
        }
        public void Next()
        {
            if (actionEnd) return;

            index++;
            Debug.Log(index);

            if (index >= actions.Count - 1)
            {
                Debug.Log("Last Action");
                if (loop)
                {
                    index = 0;
                }
                else
                {
                    Debug.Log("Action End");
                    actionEnd = true;
                    OnActionEnd.Invoke();
                }
            }
        }

        public void SetFirstAction()
        {
            index = 0;
            actionEnd = false;
        }
    }

}
