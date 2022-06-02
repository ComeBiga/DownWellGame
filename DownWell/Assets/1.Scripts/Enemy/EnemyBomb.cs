using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : Enemy
{
    public override bool Stepped()
    {
        //if (PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().IsInvincible) return false;

        PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().Damaged(transform);
        Die();
        return false;
    }
}
