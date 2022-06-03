using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveRockAttack : Wall
{
    ContactFilter2D filter;
    void Start()
    {
        filter = new ContactFilter2D();
        filter.layerMask = 1 << 3;
        filter.useLayerMask = false;
    }
    
    void Update()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        var colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(filter, colliders);

        foreach (var collider in colliders)
        {
            if (collider != null && collider.tag == "Player")
            {
                if (!collider.GetComponent<PlayerCombat>().IsInvincible)
                    collider.GetComponent<PlayerCombat>().Damaged(transform);

                Destroy(this.gameObject);

                return;
            }
        }
    }
}
