using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [HideInInspector] public CollisionCheck wallCollision;

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
    private bool jumping = false;
    private bool grounded = false;

    public bool Grounded { get { return grounded; } }
    public event System.Action OnGrounded;

    #region Initialization

    private void Start()
    {
        
    }

    public void Init()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = gravity;

        wallCollision = new CollisionCheck();
        wallCollision.Init(GetComponent<BoxCollider2D>(),
                        .1f,
                        horizontalRayCount,
                        verticalRayCount,
                        groundLayer
                        );
        wallCollision.CalculateRaySpacing();
    }

    public void InitVelocity()
    {
        rigidbody.velocity = Vector3.zero;
    }

    #endregion

    #region Public Method

    public void Move(float hInput)
    {
        this.hInput = hInput;
    }

    public void Jump(float additive = 1f)
    {
        if(grounded)
        {
            MoveVertical(jumpSpeed * additive);
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

    public void UseGravity(bool value)
    {
        if(value)
        {
            rigidbody.gravityScale = gravity;
        }
        else
        {
            rigidbody.gravityScale = 0f;
        }
    }

    #endregion

    #region Private Method

    private void Update()
    {
        if (PlayerManager.instance.playerObject == null) return;

        wallCollision.UpdateRaycastOrigins();

        UpdateGravity();

        MoveHorizontal(hInput);

        CheckGround();
    }

    private void UpdateGravity()
    {
        // �ִ�ӵ�
        if (rigidbody.velocity.y <= -maxFallSpeed) 
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);
    }

    private void MoveHorizontal(float hInput)
    {
        if(!CheckHorizontalCollision(hInput))
        {
            transform.position += Vector3.right * speed * hInput * Time.deltaTime;
            //rigidbody.velocity = new Vector2(speed * hInput, rigidbody.velocity.y);
        }
    }

    private void MoveVertical(float yVel)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, yVel);
    }

    private void CheckGround()
    {
        if (wallCollision.CheckCollision(CollisionDirection.DOWN, OnGround))
            grounded = true;
        else
            grounded = false;
    }

    private void OnGround(RaycastHit2D hit)
    {
        if(!grounded)
        {
            if(OnGrounded != null) OnGrounded.Invoke();
        }
    }

    private bool CheckHorizontalCollision(float hInput)
    {
        if (hInput > 0)
            return wallCollision.CheckCollision(CollisionDirection.RIGHT);
        else if (hInput < 0)
            return wallCollision.CheckCollision(CollisionDirection.LEFT);
        else
            return false;
    }

    #endregion
}
