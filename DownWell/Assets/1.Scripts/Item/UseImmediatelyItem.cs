using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseImmediatelyItem : UseItem
{
    protected override void OnPickedUp()
    {
        if (PlayerManager.instance.playerObject.GetComponent<PlayerItem>().Exist(i_Info.code))
        {
            UICollector.Instance.coin.Gain(10);
        }
        else
        {
            Use();
        }
    }

    public override void Use()
    {

    }
}
