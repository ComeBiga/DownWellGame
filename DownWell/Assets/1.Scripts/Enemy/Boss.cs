using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameObject bossObject;

    public void GetBoss()
    {
        bossObject = GameObject.FindGameObjectWithTag("Boss");
        SetBossActive(false);
    }

    public void SetBossActive(bool value)
    {
        bossObject.SetActive(value);
    }
}
