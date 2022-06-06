using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    protected override void OnPickedUp()
    {
        UICollector.Instance.coin.Gain();

        // AchievementSystem.Instance.ProgressAchievement("Coin");

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Coin");
    }
}
