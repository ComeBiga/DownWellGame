using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : UseImmediatelyItem
{
    //[Header("PowerUp")]
    //public bool use = false;

    public override void Use()
    {
        base.Use();

        PlayerManager.instance.playerObject.GetComponent<PlayerAttack>().ReinforceWeapon();
        Debug.Log("Power Up!");
    }
}
