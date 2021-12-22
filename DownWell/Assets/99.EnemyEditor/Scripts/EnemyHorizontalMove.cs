using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMove : EnemyCollisionMove
{
    [Header("Move Value")]
    public bool moveAsCollision = false;

    public float speed = 1f;
    public enum Direction { LEFT, RIGHT }
    public Direction startDirection = Direction.LEFT;
    protected int dir = -1;

    public float changeTime = 3f;
    protected float timer = 0;

    private void Start()
    {
        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
        collision.CalculateRaySpacing();

        SetStartDirection();
    }

    protected void SetStartDirection()
    {
        switch (startDirection)
        {
            case Direction.LEFT:
                dir = -1;
                break;
            case Direction.RIGHT:
                dir = 1;
                break;
        }
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return false;

        if (moveAsCollision) MoveAsCollision(rigidbody);
        else MoveAsTime(rigidbody);

        Animation();

        return false;
    }

    void Animation()
    {
        GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
    }

    protected void MoveAsCollision(Rigidbody2D rigidbody)
    {
        collision.UpdateRaycastOrigins();

        if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
        else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

        rigidbody.velocity = new Vector2(speed * dir, rigidbody.velocity.y);
    }

    protected void MoveAsTime(Rigidbody2D rigidbody)
    {
        timer += Time.deltaTime;

        if (timer >= changeTime)
        {
            dir *= -1;

            timer = 0;
        }

        rigidbody.velocity = new Vector2(speed * dir, rigidbody.velocity.y);
    }
}
