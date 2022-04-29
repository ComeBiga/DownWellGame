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


    #region Initialization

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = gravity;
    }

    #endregion

    #region Public Method

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
    }

    private void UpdateGravity()
    {
        // 최대속도
        if (rigidbody.velocity.y <= -maxFallSpeed) 
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);
    }

    private void MoveHorizontal()
    {

    }

    #endregion
}
