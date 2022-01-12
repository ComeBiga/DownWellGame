using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionShootRage : BossActionShootNormal
{
    [SerializeField] private float sideProjectileAngle;

    public override void Take()
    {
        GetComponent<Animator>().SetTrigger("Attack_0_Event");

        BossAction.Cut();
    }

    void ShootNormalRageByTransform()
    {
        var target = GameObject.FindGameObjectWithTag("Player").transform;
        var dir = (target.position - transform.position).normalized;

        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile.GetComponent<BossProjectile>().MoveToTargetByTransform();

        var shotProjectile1 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile1.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile1.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile1.GetComponent<BossProjectile>().RotateDirection(sideProjectileAngle);
        shotProjectile1.GetComponent<BossProjectile>().MoveToTargetByTransform();

        var shotProjectile2 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile2.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile2.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile2.GetComponent<BossProjectile>().RotateDirection(-sideProjectileAngle);
        shotProjectile2.GetComponent<BossProjectile>().MoveToTargetByTransform();
    }
}
