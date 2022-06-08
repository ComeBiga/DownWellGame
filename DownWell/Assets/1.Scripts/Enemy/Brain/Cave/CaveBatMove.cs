using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class CaveBatMove : EnemyActionFollowTarget
    {
        protected override void OnActionUpdate()
        {
            //�� ������
            if(PlayerManager.instance.playerObject.transform.GetChild(5).gameObject.GetComponent<Animator>().GetBool("On"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else
            {
                base.OnActionUpdate();
            }
    
        }
    }
}