using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Collider2D _collider;
    ContactFilter2D filter;

    [Header("Collision")]
    public LayerMask itemLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        // Collider2D
        if(_collider == null)
            _collider = GetComponent<Collider2D>();

        // ContactFilter2D
        filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = itemLayer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckItemCollision();
    }

    void CheckItemCollision()
    {
        List<Collider2D> items = new List<Collider2D>();
        var len = _collider.OverlapCollider(filter, items);

        if (len <= 0) return;

        foreach(var item in items)
        {
            var ic = item.GetComponent<Item>();

            if (ic.PickUp())
            {
                ic.PutIn(GetComponent<PlayerItem>());
            }
        }
    }
}
