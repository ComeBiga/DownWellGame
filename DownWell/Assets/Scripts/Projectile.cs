using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 0;

    public float lifeDistance = 5f;

    private void Update()
    {
        CollisionCheck();

        if (GetComponent<ProjectileMovement>().moveDistance >= lifeDistance)
            Destroy();
    }

    void CollisionCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x);

        foreach (var collider in colliders)
        {
            if (collider.tag == "Block")
            {
                Destroy();
                collider.transform.GetComponent<Block>().Destroy();

                break;
            }

            if (collider.tag == "Wall")
            {
                Destroy();
            }

            if(collider.tag == "Enemy")
            {
                Destroy();
                collider.GetComponent<Enemy>().Damaged(damage);
            }
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
