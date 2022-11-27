using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<int, Item> items = new Dictionary<int, Item>(50);

    private void Awake()
    {
        var loadedItems = Resources.LoadAll<Item>("Items");

        for(int i = 0; i < loadedItems.Length; ++i)
        {
            var value = loadedItems[i];
            var key = value.i_Info.code;

            items.Add(key, value);
        }
    }

    public static Item GetItem(int code)
    {
        Item item = null;
        if (items.TryGetValue(code, out item) == false)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Item is null(Code:{code})");
#endif
            return null;
        }

        return item;
    }
}
