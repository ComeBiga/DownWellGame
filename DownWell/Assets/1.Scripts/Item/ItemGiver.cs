using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : Block
{
    public ItemDrop itemDropper;

    [SerializeField] private List<GameObject> dropItems;

    private void Start()
    {
        itemDropper.SetItem(dropItems);
    }

    public override void Destroy()
    {
        itemDropper.Random(transform.position);

        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);
    }
}
