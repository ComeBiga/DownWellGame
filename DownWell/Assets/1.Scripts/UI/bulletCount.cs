using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletCount : MonoBehaviour
{
    public Text c_bullet;

    void Start()
    {
    }

    void Update()
    {
        countBullet();
    }

    public void countBullet()
    {
        c_bullet.text = "X " + PlayerManager.instance.player.GetComponent<PlayerCombat>().currentProjectile;
    }
}
