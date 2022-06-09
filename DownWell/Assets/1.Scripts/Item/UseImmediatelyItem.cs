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

            // Sound
            if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Coin");
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
