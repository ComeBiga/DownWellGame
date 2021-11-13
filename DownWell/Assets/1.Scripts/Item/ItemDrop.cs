using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ItemInfo i_Info;
    
    void InstatiateItem()
    {
        //반복문
        bool gain = chanceResult(i_Info.chacePercent);
        if(gain)
        {
            //리스트에 추가
            gain = false;
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
