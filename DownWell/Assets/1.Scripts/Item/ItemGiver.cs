using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : Block
{
    public ItemDrop itemDropper;

    //public float dialogueOffset = 1f;

    [SerializeField] private List<GameObject> dropItems;

    private void Start()
    {
        dropItems = StageManager.instance.Current.DropItems;
        itemDropper.SetItem(dropItems);
    }

    public override void Destroy()
    {
        itemDropper.Random(transform.position);

        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);
    }
}
