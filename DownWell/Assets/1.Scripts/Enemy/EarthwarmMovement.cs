using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthwarmMovement : MonoBehaviour, IEnemyMoveValue
{
    Rigidbody2D rigidbody;
    CollisionCheck collision = new CollisionCheck();

    bool adjustedPos = false;

    private float currentRayLength = 0;
    public float rayLength = .1f;
    public float rayLengthOutRange = .5f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask groundLayermask;

    [HideInInspector] public float speed;
    public float Speed { set; get; }
    //public float gravity = 10f;

    public float posMod = .1f;
    Vector2 gDirection = Vector2.right;

    delegate bool CollisionDirection();
    CollisionDirection forwardCollision;
    CollisionDirection groundCollision;

    enum GravityDirection { NONE, UP, DOWN, LEFT, RIGHT }
    GravityDirection gDir;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        collision.Init(GetComponent<BoxCollider2D>(), horizontalRayCount, verticalRayCount);

        collision.CalculateRaySpacing();

        //gDir = GravityDirection.DOWN;
        //currentRayLength = rayLengthOutRange;

        if (LeftSideCollision()) gDir = GravityDirection.LEFT;
        else if (RightSideCollision()) gDir = GravityDirection.RIGHT;
        else if (UpSideCollision()) gDir = GravityDirection.UP;
        else if (DownSideCollision()) gDir = GravityDirection.DOWN;
        else gDir = GravityDirection.NONE;

        UpdateGravityDirection();
    }

    void OnEnable()
    {
        if (LeftSideCollision()) Debug.Log("Left");
        else if (RightSideCollision()) Debug.Log("Right");
        else if (UpSideCollision()) Debug.Log("Up");
        else if (DownSideCollision()) Debug.Log("Down");
    }

    void AdjustPositionOutOfRange()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                transform.position += Vector3.down * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
            case GravityDirection.UP:
                transform.position += Vector3.up * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
            case GravityDirection.LEFT:
                //Debug.Log("Left");
                transform.position += Vector3.left * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
            case GravityDirection.RIGHT:
                transform.position += Vector3.right * .03f;
                //currentRayLength = rayLength;
                adjustedPos = true;
                break;
        }
        //currentRayLength = rayLengthOutRange;

        //if (!LeftSideCollision() && !RightSideCollision() && !UpSideCollision() && !DownSideCollision()) return;

        //if (!DownSideCollision())
        //{
        //    if (LeftSideCollision()) StartCoroutine(AdjustPosition(Vector2.left));
        //    else if (RightSideCollision()) StartCoroutine(AdjustPosition(Vector2.right));
        //    else { currentRayLength = rayLength; adjustedPos = true; }
        //}
        //else
        //{
        //    currentRayLength = rayLength;
        //    adjustedPos = true;
        //}
    }

    IEnumerator AdjustPosition(Vector3 direction)
    {
        currentRayLength = rayLength;

        while (true)
        {
            if (LeftSideCollision() || RightSideCollision()) break;

            transform.position += direction * .001f;
            yield return null;
        }

        Debug.Log("1");
        adjustedPos = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!adjustedPos)
            AdjustPositionOutOfRange();

        collision.UpdateRaycastOrigins();

        //Debug.Log(gDir);
        if (forwardCollision()) ChangeDirectionCCW();
        if (!groundCollision()) ChangeDirectionCW();
        if (!forwardCollision() && !groundCollision()) gDir = GravityDirection.NONE;
        

        if (gDir == GravityDirection.NONE)
        {
            if (LeftSideCollision()) gDir = GravityDirection.LEFT;
            else if (RightSideCollision()) gDir = GravityDirection.RIGHT;
            else if (UpSideCollision()) gDir = GravityDirection.UP;
            else if (DownSideCollision()) gDir = GravityDirection.DOWN;
        }

        UpdateGravityDirection();

        if (GameManager.instance.CheckTargetRange(transform))
        {
            currentRayLength = rayLength;

            switch (gDir)
            {
                case GravityDirection.DOWN:
                    rigidbody.velocity = Vector2.right * Speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.UP:
                    rigidbody.velocity = Vector2.left * Speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.LEFT:
                    rigidbody.velocity = Vector2.down * Speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.RIGHT:
                    rigidbody.velocity = Vector2.up * Speed;
                    rigidbody.gravityScale = 0f;
                    break;
                case GravityDirection.NONE:
                    rigidbody.gravityScale = 3f;
                    break;
            }
        }
        else
        {
            currentRayLength = rayLengthOutRange;
            if (gDir == GravityDirection.NONE) rigidbody.gravityScale = 3f;
            else { rigidbody.gravityScale = 0f; rigidbody.velocity = Vector2.zero; }
        }
    }

    public bool RightSideCollision()
    {
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = collision.raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (collision.horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, currentRayLength, groundLayermask);
            //Debug.Log(hit.transform.name);

            Debug.DrawRay(rayOrigin, Vector2.right * currentRayLength, Color.red);
            
            if (hit) return true;
        }

        return false;
    }

    public bool LeftSideCollision()
    {
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = collision.raycastOrigins.bottomLeft;
            rayOrigin += Vector2.up * (collision.horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.left, currentRayLength, groundLayermask);
            //Debug.Log(hit.transform.name);

            Debug.DrawRay(rayOrigin, Vector2.left * currentRayLength, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public bool DownSideCollision()
    {
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = collision.raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (collision.verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, currentRayLength, groundLayermask);

            Debug.DrawRay(rayOrigin, Vector2.down * currentRayLength, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public bool UpSideCollision()
    {
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = collision.raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (collision.verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, currentRayLength, groundLayermask);

            Debug.DrawRay(rayOrigin, Vector2.up * currentRayLength, Color.red);

            if (hit) return true;
        }

        return false;
    }

    void ChangeDirectionCCW()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                gDir = GravityDirection.RIGHT;
                break;
            case GravityDirection.UP:
                gDir = GravityDirection.LEFT;
                break;
            case GravityDirection.LEFT:
                gDir = GravityDirection.DOWN;
                break;
            case GravityDirection.RIGHT:
                gDir = GravityDirection.UP;
                break;
        }
    }

    void ChangeDirectionCW()
    {
        switch (gDir)
        {
            case GravityDirection.DOWN:
                gDir = GravityDirection.LEFT;
                transform.position += Vector3.down * posMod;
                break;
            case GravityDirection.UP:
                gDir = GravityDirection.RIGHT;
                transform.position += Vector3.up * posMod;
                break;
            case GravityDirection.LEFT:
                gDir = GravityDirection.UP;
                transform.position += Vector3.left * posMod;
                break;
            case GravityDirection.RIGHT:
                gDir = GravityDirection.DOWN;
                transform.position += Vector3.right * posMod;
                break;
        }
    }

    void UpdateGravityDirection()
    {
        switch(gDir)
        {
            case GravityDirection.DOWN:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                forwardCollision = RightSideCollision;
                groundCollision = DownSideCollision;
                break;
            case GravityDirection.UP:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                forwardCollision = LeftSideCollision;
                groundCollision = UpSideCollision;
                break;
            case GravityDirection.LEFT:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                forwardCollision = DownSideCollision;
                groundCollision = LeftSideCollision;
                break;
            case GravityDirection.RIGHT:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                forwardCollision = UpSideCollision;
                groundCollision = RightSideCollision;
                break;
            case GravityDirection.NONE:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                forwardCollision = () => { return false; };
                groundCollision = () => { return false; };
                break;
        }
    }
}
