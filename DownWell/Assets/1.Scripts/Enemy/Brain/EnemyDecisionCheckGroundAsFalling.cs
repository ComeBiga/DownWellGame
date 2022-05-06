using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    public class EnemyDecisionCheckGroundAsFalling : EnemyDecisionCheckCollision
    {
        [Header("Falling")]
        [SerializeField] private float minFallingVelocity = 1f;

        protected override void EnterExamine()
        {
            collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
            collision.CalculateRaySpacing();
        }

        protected override void Examine()
        {
            collision.UpdateRaycastOrigins();

            float Yvel = GetComponent<Rigidbody2D>().velocity.y;

            //Debug.Log(Yvel);
            
            if (collision.CheckCollision(CollisionDirection.DOWN) && Yvel < -minFallingVelocity)
            {

                Debug.Log("Grounded");

                base.Decide();
            }
        }
    }
}