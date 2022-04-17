using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CoinItem", menuName = "Item/CoinItem")]
public class CoinItem : ItemInfo
{
    public override void PickUp()
    {
        // Score
        GameManager.instance.coin.Gain();
    }
}
