using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    #region Singleton
    public static ItemDrop instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public List<GameObject> enemyDropItem;
    
    void Start()
    {
        //리스트 내림차순 정렬
        enemyDropItem.Sort((A, B) => B.GetComponent<Item>().i_Info.chacePercent.CompareTo(A.GetComponent<Item>().i_Info.chacePercent));
    }

    public void InstantiateRandomItem(Vector3 position, string whatTag)
    {
        switch (whatTag)
        {
            case "Enemy":
                if (enemyDropItem.Count > 0)
                {
                    for (int i = 0; i < enemyDropItem.Count; i++)
                    {
                        if (chanceResult(enemyDropItem[i].GetComponent<Item>().i_Info.chacePercent))
                        {
                            enemyDropItem[i].GetComponent<Item>().InstantiateItem(position);
                            break;
                        }
                    }
                }
                break;
        }
    }


    public bool chanceResult(float chance)  //chance = 확률(%)
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
