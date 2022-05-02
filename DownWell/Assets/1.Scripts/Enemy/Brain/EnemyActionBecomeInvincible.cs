using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class EnemyActionBecomeInvincible : EnemyAction
    {
        [SerializeField] private bool onlyThisState = false;

        protected override void OnActionEnter()
        {
            GetComponent<Enemy>().invincible = true;
            handler.Next();
        }

        protected override void OnActionExit()
        {
            if (onlyThisState) GetComponent<Enemy>().invincible = false;
        }
    }
}