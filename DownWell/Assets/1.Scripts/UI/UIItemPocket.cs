using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemPocket : MonoBehaviour
{
    [SerializeField] private GameObject itemUI;

    //private List<ItemInfo> playerItems;
    public void Init(PlayerItem pi)
    {
        //playerItems = pi.Items;
        pi.OnAdded += AddItem;
    }

    public void AddItem(ItemInfo itemInfo)
    {
        var newItemUI = Instantiate(itemUI, transform);
        newItemUI.GetComponent<Image>().sprite = itemInfo.sprite;
    }

}
