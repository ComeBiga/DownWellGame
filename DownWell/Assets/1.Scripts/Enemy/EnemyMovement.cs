using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody = new Rigidbody2D();
    public float speed = 0;

    public CollisionCheck collision = new CollisionCheck();
    public float rayLength = 0.1f;
    public int horizontalRaycount = 4;
    public int verticalRaycount = 4;
    public LayerMask groundLayerMask;
}

interface IEnemyMoveValue
{
    public float Speed { get; set; }
}
