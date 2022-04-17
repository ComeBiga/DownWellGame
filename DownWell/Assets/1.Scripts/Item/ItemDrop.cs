using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private List<GameObject> dropItems;

    public ItemDrop()
    {
        dropItems = new List<GameObject>();
    }

    public void Init(List<GameObject> dropItems)
    {
        this.dropItems = dropItems;
    }

    public void Random(Vector3 position, int count = 5)
    {
        for(int i = 0; i< count; i++)
        {
            var rItem = dropItems[CatDown.Random.Get().Next(dropItems.Count)].GetComponent<Item>();

            if(CatDown.Random.Get().Next(100) < rItem.i_Info.chacePercent)
            {
                rItem.InstantiateItem(position);
            }
        }
    }

    public void InstantiateRandomItem(List<GameObject> dropitems, Vector3 position)
    {
        if (dropitems.Count > 0)
        {
            for (int i = 0; i < dropitems.Count; i++)
            {
                if (chanceResult(dropitems[i].GetComponent<Item>().i_Info.chacePercent))
                {
                    dropitems[i].GetComponent<Item>().InstantiateItem(position);
                    return;
                }
            }
        }
    }


    public bool chanceResult(float chance)  //chance = Ȯ��(%)
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
