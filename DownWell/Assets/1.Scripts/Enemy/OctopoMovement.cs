using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopoMovement : MonoBehaviour, IEnemyMoveValue
{
    Rigidbody2D rigidbody;

    [HideInInspector] public float speed = 5f;
    public float Speed { get; set; }
    int dir = 1;

    CollisionCheck collision = new CollisionCheck();
    public float rayLength = .1f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask groundLayermask;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        collision.Init(GetComponent<BoxCollider2D>(), horizontalRayCount, verticalRayCount);

        collision.CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.CheckTargetRange(transform)) return;

        collision.UpdateRaycastOrigins();

        if (collision.CheckCollision(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckCollision(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;
        else if (collision.CheckEndOfGround(CollisionDirection.RIGHT, rayLength, groundLayermask)) dir = -1;
        else if (collision.CheckEndOfGround(CollisionDirection.LEFT, rayLength, groundLayermask)) dir = 1;

        rigidbody.velocity = new Vector2(Speed * dir, rigidbody.velocity.y);
    }

    //public bool RightSideCollision()
    //{
    //    for (int i = 0; i < horizontalRayCount; i++)
    //    {
    //        Vector2 rayOrigin = collision.raycastOrigins.bottomRight;
    //        rayOrigin += Vector2.up * (collision.horizontalRaySpacing * i);
    //        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, groundLayermask);
    //        //Debug.Log(hit.transform.name);

    //        Debug.DrawRay(rayOrigin, Vector2.right * rayLength, Color.red);

    //        if (hit) return true;
    //    }

    //    return false;
    //}

    //public bool LeftSideCollision()
    //{
    //    for (int i = 0; i < horizontalRayCount; i++)
    //    {
    //        Vector2 rayOrigin = collision.raycastOrigins.bottomLeft;
    //        rayOrigin += Vector2.up * (collision.horizontalRaySpacing * i);
    //        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.left, rayLength, groundLayermask);
    //        //Debug.Log(hit.transform.name);

    //        Debug.DrawRay(rayOrigin, Vector2.left * rayLength, Color.red);

    //        if (hit) return true;
    //    }

    //    return false;
    //}

    //public bool CheckRightEndOfGround()
    //{
    //    Vector2 rayOrigin = collision.raycastOrigins.bottomLeft;
    //    rayOrigin += Vector2.right * (collision.verticalRaySpacing * (verticalRayCount - 1));
    //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayermask);

    //    Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

    //    if (hit) return false;
    //    else return true;
    //}

    //public bool CheckLeftEndOfGround()
    //{
    //    Vector2 rayOrigin = collision.raycastOrigins.bottomLeft;
    //    rayOrigin += Vector2.right * (collision.verticalRaySpacing * 0);
    //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayermask);

    //    Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

    //    if (hit) return false;
    //    else return true;
    //}
}
