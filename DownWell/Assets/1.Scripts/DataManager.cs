using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<int, Item> items = new Dictionary<int, Item>(50);
    public static Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>(50);

    private void Awake()
    {
        var loadedItems = Resources.LoadAll<Item>("Items");

        for (int i = 0; i < loadedItems.Length; ++i)
        {
            var value = loadedItems[i];
            var key = value.i_Info.code;

            items.Add(key, value);
        }

        var loadedEnemies = Resources.LoadAll<Enemy>("EnemyObjects");

        for (int i = 0; i < loadedEnemies.Length; ++i)
        {
            var value = loadedEnemies[i];
            var key = value.info.code;

            enemies.Add(key, value);
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

    public static Enemy GetEnemy(int code)
    {
        Enemy enemy = null;
        if (enemies.TryGetValue(code, out enemy) == false)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Enemy is null(Code:{code}");
#endif

            return null;
        }

        return enemy;
    }
}
