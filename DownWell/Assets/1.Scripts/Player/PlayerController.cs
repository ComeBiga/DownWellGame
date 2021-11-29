using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public LayerMask groundLayerMask;

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    [Space()]
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    RaycastOrigins raycastOrigins;
    struct RaycastOrigins
    {
        public Vector2 bottomLeft, topLeft;
        public Vector2 bottomRight, topRight;
    }

    [HideInInspector] public bool cantMove = false;
    bool grounded = true; 
    public bool Grounded { get { return grounded; } }
    [HideInInspector] public bool jumping = false;

    bool shootable = true;
    bool shooting = false;

    float normalSpeed;
    float normalJumpSpeed;
    float splashedSpeed;
    float splashedJumpSpeed;

    #region EventCallback
    public event System.Action OnGrounded;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.gravityScale = gravity;

        CalculateRaySpacing();

        normalSpeed = speed;
        normalJumpSpeed = jumpSpeed;
        splashedSpeed = speed * .7f;
        splashedJumpSpeed = jumpSpeed * .7f;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.gravityScale = gravity;

        // 최대 속도
        if (rigidbody.velocity.y <= -maxFallSpeed) rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);

        HorizontalMove();
        MoveOnSplashed();

        CheckGroundForEvent();
        grounded = GroundCollision();

        Jump();

        Shoot();
    }

    private void FixedUpdate()
    {
        //HorizontalMove();
    }

    void Shoot()
    {
#if UNITY_EDITOR
        if (InputManager.instance.mouseClick)
        {
            if (InputManager.instance.GetJumpButtonUp())
            {
                shootable = true;
            }

            if (shootable && InputManager.instance.GetJumpButtonDown())
            {
                shooting = true;
                //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
            if (shooting && InputManager.instance.GetJumpButton())
            {
                GetComponent<PlayerCombat>().Shoot();
            }
        }
        else
        {
            if (Input.GetButtonUp("Jump"))
            {
                shootable = true;
            }

            if (shootable && Input.GetButtonDown("Jump"))
            {
                shooting = true;
                //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
            if (shooting && Input.GetButton("Jump"))
            {
                GetComponent<PlayerCombat>().Shoot();
            }
        }
#elif UNITY_ANDROID
        if (InputManager.instance.GetJumpButtonUp())
        {
            shootable = true;
        }

        if (shootable && InputManager.instance.GetJumpButtonDown())
        {
            shooting = true;
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        }
        if (shooting && InputManager.instance.GetJumpButton())
        {
            GetComponent<PlayerCombat>().Shoot();
        }
#endif

        if (grounded)
        {
            jumping = false;
            shootable = false;
            shooting = false;
        }
        else
        {
            shootable = true;
        }
    }

    void HorizontalMove()
    {
        UpdateRaycastOrigins();

        if (cantMove) return;
        float h = Input.GetAxis("Horizontal");
        //float h = Input.GetAxisRaw("Horizontal");

        //Debug.Log(HorizontalCollisions());
        //rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);
#if UNITY_EDITOR
        if (InputManager.instance.mouseClick)
        {
            if (!HorizontalCollisions()) transform.position += Vector3.right * speed * InputManager.instance.horizontal * Time.deltaTime;
        }
        else
        {
            if (!HorizontalCollisions()) transform.position += Vector3.right * speed * h * Time.deltaTime;
        }
#elif UNITY_ANDROID
        if (!HorizontalCollisions()) transform.position += Vector3.right * speed * InputManager.instance.horizontal * Time.deltaTime;
#endif
    }

    void Jump()
    {
#if UNITY_EDITOR
        if (InputManager.instance.mouseClick)
        {
            if (InputManager.instance.GetJumpButtonDown() && grounded)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
                jumping = true;
            }

            if (rigidbody.velocity.y > 0 && InputManager.instance.GetJumpButtonUp())
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
                jumping = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
                jumping = true;
            }

            if (rigidbody.velocity.y > 0 && Input.GetButtonUp("Jump"))
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
                jumping = false;
            }
        }
#elif UNITY_ANDROID
        if (InputManager.instance.GetJumpButtonDown() && grounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            jumping = true;
        }

        if (rigidbody.velocity.y > 0 && InputManager.instance.GetJumpButtonUp())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
            jumping = false;
        }
#endif

    }

    public void MoveOnSplashed()
    {
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

            Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

            if (hit)
            {
                if(hit.transform.GetComponent<BeSplashed>() != null && hit.transform.GetComponent<BeSplashed>().splashed)
                {
                    speed = splashedSpeed;
                    jumpSpeed = splashedJumpSpeed;
                    return;
                }
            }
        }

        speed = normalSpeed;
        jumpSpeed = normalJumpSpeed;
    }

    #region Collision
    public void CheckGroundForEvent()
    {
        if(grounded == false)
        {
            if(GroundCollision() == true)
            {
                OnGrounded.Invoke();
            }
        }
    }

    public bool GroundCollision()
    {
        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

            Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public bool OneSidePlatformCollision()
    {
        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, .1f);

            Debug.DrawRay(rayOrigin, Vector2.up * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public bool HorizontalCollisions()
    {
        float directionX;
#if UNITY_EDITOR
        if (InputManager.instance.mouseClick)
            directionX = Mathf.Sign(InputManager.instance.horizontal);
        else
            directionX = Mathf.Sign(Input.GetAxis("Horizontal"));
#elif UNITY_ANDROID
        directionX = Mathf.Sign(InputManager.instance.horizontal);
#endif

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, .1f, groundLayerMask);
            //Debug.Log(hit.transform.name);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }

    #endregion
}
