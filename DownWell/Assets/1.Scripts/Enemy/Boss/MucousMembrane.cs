using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucousMembrane : BossProjectile
{
    [SerializeField] BoxCollider2D hitBox;

    protected override void TakeDamage()
    {
        //base.TakeDamage();

        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        if (colliders.Count > 0)
        {
            Splash();

            foreach (var collider in colliders)
            {
                if (collider.tag == "Wall")
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

        void Splash()
        {
            List<Collider2D> colliders = new List<Collider2D>();
            hitBox.OverlapCollider(new ContactFilter2D(), colliders);

            if (colliders.Count > 0)
            {
                foreach (var collider in colliders)
                {
                    if(collider.GetComponent<BeSplashed>() != null)
                        collider.GetComponent<BeSplashed>().Splash();
                }
            }
        }
    }
}
