using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : UseImmediatelyItem
{
    [Header("Potion")]
    [SerializeField] private int amount = 1;

    public override void Use()
    {
        PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().GainHealth(amount);
    }
}
