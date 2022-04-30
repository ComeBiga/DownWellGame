using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [Header("Move Speed")]
    public float speed = 5f;
    public float jumpSpeed = 5f;

    [Header("Gravity")]
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    private float hInput = 0;
    private bool jumping = false;

    #region Initialization

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = gravity;        
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
        UpdateGravity();

        MoveHorizontal(hInput);
    }

    private void UpdateGravity()
    {
        // 최대속도
        if (rigidbody.velocity.y <= -maxFallSpeed) 
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);
    }

    private void MoveHorizontal(float hInput)
    {
        transform.position += Vector3.right * speed * hInput * Time.deltaTime;
    }

    private void MoveVertical(float yVel)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, yVel);
    }

    #endregion
}
