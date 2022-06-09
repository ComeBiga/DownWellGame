using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public enum ItemType { PASSIVE, SLOT }

    private List<ItemInfo> items;
    private UseItem slotItem;

    public List<ItemInfo> Items { get { return items; } }

    public event System.Action<ItemInfo> OnAdded;

    public void AddItem(ItemInfo itemInfo)
    {
        if (Exist(itemInfo.code)) return;

        items.Add(itemInfo);

        OnAdded.Invoke(itemInfo);
    }

    public bool Exist(int itemCode)
    {
        return items.Exists(item => item.code == itemCode);
    }

    #region SlotItem
    public void UseSlotItem()
    {
        if(slotItem != null) slotItem.Use();
    }

    public void AddItem(ItemType type, Item item)
    {
        switch (type)
        {
            case ItemType.PASSIVE:
                items.Add(item.i_Info);
                break;
            case ItemType.SLOT:
                slotItem = item as UseItem;
                break;
            default:
                Debug.LogWarning("Not Found Type of Item!");
                break;
        }
    }

    #endregion

    #region Private Method

    private void Start()
    {
        items = new List<ItemInfo>();

        // UI
        UICollector.Instance.itemPocket.Init(this);
    }

    #endregion
}
