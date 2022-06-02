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
        private bool decided = false;

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
            decided = false;

            while (true)
            {
                if (EnemyBrain.CheckTargetRange(PlayerManager.instance.playerObject.transform, this.transform)) break;

                yield return null;
            }

            EnterExamine();
            //Debug.Log($"current state : {transition.Brain.Current.name} (ECheck / EnemyDecision.cs");

            while(true)
            {
                if (!EnemyBrain.CheckTargetRange(PlayerManager.instance.playerObject.transform, this.transform) || decided) break;

                Examine();

                yield return null;
            }
        }

        public void StopCheck()
        {
            if(coroutineECheck != null) StopCoroutine(coroutineECheck);
        }

        public virtual void OnActionEnd() { }
        #endregion

        #region Protected Method
        protected virtual void EnterExamine() { }

        protected virtual void Examine() { }

        protected void Decide(string log = "")
        {
            //transition.OnChangeState();
            //Debug.Log($"Decide from : {log}, current state : {transition.Brain.Current.name}");
            decided = true;
            OnDecide.Invoke();
        }

        protected void DecideAfterOneFrame(string log = "")
        {
            StartCoroutine(EDecideAfterOneFrame(log));
        }

        #endregion

        #region Private Method

        private IEnumerator EDecideAfterOneFrame(string log = "")
        {
            yield return null;

            Decide(log);
        }

        #endregion
    }
}
