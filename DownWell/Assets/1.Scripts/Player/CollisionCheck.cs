using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionDirection { NONE, HORIZONTAL, UP, DOWN, LEFT, RIGHT }

public class CollisionCheck : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    public struct CollisionInfo
    {
        public int rayCount;
        public Vector2 raycastOrigin;
        public Vector2 raySpacingDir;
        public float raySpacing;
        public Vector2 rayDir;
        public float rayLength;
        public LayerMask layerMask;
    }
    private CollisionInfo info;

    public float rayLength = .1f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public float horizontalRaySpacing;
    public float verticalRaySpacing;
    public LayerMask groundLayermask;

    public RaycastOrigins raycastOrigins;
    public struct RaycastOrigins
    {
        public Vector2 bottomLeft, topLeft;
        public Vector2 bottomRight, topRight;
    }

    delegate Vector2 GetRaycastOrigin();
    public delegate void CollisionOption(RaycastHit2D hit);

    public void Init(BoxCollider2D boxCollider, float rayLength, int horizontalRayCount, int verticalRayCount, LayerMask groundLayermask)
    {
        this.boxCollider = boxCollider;
        this.rayLength = rayLength;
        this.horizontalRayCount = horizontalRayCount;
        this.verticalRayCount = verticalRayCount;
        this.groundLayermask = groundLayermask;

        info.rayLength = rayLength;
        info.layerMask = groundLayermask;
    }

    private RaycastHit2D Raycast(int index, GetRaycastOrigin getOrigin, Vector2 raySpacingDirection, float raySpacing, Vector2 rayDirection, float rayLength, LayerMask layerMask)
    {
        Vector2 rayOrigin = getOrigin();
        rayOrigin += raySpacingDirection * (raySpacing * index);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, layerMask);

        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

        return hit;
    }
    
    private RaycastHit2D Raycast(int index, CollisionInfo info)
    {
        Vector2 rayOrigin = info.raycastOrigin;
        rayOrigin += info.raySpacingDir * (info.raySpacing * index);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, info.rayDir, info.rayLength, info.layerMask);

        Debug.DrawRay(rayOrigin, info.rayDir * info.rayLength, Color.red);

        return hit;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    private bool RayCastCollision(CollisionInfo info)
    {
        return RayCastCollision(info,
                                (hit) => {}
                                );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="OnCollision"></param>
    /// <returns></returns>
    private bool RayCastCollision(CollisionInfo info, CollisionOption OnCollision)
    {
        for(int i = 0; i < info.rayLength; i++)
        {
            RaycastHit2D hit = Raycast(i, info);

            if (hit)
            {
                OnCollision(hit);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rayCount"></param>
    /// <param name="getOrigin"></param>
    /// <param name="raySpacingDirection"></param>
    /// <param name="raySpacing"></param>
    /// <param name="rayDirection"></param>
    /// <param name="rayLength"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    private bool RayCastCollision(int rayCount, GetRaycastOrigin getOrigin, Vector2 raySpacingDirection, float raySpacing, Vector2 rayDirection, float rayLength, LayerMask layerMask)
    {
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = getOrigin();
            rayOrigin += raySpacingDirection * (raySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, layerMask);

            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

            if (hit) return true;
        }

        return false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rayCount"></param>
    /// <param name="getOrigin"></param>
    /// <param name="raySpacingDirection"></param>
    /// <param name="raySpacing"></param>
    /// <param name="rayDirection"></param>
    /// <param name="rayLength"></param>
    /// <param name="layerMask"></param>
    /// <param name="OnCollision"></param>
    /// <returns></returns>
    private bool RayCastCollision(int rayCount, GetRaycastOrigin getOrigin, Vector2 raySpacingDirection, float raySpacing, Vector2 rayDirection, float rayLength, LayerMask layerMask, CollisionOption OnCollision)
    {
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = getOrigin();
            rayOrigin += raySpacingDirection * (raySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, layerMask);

            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

            if (hit)
            {
                OnCollision(hit);
                return true;
            }
        }

        return false;
    }

    public bool CheckCollision(CollisionDirection direction)
    {
        //return CheckCollision(direction, rayLength, groundLayermask);
        return CheckCollision(direction, info, (hit)=> { });
    }

    public bool CheckCollision(CollisionDirection direction, CollisionOption OnCollision)
    {
        //return CheckCollision(direction, rayLength, groundLayermask, OnCollision);
        return CheckCollision(direction, info, OnCollision);
    }

    public bool CheckCollision(CollisionDirection direction, CollisionInfo info, CollisionOption OnCollision)
    {
        if (SetAsDirection(direction, info) == false) return false;

        return RayCastCollision(info, OnCollision);
    }

    public bool CheckCollision(CollisionDirection direction, float rayLength, LayerMask layerMask)
    {
        int rayCount;
        Vector2 raycastOrigin = new Vector2();
        Vector2 raySpacingDir = new Vector2();
        float raySpacing;
        Vector2 rayDir = new Vector2();

        if (SetAsDirection(direction,
                            out rayCount,
                            out raycastOrigin,
                            out raySpacingDir,
                            out raySpacing,
                            out rayDir
                            ) == false) return false;


        //switch (direction)
        //{
        //    case CollisionDirection.UP:
        //        rayCount = verticalRayCount;
        //        raycastOrigin = raycastOrigins.topLeft;
        //        raySpacingDir = Vector2.right;
        //        raySpacing = verticalRaySpacing;
        //        rayDir = Vector2.up;
        //        break;
        //    case CollisionDirection.DOWN:
        //        rayCount = verticalRayCount;
        //        raycastOrigin = raycastOrigins.bottomLeft;
        //        raySpacingDir = Vector2.right;
        //        raySpacing = verticalRaySpacing;
        //        rayDir = Vector2.down;
        //        break;
        //    case CollisionDirection.LEFT:
        //        rayCount = horizontalRayCount;
        //        raycastOrigin = raycastOrigins.bottomLeft;
        //        raySpacingDir = Vector2.up;
        //        raySpacing = horizontalRaySpacing;
        //        rayDir = Vector2.left;
        //        break;
        //    case CollisionDirection.RIGHT:
        //        rayCount = horizontalRayCount;
        //        raycastOrigin = raycastOrigins.bottomRight;
        //        raySpacingDir = Vector2.up;
        //        raySpacing = horizontalRaySpacing;
        //        rayDir = Vector2.right;
        //        break;
        //    case CollisionDirection.NONE:
        //        return false;
        //    default:
        //        Debug.LogError("Wrong CollisionDirection");
        //        return false;
        //}

        return RayCastCollision(rayCount,
                                () => { return raycastOrigin; },
                                raySpacingDir,
                                raySpacing,
                                rayDir,
                                rayLength,
                                layerMask
                                );

        //for (int i = 0; i < rayCount; i++)
        //{
        //    Vector2 rayOrigin = raycastOrigin;
        //    rayOrigin += raySpacingDir * (raySpacing * i);
        //    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDir, rayLength, layerMask);

        //    Debug.DrawRay(rayOrigin, rayDir * rayLength, Color.red);

        //    if (hit) return true;
        //}

        //return false;
    }

    public bool CheckCollision(CollisionDirection direction, float rayLength, LayerMask layerMask, CollisionOption OnCollision)
    {
        int rayCount;
        Vector2 raycastOrigin = new Vector2();
        Vector2 raySpacingDir = new Vector2();
        float raySpacing;
        Vector2 rayDir = new Vector2();

        if (SetAsDirection(direction,
                            out rayCount,
                            out raycastOrigin,
                            out raySpacingDir,
                            out raySpacing,
                            out rayDir
                            ) == false) return false;

        return RayCastCollision(rayCount,
                                () => { return raycastOrigin; },
                                raySpacingDir,
                                raySpacing,
                                rayDir,
                                rayLength,
                                layerMask,
                                OnCollision
                                );
    }

    private bool SetAsDirection(CollisionDirection direction, CollisionInfo info)
    {
        return SetAsDirection(direction,
                        out info.rayCount,
                        out info.raycastOrigin,
                        out info.raySpacingDir,
                        out info.raySpacing,
                        out info.rayDir
                        );
    }

    private bool SetAsDirection(CollisionDirection direction, out int rayCount, out Vector2 raycastOrigin, out Vector2 raySpacingDir, out float raySpacing, out Vector2 rayDir)
    {
        switch (direction)
        {
            case CollisionDirection.UP:
                rayCount = verticalRayCount;
                raycastOrigin = raycastOrigins.topLeft;
                raySpacingDir = Vector2.right;
                raySpacing = verticalRaySpacing;
                rayDir = Vector2.up;
                break;
            case CollisionDirection.DOWN:
                rayCount = verticalRayCount;
                raycastOrigin = raycastOrigins.bottomLeft;
                raySpacingDir = Vector2.right;
                raySpacing = verticalRaySpacing;
                rayDir = Vector2.down;
                break;
            case CollisionDirection.LEFT:
                rayCount = horizontalRayCount;
                raycastOrigin = raycastOrigins.bottomLeft;
                raySpacingDir = Vector2.up;
                raySpacing = horizontalRaySpacing;
                rayDir = Vector2.left;
                break;
            case CollisionDirection.RIGHT:
                rayCount = horizontalRayCount;
                raycastOrigin = raycastOrigins.bottomRight;
                raySpacingDir = Vector2.up;
                raySpacing = horizontalRaySpacing;
                rayDir = Vector2.right;
                break;
            case CollisionDirection.NONE:
                rayCount = horizontalRayCount;
                raycastOrigin = raycastOrigins.bottomRight;
                raySpacingDir = Vector2.zero;
                raySpacing = horizontalRaySpacing;
                rayDir = Vector2.zero;
                return false;
            default:
                rayCount = horizontalRayCount;
                raycastOrigin = raycastOrigins.bottomRight;
                raySpacingDir = Vector2.zero;
                raySpacing = horizontalRaySpacing;
                rayDir = Vector2.zero;
                Debug.LogError("Wrong CollisionDirection");
                return false;
        }

        return true;
    }    

    public bool CheckHorizontalCollision(int direction)
    {
        return RayCastCollision(verticalRayCount,
                                () => { return (direction == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight; },
                                Vector2.up,
                                verticalRaySpacing,
                                Vector2.down,
                                rayLength,
                                groundLayermask);
    }

    public bool CheckEndOfGround(CollisionDirection direction, float rayLength, LayerMask layerMask)
    {
        Vector2 raycastOrigin = new Vector2();

        switch (direction)
        {
            case CollisionDirection.LEFT:
                raycastOrigin = raycastOrigins.bottomLeft;
                break;
            case CollisionDirection.RIGHT:
                raycastOrigin = raycastOrigins.bottomRight;
                break;
            default:
                Debug.LogError("Wrong CollisionDirection");
                return false;
        }

        Vector2 rayOrigin = raycastOrigin;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, layerMask);

        Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red);

        if (hit) return false;
        else return true;
    }

    public bool GroundCollision(LayerMask layerMask)
    {
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, .1f, layerMask);

            Debug.DrawRay(rayOrigin, Vector2.down * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public bool OneSidePlatformCollision()
    {
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, .1f);

            Debug.DrawRay(rayOrigin, Vector2.up * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public bool HorizontalCollisions(float direction, LayerMask layerMask)
    {
        float directionX = direction;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, .1f, layerMask);
            //Debug.Log(hit.transform.name);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * .1f, Color.red);

            if (hit) return true;
        }

        return false;
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds;

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;

        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }
}
