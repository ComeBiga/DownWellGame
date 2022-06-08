using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class CaveHelmetMove : EnemyActionHorizontalMoveTowardTarget
    {
        protected override void OnActionUpdate()
        {
            //불 켜지면
            if (PlayerManager.instance.playerObject.transform.GetChild(5).gameObject.GetComponent<Animator>().GetBool("On"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                base.OnActionUpdate();
            }

        }
    }
}