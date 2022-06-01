using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    [Header("Potion")]
    [SerializeField] private int amount = 1;

    protected override void OnPickedUp()
    {
        PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().GainHealth(amount);

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Health");
    }
}
