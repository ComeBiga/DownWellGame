using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    public int damage = 1;
    public float speed = 3f;
    Vector2 direction;

    ContactFilter2D filter;

    private void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(1 << 3);
    }

    private void Update()
    {
        TakeDamage();
    }

    public void SetTarget(Transform target)
    {
        direction = (target.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void TakeDamage()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        if(colliders.Count > 0)
        {
            foreach(var collider in colliders)
            {
                if(collider.tag == "Wall")
                {
                    Destroy(this.gameObject);
                    GetComponent<Effector>().Generate("Hit");
                    return;
                }
                if (collider.tag == "Block")
                {
                    Destroy(this.gameObject);
                    GetComponent<Effector>().Generate("Hit");
                    return;
                }
                if (collider.tag == "Player")
                {
                    collider.GetComponent<PlayerCombat>().Damaged(transform);
                    Destroy(this.gameObject);
                    GetComponent<Effector>().Generate("Hit");
                    return;
                }
            }
        }
    }
}
