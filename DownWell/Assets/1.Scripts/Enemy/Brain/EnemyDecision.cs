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
            if (PlayerManager.instance.playerObject == null) yield return null;

            while (true)
            {
                if (EnemyBrain.CheckTargetRange(PlayerManager.instance.playerObject.transform, this.transform)) break;

                yield return null;
            }

            EnterExamine();

            while(true)
            {
                if (!EnemyBrain.CheckTargetRange(PlayerManager.instance.playerObject.transform, this.transform)) break;

                Examine();

                yield return null;
            }
        }

        public void StopCheck()
        {
            StopCoroutine(coroutineECheck);
        }

        public virtual void OnActionEnd() { }
        #endregion

        #region Protected Method
        protected virtual void EnterExamine() { }

        protected virtual void Examine() { }

        protected void Decide()
        {
            //transition.OnChangeState();
            OnDecide.Invoke();
        }

        protected void DecideAfterOneFrame()
        {
            StartCoroutine(EDecideAfterOneFrame());
        }

        #endregion

        #region Private Method

        private IEnumerator EDecideAfterOneFrame()
        {
            yield return null;

            Decide();
        }

        #endregion
    }
}
