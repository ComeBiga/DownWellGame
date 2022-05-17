using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CatDown
{

    public class EnemyActionEvent : EnemyAction
    {
        public UnityEvent onEvent;

        protected override void OnActionEnter()
        {
            Debug.Log("OnEvent");
            onEvent.Invoke();

            handler.Next();
        }
    }
}