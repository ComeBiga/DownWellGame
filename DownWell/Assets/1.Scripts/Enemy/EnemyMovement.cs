using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody = new Rigidbody2D();
    protected CollisionCheck collision = new CollisionCheck();

    [Header("Values")]
    public float speed = 0;
    
    [Header("Collision")]
    public float rayLength = 0.1f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask groundLayermask;
}

interface IEnemyMoveValue
{
    public float Speed { get; set; }
}
