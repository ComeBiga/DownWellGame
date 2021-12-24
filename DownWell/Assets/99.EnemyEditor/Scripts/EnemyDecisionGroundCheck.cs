using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDecisionGroundCheck : EnemyDecisionCollisionCheck
{

    private void Start()
    {
        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
        collision.CalculateRaySpacing();
    }

    public override bool Decide()
    {
        collision.UpdateRaycastOrigins();

        float Yvel = GetComponent<Rigidbody2D>().velocity.y;

        if (collision.CheckCollision(CollisionDirection.DOWN) && Yvel < -0.01f)
        {
            Debug.Log(GetComponent<Rigidbody2D>().velocity.y);

            Debug.Log("Grounded");
            return true;
        }

        return false;
    }
}
