using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollector : Singleton<UICollector>
{
    public HealthBar hpBar;
    public bulletCount bullet;
    public Score score;
    public CoinCount coin;
}