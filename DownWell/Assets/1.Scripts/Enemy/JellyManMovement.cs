using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyManMovement : EnemyMovement
{
    [Header("JellyMan Value")]
    public bool moveAsCollision = false;

    public enum Direction { LEFT, RIGHT }
    public Direction startDirection = Direction.LEFT;
    int dir = -1;

    public float changeTime = 3f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
        collision.CalculateRaySpacing();

        SetStartDirection();
    }

    void SetStartDirection()
    {
        switch(startDirection)
        {
            case Direction.LEFT:
                dir = -1;
                break;
            case Direction.RIGHT:
                dir = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        if(moveAsCollision) MoveAsCollision();
        else MoveAsTime();
    }

    void MoveAsCollision()
    {
        collision.UpdateRaycastOrigins();

        if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
        else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

        rigidbody.velocity = new Vector2(speed * dir, rigidbody.velocity.y);
    }

    void MoveAsTime()
    {
        timer += Time.deltaTime;

        if(timer >= changeTime)
        {
            dir *= -1;

            timer = 0;
        }

        rigidbody.velocity = new Vector2(speed * dir, rigidbody.velocity.y);
    }
}
