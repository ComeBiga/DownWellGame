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

        var player = PlayerManager.instance.playerObject;

        // Power Up
        player.GetComponent<PlayerAttack>().ReinforceWeapon();

        // FX
        player.GetComponent<Effector>().GenerateInParent("PowerUp");

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("PowerUp");

        //Debug.Log("Power Up!");
    }
}
