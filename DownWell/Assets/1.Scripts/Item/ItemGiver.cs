using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : Block
{
    public ItemDrop itemDropper;

    //public float dialogueOffset = 1f;

    [SerializeField] private List<GameObject> dropItems = new List<GameObject>();

    private void Start()
    {
        var dropItemSet = StageManager.instance.Current.dropItemSets[0];

        for (int i = 0; i < dropItemSet.items.Count; ++i)
        {
            var dropItem = DataManager.GetItem(dropItemSet.items[i]);
            dropItems.Add(dropItem.gameObject);
        }

        //dropItems = StageManager.instance.Current.DropItems;
        itemDropper.SetItem(dropItems);
    }

    public override void Destroy()
    {
        itemDropper.Random(transform.position);

        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);
    }
}
