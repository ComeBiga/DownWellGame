using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionDirection { HORIZONTAL, UP, DOWN, LEFT, RIGHT }

public class CollisionCheck : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public float horizontalRaySpacing;
    public float verticalRaySpacing;

    public RaycastOrigins raycastOrigins;
    public struct RaycastOrigins
    {
        public Vector2 bottomLeft, topLeft;
        public Vector2 bottomRight, topRight;
    }

    delegate Vector2 GetRaycastOrigin();

    public void Init(BoxCollider2D boxCollider, int horizontalRayCount, int verticalRayCount)
    {
        this.boxCollider = boxCollider;
        this.horizontalRayCount = horizontalRayCount;
        this.verticalRayCount = verticalRayCount;
    }

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

    public bool CheckCollision(CollisionDirection direction, float rayLength, LayerMask layerMask)
    {
        int rayCount;
        Vector2 raycastOrigin = new Vector2();
        Vector2 raySpacingDir = new Vector2();
        float raySpacing;
        Vector2 rayDir = new Vector2();

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
            default:
                Debug.LogError("Wrong CollisionDirection");
                return false;
        }

        return RayCastCollision(rayCount, () => { return raycastOrigin; },
                                raySpacingDir, raySpacing, rayDir, rayLength, layerMask);

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
