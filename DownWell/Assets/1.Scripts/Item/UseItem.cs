using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseItem : Item
{
    public abstract void Use();

    public override void PutIn(PlayerItem playerItem)
    {
        // slot
        playerItem.AddItem(PlayerItem.ItemType.SLOT, this);

        //
        playerItem.AddItem(i_Info);
    }
}
