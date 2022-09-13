using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public abstract class EnemyAction : MonoBehaviour
    {
        protected CatDown.EnemyActionHandler handler;

        private Coroutine coroutineETake;

        public void Take(CatDown.EnemyActionHandler handler)
        {
            this.handler = handler;

            coroutineETake = StartCoroutine(ETake());
        }

        IEnumerator ETake()
        {
            if (PlayerManager.instance.playerObject == null) yield return null;

            while (true)
            {
                if (EnemyBrain.CheckTargetRange(PlayerManager.instance.playerObject.transform, this.transform)) break;

                yield return null;
            }

            //Debug.Log($"This Action : {}");
            OnActionEnter();

            while(true)
            {
                if (!EnemyBrain.CheckTargetRange(PlayerManager.instance.playerObject.transform, this.transform)) break;

                //Debug.Log("ETake in While");
                OnActionUpdate();

                yield return new WaitForFixedUpdate();
            }
        }

        public void StopAction()
        {
            //Debug.Log($"{gameObject.name} / StopAction");
            StopCoroutine(coroutineETake);

            OnActionExit();
        }

        protected virtual void OnActionEnter()
        {

        }

        protected virtual void OnActionUpdate()
        {
            
        }

        protected virtual void OnActionExit()
        {
            
        }

        protected void Next()
        {
            handler.Current.StopAction();

            handler.Next();
        }

        protected void NextAfterOneFrame()
        {
            StartCoroutine(ENextAfterOneFrame());
        }

        private IEnumerator ENextAfterOneFrame()
        {
            yield return null;

            Next();
        }
    }
}
