using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics
{
    private Transform transform;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private CollisionCheck collision;

    [Header("Move Speed")]
    public float speed = 5f;
    public float jumpSpeed = 5f;

    [Header("Gravity")]
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    [Header("Collision")]
    public LayerMask groundLayer;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private float hInput = 0;
    private float velocity = 0;
    private bool jumping = false;
    private bool grounded = false;

    public bool Grounded { get { return grounded; } }
    public event System.Action OnGrounded;

    public EnemyPhysics(Transform transform, Rigidbody2D rigidbody, BoxCollider2D boxCollider)
    {
        speed = 0f;
        gravity = 3f;
        maxFallSpeed = 30f;

        this.transform = transform;

        this.rigidbody = rigidbody;
        this.rigidbody.gravityScale = gravity;

        this.boxCollider = boxCollider;
    }

    public void InitCollision(int horizontalRayCount, int verticalRayCount, LayerMask layerMask)
    {
        collision = new CollisionCheck();
        collision.Init(boxCollider,
            .1f,
            horizontalRayCount,
            verticalRayCount,
            layerMask
            );

        collision.CalculateRaySpacing();
    }

    public void SetVelocity(float vel)
    {
        velocity = vel;
    }

    public void Update()
    {
        collision.UpdateRaycastOrigins();

        UpdateGravity();

        MoveHorizontal(velocity);

        CheckGround();
    }

    private void UpdateGravity()
    {
        // �ִ�ӵ�
        if (rigidbody.velocity.y <= -maxFallSpeed)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);
    }

    private void MoveHorizontal(float velocity)
    {
        if (!CheckHorizontalCollision(velocity))
            transform.position += Vector3.right * velocity * Time.deltaTime;
    }

    private void MoveVertical(float velocity)
    {

    }

    private void CheckGround()
    {
        if (collision.CheckCollision(CollisionDirection.DOWN, OnGround))
            grounded = true;
        else
            grounded = false;
    }

    private void OnGround(RaycastHit2D hit)
    {
        if (!grounded)
        {
            if (OnGrounded != null) OnGrounded.Invoke();
        }
    }

    private bool CheckHorizontalCollision(float hInput)
    {
        if (hInput > 0)
            return collision.CheckCollision(CollisionDirection.RIGHT);
        else if (hInput < 0)
            return collision.CheckCollision(CollisionDirection.LEFT);
        else
            return false;
    }
}
