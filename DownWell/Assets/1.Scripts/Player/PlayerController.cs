using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{
    InputManager input;
    Rigidbody2D rigidbody;
    PlayerPhysics physics;
    PlayerAttack attack;

    //public LayerMask groundLayerMask;

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 1f;
    public float maxFallSpeed = 10f;

    //[Space()]
    //public int horizontalRayCount = 4;
    //public int verticalRayCount = 4;
    //float horizontalRaySpacing;
    //float verticalRaySpacing;

    //RaycastOrigins raycastOrigins;
    //struct RaycastOrigins
    //{
    //    public Vector2 bottomLeft, topLeft;
    //    public Vector2 bottomRight, topRight;
    //}

    [HideInInspector] public bool cantMove = false;
    //bool grounded = true; 
    //public bool Grounded { get { return grounded; } }
    [HideInInspector] public bool jumping = false;

    //bool shootable = true;
    bool shooting = false;

    float normalSpeed;
    float normalJumpSpeed;
    float splashedSpeed;
    float splashedJumpSpeed;

    //#region EventCallback
    //public event System.Action OnGrounded;
    //#endregion

    // Start is called before the first frame update
    void Start()
    {
        input = InputManager.instance;
        rigidbody = GetComponent<Rigidbody2D>();
        physics = GetComponent<PlayerPhysics>();
        attack = GetComponent<PlayerAttack>();

        rigidbody.gravityScale = gravity;

        //CalculateRaySpacing();

        normalSpeed = speed;
        normalJumpSpeed = jumpSpeed;
        splashedSpeed = speed * .7f;
        splashedJumpSpeed = jumpSpeed * .7f;

        //OnGrounded += () => { GetComponent<Effector>().Generate("Land"); };
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody.gravityScale = gravity;

        // 최대 속도
        //if (rigidbody.velocity.y <= -maxFallSpeed) rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);

        HorizontalMove();
        MoveOnSplashed();

        //CheckGroundForEvent();
        //grounded = GroundCollision();

        Jump();

        Shoot();
    }

    void Shoot()
    {
        if (GetComponent<PlayerCombatStepping>().ShotLock) return;

        if (physics.Grounded)
        {
            jumping = false;
            attack.weapon.shootable = false;
            shooting = false;
        }
        else
        {
            attack.weapon.shootable = true;
        }

        if (input.GetJumpButtonUp())
        {
            attack.weapon.shootable = true;
        }

        if (attack.weapon.shootable && input.GetJumpButtonDown())
        {
            shooting = true;
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        }
        if (shooting && input.GetJumpButton())
        {
            //GetComponent<PlayerCombat>().Shoot();
            GetComponent<PlayerAttack>().Shoot();
        }

//#if UNITY_EDITOR || UNITY_STANDALONE_WIN
//        if (InputManager.instance.mouseClick)
//        {
//            if (InputManager.instance.GetJumpButtonUp())
//            {
//                shootable = true;
//            }

//            if (shootable && InputManager.instance.GetJumpButtonDown())
//            {
//                shooting = true;
//                //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
//            }
//            if (shooting && InputManager.instance.GetJumpButton())
//            {
//                //GetComponent<PlayerCombat>().Shoot();
//                GetComponent<PlayerAttack>().Shoot();
//            }
//        }
//        else
//        {
//            if (Input.GetButtonUp("Jump"))
//            {
//                shootable = true;
//            }

//            if (shootable && Input.GetButtonDown("Jump"))
//            {
//                shooting = true;
//                //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
//            }
//            if (shooting && Input.GetButton("Jump"))
//            {
//                //GetComponent<PlayerCombat>().Shoot();
//                GetComponent<PlayerAttack>().Shoot();
//            }
//        }
//#elif UNITY_ANDROID 
//        if (InputManager.instance.GetJumpButtonUp())
//        {
//            shootable = true;
//        }

//        if (shootable && InputManager.instance.GetJumpButtonDown())
//        {
//            shooting = true;
//            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
//        }
//        if (shooting && InputManager.instance.GetJumpButton())
//        {
//            GetComponent<PlayerCombat>().Shoot();
//        }
//#endif

        
    }

    void HorizontalMove()
    {
        if (cantMove)
        {
            physics.Move(0);
            return;
        }

        physics.Move(input.Horizontal);

        //UpdateRaycastOrigins();

        //if (cantMove) return;

        //if (!HorizontalCollisions()) transform.position += Vector3.right * speed * input.Horizontal * Time.deltaTime;

//        float h = Input.GetAxis("Horizontal");

//#if UNITY_EDITOR || UNITY_STANDALONE_WIN
//        if (InputManager.instance.mouseClick)
//        {
//            if (!HorizontalCollisions()) transform.position += Vector3.right * speed * InputManager.instance.horizontal * Time.deltaTime;
//        }
//        else
//        {
//            if (!HorizontalCollisions()) transform.position += Vector3.right * speed * h * Time.deltaTime;
//        }
//#elif UNITY_ANDROID
//        if (!HorizontalCollisions()) transform.position += Vector3.right * speed * InputManager.instance.horizontal * Time.deltaTime;
//#endif
    }

    void Jump()
    {
        if (cantMove) return;

        // Basic Jump
        if(input.GetJumpButtonDown())
        {
            physics.Jump();
        }

        // Short Jump(Jump Canceling)
        if(input.GetJumpButtonUp())
        {
            physics.JumpCancel();
        }
        
        //// Basic Jump
        //if (input.GetJumpButtonDown() && grounded)
        //{
        //    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        //    jumping = true;
        //}

        //// Jump Cancel (Short Jump)
        //if (rigidbody.velocity.y > 0 && input.GetJumpButtonUp())
        //{
        //    rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
        //    jumping = false;
        //}

//#if UNITY_EDITOR || UNITY_STANDALONE_WIN
//        if (InputManager.instance.mouseClick)
//        {
//            if (InputManager.instance.GetJumpButtonDown() && grounded)
//            {
//                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
//                jumping = true;
//            }

//            if (rigidbody.velocity.y > 0 && InputManager.instance.GetJumpButtonUp())
//            {
//                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
//                jumping = false;
//            }
//        }
//        else
//        {
//            if (Input.GetButtonDown("Jump") && grounded)
//            {
//                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
//                jumping = true;

//                GetComponent<Effector>().Generate("Jump");
//            }

//            if (rigidbody.velocity.y > 0 && Input.GetButtonUp("Jump"))
//            {
//                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
//                jumping = false;
//            }
//        }
//#elif UNITY_ANDROID
//        if (InputManager.instance.GetJumpButtonDown() && grounded)
//        {
//            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
//            jumping = true;
//        }

//        if (rigidbody.velocity.y > 0 && InputManager.instance.GetJumpButtonUp())
//        {
//            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
//            jumping = false;
//        }
//#endif

    }

    public void MoveOnSplashed()
    {
        RaycastHit2D hit;

        if(physics.wallCollision.CheckCollision(CollisionDirection.DOWN, out hit))
        {
            if (hit.transform.GetComponent<BeSplashed>() != null && hit.transform.GetComponent<BeSplashed>().splashed)
            {
                speed = splashedSpeed;
                jumpSpeed = splashedJumpSpeed;
                return;
            }
        }
        else
        {
            speed = normalSpeed;
            jumpSpeed = normalJumpSpeed;
        }

        //for (int i = 0; i < verticalRayCount; i++)
        //{
        //    Vector2 rayOrigin = raycastOrigins.bottomLeft;
        //    rayOrigin += Vector2.right * (verticalRaySpacing * i);
        //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

        //    Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

        //    if (hit)
        //    {
        //        if(hit.transform.GetComponent<BeSplashed>() != null && hit.transform.GetComponent<BeSplashed>().splashed)
        //        {
        //            speed = splashedSpeed;
        //            jumpSpeed = splashedJumpSpeed;
        //            return;
        //        }
        //    }
        //}

        //speed = normalSpeed;
        //jumpSpeed = normalJumpSpeed;
    }

    #region Collision(Deprecated)
//    public void CheckGroundForEvent()
//    {
//        if(grounded == false)
//        {
//            if(GroundCollision() == true)
//            {
//                OnGrounded.Invoke();
//            }
//        }
//    }

//    public bool GroundCollision()
//    {
//        for(int i = 0; i < verticalRayCount; i++)
//        {
//            Vector2 rayOrigin = raycastOrigins.bottomLeft;
//            rayOrigin += Vector2.right * (verticalRaySpacing * i);
//            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, groundLayerMask);

//            //Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

//            if (hit) return true;
//        }

//        return false;
//    }

//    public bool OneSidePlatformCollision()
//    {
//        for(int i = 0; i < verticalRayCount; i++)
//        {
//            Vector2 rayOrigin = raycastOrigins.topLeft;
//            rayOrigin += Vector2.right * (verticalRaySpacing * i);
//            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, .1f);

//            Debug.DrawRay(rayOrigin, Vector2.up * .1f, Color.red);

//            if (hit) return true;
//        }

//        return false;
//    }

//    public bool HorizontalCollisions()
//    {
//        float directionX;
//#if UNITY_STANDALONE_WIN
//        directionX = Mathf.Sign(Input.GetAxis("Horizontal"));
//#endif
//#if UNITY_EDITOR
//        if (InputManager.instance.mouseClick)
//            directionX = Mathf.Sign(InputManager.instance.horizontal);
//        else
//            directionX = Mathf.Sign(Input.GetAxis("Horizontal"));
//#elif UNITY_ANDROID
//        directionX = Mathf.Sign(InputManager.instance.horizontal);
//#endif

//        for (int i = 0; i < horizontalRayCount; i++)
//        {
//            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
//            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
//            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, .1f, groundLayerMask);
//            //Debug.Log(hit.transform.name);

//            Debug.DrawRay(rayOrigin, Vector2.right * directionX * .1f, Color.red);

//            if (hit) return true;
//        }

//        return false;
//    }

//    public void UpdateRaycastOrigins()
//    {
//        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

//        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
//        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
//        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
//        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
//    }

//    public void CalculateRaySpacing()
//    {
//        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

//        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
//        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

//        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
//        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
//    }

    #endregion
}
