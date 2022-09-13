using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyOctoTurretMovement : JellyOctoMovement
{

    // Update is called once per frame
    protected override void Update()
    {
        if (!adjustedPos)
            AdjustPositionOutOfRange();

        collision.UpdateRaycastOrigins();

        if (collision.CheckCollision(forwardCollision)) ChangeDirectionCCW();
        if (!collision.CheckCollision(groundCollision)) ChangeDirectionCW();
        if (!collision.CheckCollision(forwardCollision) && !collision.CheckCollision(groundCollision)) gDir = GravityDirection.NONE;


        if (gDir == GravityDirection.NONE)
        {
            if (collision.CheckCollision(CollisionDirection.LEFT)) gDir = GravityDirection.LEFT;
            else if (collision.CheckCollision(global::CollisionDirection.RIGHT)) gDir = GravityDirection.RIGHT;
            else if (collision.CheckCollision(CollisionDirection.UP)) gDir = GravityDirection.UP;
            else if (collision.CheckCollision(CollisionDirection.DOWN)) gDir = GravityDirection.DOWN;
        }

        UpdateGravityDirection();
    }
}
