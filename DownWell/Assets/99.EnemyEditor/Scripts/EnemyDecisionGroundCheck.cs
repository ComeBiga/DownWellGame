using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDecisionGroundCheck : EnemyDecisionCollisionCheck
{
    public UnityEvent onGround;

    private void Start()
    {
        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
        collision.CalculateRaySpacing();
    }

    public override bool Decide()
    {
        collision.UpdateRaycastOrigins();

        //Debug.Log("GroundCheck");

        if (collision.CheckCollision(CollisionDirection.DOWN) && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            onGround.Invoke();
            return true;
        }

        return false;
    }
}
