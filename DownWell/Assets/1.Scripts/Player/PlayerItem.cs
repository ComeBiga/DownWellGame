using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public enum ItemType { PASSIVE, SLOT }

    private List<Item> items;
    private UseItem slotItem;

    public void UseSlotItem()
    {
        if(slotItem != null) slotItem.Use();
    }

    public void AddItem(ItemType type, Item item)
    {
        switch (type)
        {
            case ItemType.PASSIVE:
                items.Add(item);
                break;
            case ItemType.SLOT:
                slotItem = item as UseItem;
                break;
            default:
                Debug.LogWarning("Not Found Type of Item!");
                break;
        }
    }

    #region Private Method
    #endregion
}
