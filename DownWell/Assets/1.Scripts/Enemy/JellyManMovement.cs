using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyManMovement : EnemyMovement
{
    int dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);

        collision.CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        collision.UpdateRaycastOrigins();

        if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
        else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

        rigidbody.velocity = new Vector2(speed * dir, rigidbody.velocity.y);
    }
}
