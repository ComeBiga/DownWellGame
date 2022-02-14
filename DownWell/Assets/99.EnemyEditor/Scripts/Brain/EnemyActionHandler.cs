using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionHandler
    {
        private List<CatDown.EnemyAction> actions;

        private int index = -1;

        public int Index
        {
            get { return index; }
        }

        public CatDown.EnemyAction Current
        {
            get { return actions[index]; }
        }

        public void Next()
        {
            index++;

            if (index >= actions.Count) index = 0;
        }

        public EnemyActionHandler(List<CatDown.EnemyAction> actions)
        {
            Init(actions);
        }

        public void Init(List<CatDown.EnemyAction> actions)
        {
            this.actions = actions;
            index = 0;
        }
    }

}
