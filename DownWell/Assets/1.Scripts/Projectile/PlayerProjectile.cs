using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.GetComponent<IHitByProjectile>() != null)
            {
                collision.GetComponent<IHitByProjectile>().Hit(damage);

                GetComponent<Effector>().Generate("Hit");

                if (destroyOnHit) Destroy();
            }

            GetComponent<Effector>().Generate("Hit");
            if (destroyOnHit) Destroy();
        }
    }
}
