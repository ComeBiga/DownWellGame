using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Init(BoxCollider2D boxCollider, int horizontalRayCount, int verticalRayCount)
    {
        this.boxCollider = boxCollider;
        this.horizontalRayCount = horizontalRayCount;
        this.verticalRayCount = verticalRayCount;
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
