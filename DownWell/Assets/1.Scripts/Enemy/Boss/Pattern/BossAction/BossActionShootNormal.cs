using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionShootNormal : BossAction
{
    [SerializeField] private GameObject projectile;

    public override void Take()
    {
        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        var target = GameObject.FindGameObjectWithTag("Player").transform;
        shotProjectile.GetComponent<BossProjectile>().SetTarget(target);

        // Event
        onEvent.Invoke();
    }

    //public override void StartAction()
    //{
    //    var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
    //    var target = GameObject.FindGameObjectWithTag("Player").transform;
    //    shotProjectile.GetComponent<BossProjectile>().SetTarget(target);

    //    // Event
    //    onEvent.Invoke();
    //}
}
