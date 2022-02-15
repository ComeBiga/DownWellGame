using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public abstract class EnemyDecision : MonoBehaviour
    {
        protected CatDown.EnemyTransition transition;

        public event System.Action OnDecide;

        public void Init(CatDown.EnemyTransition transition)
        {
            this.transition = transition;
        }

        #region Public Method
        public void Check()
        {
            StartCoroutine(ECheck());
        }

        IEnumerator ECheck()
        {
            while(true)
            {
                Examine();

                yield return null;
            }
        }

        public void StopCheck()
        {
            StopCoroutine(ECheck());
        }
        #endregion

        #region Protected Method
        protected virtual void EnterExamine() { }

        protected abstract void Examine();

        protected void Decide()
        {
            transition.OnChangeState();
        }

        #endregion
    }
}
