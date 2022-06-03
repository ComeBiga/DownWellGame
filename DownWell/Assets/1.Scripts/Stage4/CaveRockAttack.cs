using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveRockAttack : Enemy
{
    public override void Damaged(int damage)
    {
    }

    public override bool Stepped()
    {
        //if (PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().IsInvincible) return false;

        PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().Damaged(transform);
        Destroy(this.gameObject);
        return false;
    }
}
