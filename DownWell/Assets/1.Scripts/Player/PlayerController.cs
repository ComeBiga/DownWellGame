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
    bool jumping = false;

    bool shootable = true;
    bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.gravityScale = gravity;

        CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.gravityScale = gravity;
        //Debug.Log(rigidbody.velocity.y);

        if (rigidbody.velocity.y <= -maxFallSpeed) rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxFallSpeed);

        HorizontalMove();

        //grounded = CheckTileUnderPlayer(groundLayerMask);
        grounded = VerticalCollisions();

        if (Input.GetButtonDown("Jump") && grounded)
            Jump();

        Shoot();
    }

    void Shoot()
    {
        if (Input.GetButtonUp("Jump"))
        {
            shootable = true;
        }

        if (shootable && Input.GetButtonDown("Jump"))
        {
            shooting = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        }
        if (shooting && Input.GetButton("Jump"))
        {
            GetComponent<PlayerAttack>().Shoot();
        }

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

        float h = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        jumping = true;
    }

    public void LeapOff(float leapSpeed)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, leapSpeed);
        jumping = true;
    }

    public void KnuckBack(float knuckBackSpeed, int direction)
    {
        //rigidbody.velocity = new Vector2(knuckBackSpeed * direction, rigidbody.velocity.y);

        StartCoroutine(KnuckBacking(knuckBackSpeed, direction));
    }

    IEnumerator KnuckBacking(float knuckBackSpeed, int direction)
    {
        float duration = .3f;
        float elapse = 0;

        while(elapse < duration)
        {
            transform.position = new Vector3(transform.position.x + knuckBackSpeed * direction * Time.deltaTime, transform.position.y, transform.position.z);

            elapse += Time.deltaTime;

            yield return null;
        }
    }

    bool CheckTileUnderPlayer(LayerMask checkLayer)
    {
        float rayDistance = .1f;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.size.y / 2);
        RaycastHit2D[] results = Physics2D.RaycastAll(origin, Vector2.down, rayDistance, checkLayer);

        Debug.DrawRay(origin, Vector3.down * rayDistance, Color.green);

        if (results.Length > 0) return true;

        return false;
    }

    public void ShotRebound()
    {
        //rigidbody.velocity += Vector2.up * 0;
        rigidbody.velocity += Vector2.up * shotReboundSpeed;
    }

    bool VerticalCollisions()
    {
        for(int i = 0; i< verticalRayCount; i++)
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
}
