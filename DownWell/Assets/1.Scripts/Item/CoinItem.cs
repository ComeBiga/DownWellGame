using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    protected override void OnPickedUp()
    {
        // Score
        GameManager.instance.coin.Gain();
    }
}
