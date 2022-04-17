using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemDropper", menuName = "Item/ItemDropper")]
public class ItemDrop : ScriptableObject
{
    private List<GameObject> dropItems;

    [Header("TimeSet")]
    public float popingTime = .5f;
    public float livingTime = 2f;

    [Header("PopSpeed")]
    public float maxHorizontalPopSpeed = 5f;
    public float minVerticalPopSpeed = 2f;
    public float maxVerticalPopSpeed = 10f;

    public ItemDrop()
    {
        dropItems = new List<GameObject>();
    }

    public void SetItem(List<GameObject> dropItems)
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
                //rItem.InstantiateItem(position);
                InstantiateItem(rItem.gameObject, position);
            }
        }
    }

    private void InstantiateItem(GameObject itemObject, Vector3 position)
    {
        var dropItem = Instantiate(itemObject, position, Quaternion.identity);

        var rand = CatDown.Random.Get();
        var popSpeed = new Vector2(rand.Next(-(int)maxHorizontalPopSpeed, (int)maxHorizontalPopSpeed),
                                   rand.Next(-(int)minVerticalPopSpeed, (int)maxVerticalPopSpeed));

        dropItem.GetComponent<Rigidbody2D>().AddForce(popSpeed, ForceMode2D.Impulse);
        dropItem.GetComponent<Item>().Invoke("EndPoping", popingTime);
        dropItem.GetComponent<Item>().DestroyItem(livingTime);
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
