using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CatDown
{

    public class EnemyDecisionCheckGround : CatDown.EnemyDecisionCheckCollision
    {

        protected override void EnterExamine()
        {
            collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
            collision.CalculateRaySpacing();
        }

        protected override void Examine()
        {
            collision.UpdateRaycastOrigins();

            if (collision.CheckCollision(CollisionDirection.DOWN))
            {
                Debug.Log("Grounded");

                base.Decide();
            }
        }
    }
}
