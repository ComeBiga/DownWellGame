using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoTurretProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().Damaged(transform);
            
            if(destroyOnHit) Destroy();
        }
    }
}
