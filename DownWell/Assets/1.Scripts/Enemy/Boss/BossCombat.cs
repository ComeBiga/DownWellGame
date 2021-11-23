using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public GameObject projectile;
    public float shotTime = 3f;
    float curTime = 0f;

    Transform target;

    private void Update()
    {
        curTime += Time.deltaTime;

        if(curTime > shotTime)
        {
            ShootProjectile();

            curTime = 0;
        }
    }

    void ShootProjectile()
    {
        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
    }
}
