using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private CollisionCheck wallCollision;

    [Header("Move Speed")]
    public float speed = 5f;
    public float jumpSpeed = 5f;

    [Header("Gravity")]
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    [Header("Collision")]
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private float hInput = 0;
    private bool jumping = false;
    private bool grounded = false;

    #region Initialization

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = gravity;

        wallCollision = new CollisionCheck();
        wallCollision.Init(GetComponent<BoxCollider2D>(),
                        .1f,
                        horizontalRayCount,
                        verticalRayCount,
                        LayerMask.GetMask("Ground")
                        );
        wallCollision.CalculateRaySpacing();
    }

    #endregion

    #region Public Method

    public void Move(float hInput)
    {
        this.hInput = hInput;
    }

    public void Jump()
    {
        if(grounded)
        {
            MoveVertical(jumpSpeed);
            jumping = true;
        }
    }

    public void JumpCancel()
    {
        var yVel = rigidbody.velocity.y;

        if(yVel > 0)
        {
            MoveVertical(yVel / 2);
        }
    }

    public void LeapOff(float speed)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, speed);
        GetComponent<PlayerController>().jumping = true;
    }

    #endregion

    #region Private Method

    private void Update()
    {
        wallCollision.UpdateRaycastOrigins();

        UpdateGravity();

        MoveHorizontal(hInput);

        CheckGround();
    }

    private void UpdateGravity()
    {
        // 최대속도
        if (rigidbody.velocity.y <= -maxFallSpeed) 
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);
    }

    private void MoveHorizontal(float hInput)
    {
        if (!wallCollision.CheckCollision(CollisionDirection.LEFT) &&
            !wallCollision.CheckCollision(CollisionDirection.RIGHT))
        {
            transform.position += Vector3.right * speed * hInput * Time.deltaTime;
        }
    }

    private void MoveVertical(float yVel)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, yVel);
    }

    private void CheckGround()
    {
        if (wallCollision.CheckCollision(CollisionDirection.DOWN))
            grounded = true;
        else
            grounded = false;
    }

    #endregion
}
