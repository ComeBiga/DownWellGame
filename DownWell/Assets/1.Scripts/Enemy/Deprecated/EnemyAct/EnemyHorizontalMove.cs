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

    public override void Init()
    {
        base.Init();

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public override bool Act(Rigidbody2D rigidbody)
    {
        //if (!GameManager.instance.CheckTargetRange(transform)) return false;

        if (moveAsCollision) MoveAsCollision();
        else MoveAsTime();

        Animation();

        return false;
    }

    protected override void EndAct()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void Animation()
    {
        GetComponent<SpriteRenderer>().flipX = (GetComponent<Rigidbody2D>().velocity.x < -0.01) ? true : false;
    }

    protected void MoveAsCollision()
    {
        collision.UpdateRaycastOrigins();

        if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
        else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, GetComponent<Rigidbody2D>().velocity.y);
    }

    protected void MoveAsTime()
    {
        timer += Time.deltaTime;

        if (timer >= changeTime)
        {
            dir *= -1;

            timer = 0;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dir, GetComponent<Rigidbody2D>().velocity.y);
    }
}
