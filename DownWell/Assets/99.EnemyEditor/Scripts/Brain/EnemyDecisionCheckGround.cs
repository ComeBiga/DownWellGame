using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            float Yvel = GetComponent<Rigidbody2D>().velocity.y;

            if (collision.CheckCollision(CollisionDirection.DOWN) && Yvel < -0.01f)
            {
                Debug.Log(GetComponent<Rigidbody2D>().velocity.y);

                Debug.Log("Grounded");

                base.Decide();
            }
        }
    }
}
