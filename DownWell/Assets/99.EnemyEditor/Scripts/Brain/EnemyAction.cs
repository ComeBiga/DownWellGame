using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public abstract class EnemyAction : MonoBehaviour
    {
        protected CatDown.EnemyActionHandler handler;

        public void Take(CatDown.EnemyActionHandler handler)
        {
            this.handler = handler;

            StartCoroutine(ETake());
        }

        IEnumerator ETake()
        {
            OnActionEnter();

            while(true)
            {
                OnActionUpdate();

                yield return null;
            }
        }

        public void StopAction()
        {
            StopCoroutine(ETake());
        }

        protected virtual void OnActionEnter()
        {

        }

        protected virtual void OnActionUpdate()
        {
            
        }

    }
}
