using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyWormMovement : JellyManMovement
{
    public float jumpSpeed = 3f;
    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        collision.Init(GetComponent<BoxCollider2D>(), rayLength, horizontalRayCount, verticalRayCount, groundLayermask);
        collision.CalculateRaySpacing();

        SetStartDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        if (jumping)
        {
            collision.UpdateRaycastOrigins();

            if (collision.CheckCollision(CollisionDirection.DOWN) && rigidbody.velocity.y < 0)
            {
                Land();
            }

            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        else
        {
            timer += Time.deltaTime;

            if(timer > changeTime)
            {
                Jump();
                timer = 0;
            }

            MoveAsCollision();
        }
    }

    void Jump()
    {
        jumping = true;
        rigidbody.velocity = new Vector2(0, jumpSpeed);
    }

    void Land()
    {
        jumping = false;
        dir *= -1;
        Debug.Log("Land");
    }
}
