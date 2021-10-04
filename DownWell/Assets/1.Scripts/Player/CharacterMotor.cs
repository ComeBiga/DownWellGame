using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CharacterMotor : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public LayerMask groundLayerMask;

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    [Header("Shoot rebound")]
    public float shotReboundSpeed = 1f;
    public float reboundTime = 1f;

    [Space()]
    public int verticalRayCount = 4;
    float verticalRaySpacing;
    RaycastOrigins raycastOrigins;
    struct RaycastOrigins
    {
        public Vector2 bottomLeft, topLeft;
    }

    bool grounded = true;
    public bool Grounded { get { return grounded; } }
    bool jumping = false;
    public bool Jumping { get { return jumping; } }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = gravity;

        CalculateRaySpacing();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.y <= -maxFallSpeed) rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);

        grounded = VerticalCollisions();
    }

    void HorizontalMove()
    {
        UpdateRaycastOrigins();

        float h = Input.GetAxis("Horizontal");

        //rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);
        transform.position += Vector3.right * speed * h * Time.deltaTime;
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        jumping = true;
    }

    public void LeapOff(float stepUpSpeed)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, stepUpSpeed);
        jumping = true;
    }

    public void KnuckBack(float knuckBackSpeed, int direction)
    {
        //rigidbody.velocity = new Vector2(knuckBackSpeed * direction, rigidbody.velocity.y + knuckBackSpeed);

        //StartCoroutine(KnuckBacking(knuckBackSpeed, direction));

        rigidbody.AddForce(new Vector2(knuckBackSpeed * direction, knuckBackSpeed), ForceMode2D.Impulse);
        //rigidbody.AddRelativeForce(new Vector2(knuckBackSpeed * direction, knuckBackSpeed), ForceMode2D.Impulse);
    }

    #region Collision Check
    bool VerticalCollisions()
    {
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

            Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    #endregion
}
