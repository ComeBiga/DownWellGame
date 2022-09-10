using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public int damage = 0;
    public float lifeDistance = 5f;
    public bool destroybyDistance = true;
    public bool destroyOnHit = true;

    [Header("Movement")]
    public float speed = 20f;
    public float moveDistance = 0;
    private float currentPos;
    private float lastPos;
    private Vector3 direction;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;

        lastPos = currentPos = transform.position.y;
    }

    public virtual void Init(float speed, Vector2 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    public void Init()
    {
        direction = Vector3.down;
    }

    public virtual void Update()
    {
        if (destroybyDistance && moveDistance >= lifeDistance)
            Destroy();

        currentPos = transform.position.y;
        moveDistance += Mathf.Abs(lastPos - currentPos);
        lastPos = currentPos;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision != null)
    //     {
    //         if (collision.GetComponent<IHitByProjectile>() != null)
    //         {
    //             collision.GetComponent<IHitByProjectile>().Hit(damage);

    //             GetComponent<Effector>().Generate("Hit");

    //             if (destroyOnHit) Destroy();
    //         }

    //     }
    // }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
