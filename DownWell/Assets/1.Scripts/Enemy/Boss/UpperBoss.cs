using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBoss : MonoBehaviour
{
    public ContactFilter2D filter;

    private void Update()
    {
        var colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(filter, colliders);

        foreach (var collider in colliders)
        {
            if (collider != null && collider.tag == "Player")
            {
                var player = collider.transform;

                if (player.GetComponent<PlayerPhysics>().Grounded)
                {
                    player.GetComponent<PlayerCombat>().Damaged(transform, player.GetComponent<PlayerHealth>().CurrentHealth, true);
                    //GetComponent<PlayerHealth>().Die();
                }
                else
                {
                    player.GetComponent<PlayerCombat>().Damaged(transform);
                }

                return;
            }
        }
    }
}
