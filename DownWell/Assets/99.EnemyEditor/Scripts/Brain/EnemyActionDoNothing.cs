using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{

    public class EnemyActionDoNothing : CatDown.EnemyAction
    {
        protected override void OnActionEnter()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
