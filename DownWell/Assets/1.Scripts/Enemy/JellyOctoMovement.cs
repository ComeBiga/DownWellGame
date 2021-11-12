using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyOctoMovement : EnemyMovement
{
    bool adjustedPos = false;

    private float currentRayLength = 0;
    public float rayLengthOutRange = .5f;

    public float posMod = .1f;
    Vector2 gDirection = Vector2.right;

    global::CollisionDirection forwardCollision;
    global::CollisionDirection groundCollision;

    enum GravityDirection { NONE, UP, DOWN, LEFT, RIGHT }
    GravityDirection gDir;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);

        collision.CalculateRaySpacing();

        if (collision.CheckCollision(CollisionDirection.LEFT)) gDir = GravityDirection.LEFT;
        else if (collision.CheckCollision(CollisionDirection.RIGHT)) gDir = GravityDirection.RIGHT;
        else if (collision.CheckCollision(CollisionDirection.UP)) gDir = GravityDirection.UP;
        else if (collision.CheckCollision(CollisionDirection.DOWN)) gDir = GravityDirection.DOWN;
        else gDir = GravityDirection.NONE;

        UpdateGravityDirection();
    }

    void AdjustPositionOutOfRange()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                transform.position += Vector3.down * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
            case GravityDirection.UP:
                transform.position += Vector3.up * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
            case GravityDirection.LEFT:
                //Debug.Log("Left");
                transform.position += Vector3.left * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
            case GravityDirection.RIGHT:
                transform.position += Vector3.right * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!adjustedPos)
            AdjustPositionOutOfRange();

        collision.UpdateRaycastOrigins();

        //Debug.Log(gDir);
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

        if (GameManager.instance.CheckTargetRange(transform))
        {
            currentRayLength = rayLength;

            switch (gDir)
            {
                case GravityDirection.DOWN:
                    rigidbody.velocity = Vector2.right * speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.UP:
                    rigidbody.velocity = Vector2.left * speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.LEFT:
                    rigidbody.velocity = Vector2.down * speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.RIGHT:
                    rigidbody.velocity = Vector2.up * speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.NONE:
                    rigidbody.gravityScale = 3f;
                    break;
            }
        }
        else
        {
            currentRayLength = rayLengthOutRange;
            if (gDir == GravityDirection.NONE) rigidbody.gravityScale = 3f;
            else { rigidbody.gravityScale = 0f; rigidbody.velocity = Vector2.zero; }
        }
    }

    void ChangeDirectionCCW()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                gDir = GravityDirection.RIGHT;
                break;
            case GravityDirection.UP:
                gDir = GravityDirection.LEFT;
                break;
            case GravityDirection.LEFT:
                gDir = GravityDirection.DOWN;
                break;
            case GravityDirection.RIGHT:
                gDir = GravityDirection.UP;
                break;
        }
    }

    void ChangeDirectionCW()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                gDir = GravityDirection.LEFT;
                transform.position += Vector3.down * posMod;
                break;
            case GravityDirection.UP:
                gDir = GravityDirection.RIGHT;
                transform.position += Vector3.up * posMod;
                break;
            case GravityDirection.LEFT:
                gDir = GravityDirection.UP;
                transform.position += Vector3.left * posMod;
                break;
            case GravityDirection.RIGHT:
                gDir = GravityDirection.DOWN;
                transform.position += Vector3.right * posMod;
                break;
        }
    }

    void UpdateGravityDirection()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                forwardCollision = CollisionDirection.RIGHT;
                groundCollision = CollisionDirection.DOWN;
                break;
            case GravityDirection.UP:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                forwardCollision = CollisionDirection.LEFT;
                groundCollision = CollisionDirection.UP;
                break;
            case GravityDirection.LEFT:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                forwardCollision = CollisionDirection.DOWN;
                groundCollision = CollisionDirection.LEFT;
                break;
            case GravityDirection.RIGHT:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                forwardCollision = CollisionDirection.UP;
                groundCollision = CollisionDirection.RIGHT;
                break;
            case GravityDirection.NONE:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                forwardCollision = CollisionDirection.NONE;
                groundCollision = CollisionDirection.NONE;
                break;
        }
    }
}
