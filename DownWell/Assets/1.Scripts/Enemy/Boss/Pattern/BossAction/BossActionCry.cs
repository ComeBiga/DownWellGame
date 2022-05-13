using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionCry : BossAction
{
    PlayerPhysics playerPhysics;
    private void Start()
    {
        playerPhysics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPhysics>();
    }
    public override void Take()
    {
        PlayerManager.instance.playerObject.GetComponent<PlayerController>().cantMove = true;
        playerPhysics.JumpCancel();
        Camera.main.GetComponent<CameraShake>().Shake(.07f, 1f, true);
        StartCoroutine(movePlayer());
    
        Cut();
    }

    IEnumerator movePlayer()
    {
        yield return new WaitForSeconds(1f);
        PlayerManager.instance.playerObject.GetComponent<PlayerController>().cantMove = false;
    }
}