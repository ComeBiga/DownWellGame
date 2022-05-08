using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttack : MonoBehaviour
{
    private BoxCollider2D collider;

    private List<Collider2D> colliders;
    private ContactFilter2D playerFilter;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();

        colliders = new List<Collider2D>();

        playerFilter = new ContactFilter2D();
        playerFilter.useLayerMask = true;
        playerFilter.layerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Collision();
    }

    private void Collision()
    {
        var count = collider.OverlapCollider(playerFilter, colliders);

        if (count > 0)
        {
            foreach (var c in colliders)
            {
                c.GetComponent<PlayerCombat>().Damaged(transform);
                break;
            }
        }
    }
}
