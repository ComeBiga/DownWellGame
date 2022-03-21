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
            while(true)
            {
                if (GameManager.instance.CheckTargetRange(this.gameObject.transform)) break;

                yield return null;
            }

            Debug.Log("StartAction");
            OnActionEnter();

            while(true)
            {
                //Debug.Log("ETake in While");
                OnActionUpdate();

                yield return null;
            }
        }

        public void StopAction()
        {
            Debug.Log($"{gameObject.name} / StopAction");
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
    }
}
