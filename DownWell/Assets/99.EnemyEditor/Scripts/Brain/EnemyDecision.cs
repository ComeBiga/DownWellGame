using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public abstract class EnemyDecision : MonoBehaviour
    {
        protected CatDown.EnemyTransition transition;

        public event System.Action OnDecide;

        private Coroutine coroutineECheck;

        public void Init(CatDown.EnemyTransition transition)
        {
            this.transition = transition;
        }

        #region Public Method
        public void Check()
        {
            coroutineECheck = StartCoroutine(ECheck());
        }

        IEnumerator ECheck()
        {
            while (true)
            {
                if (GameManager.instance.CheckTargetRange(this.gameObject.transform)) break;

                yield return null;
            }

            EnterExamine();

            while(true)
            {
                Examine();

                yield return null;
            }
        }

        public void StopCheck()
        {
            StopCoroutine(coroutineECheck);
        }
        #endregion

        #region Protected Method
        protected virtual void EnterExamine() { }

        protected virtual void Examine() { }

        protected void Decide()
        {
            //transition.OnChangeState();
            OnDecide.Invoke();
        }

        #endregion
    }
}
