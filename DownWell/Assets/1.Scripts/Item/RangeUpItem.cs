using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpItem : UseImmediatelyItem
{
    public float addRange = 3f;

    public override void Use()
    {
        PlayerManager.instance.playerObject.GetComponent<PlayerAttack>().weaponReinforcer.ReinforceRange(addRange);

        Debug.Log("Range Up");
    }
}
