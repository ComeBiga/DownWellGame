using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item/Item")]
public class ItemInfo : LevelObject
{
    public float chacePercent;
    public bool immediately;

    public virtual void PickUp()
    {

    }
}
