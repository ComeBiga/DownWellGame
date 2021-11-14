using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public List<GameObject> enemyDropItem;
    List<GameObject> successItem;

    public void InstantiateRandomItem(Vector3 position)
    {
        if (enemyDropItem.Count > 0)
        {
            for (int i = 0; i < enemyDropItem.Count; i++)
            {
                if (chanceResult(enemyDropItem[i].GetComponent<Item>().i_Info.chacePercent))
                    successItem.Add(enemyDropItem[i]);
            }
            if (successItem.Count > 0)
            {
                int itemRand = UnityEngine.Random.Range(0, successItem.Count);
                successItem[itemRand].GetComponent<Item>().InstantiateItem(position);
                successItem.Clear();
            }
        }
    }


    public bool chanceResult(float chance)  //chance = È®·ü(%)
    {
        chance = chance / 100f;
     
        if (chance < 0.0000001f)
            chance = 0.0000001f;

        bool Success = false;
        
        int RandAccuracy = 10000000;
        float RandHitRange = chance * RandAccuracy;
        int Rand = UnityEngine.Random.Range(1, RandAccuracy + 1);
        
        if (Rand <= RandHitRange)
            Success = true;

        return Success;
    }
}
