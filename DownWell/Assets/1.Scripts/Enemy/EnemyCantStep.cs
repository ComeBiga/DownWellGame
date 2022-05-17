using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCantStep : Enemy
{
    public override bool Stepped()
    {
        //if (PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().IsInvincible) return false;

        PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().Damaged(transform);
        return false;
    }
}
