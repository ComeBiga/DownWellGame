using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionShootRage : BossActionShootNormal
{
    [SerializeField] private float sideProjectileAngle;
    [SerializeField] private int numOfShot = 3;
    [SerializeField] private float shotInterval = 0f;

    public override void Take()
    {
        //GetComponent<Animator>().SetTrigger("Attack_0_Event");
        StartCoroutine(EAnimateShotInARow());

        Cut();
    }

    void ShootNormalRageByTransform()
    {
        StartCoroutine(EShootInARow());
        //var target = GameObject.FindGameObjectWithTag("Player").transform;
        //var dir = (target.position - transform.position).normalized;

        //var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        ////shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
        //shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
        //shotProjectile.GetComponent<BossProjectile>().MoveToTargetByTransform();

        //var shotProjectile1 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        ////shotProjectile1.GetComponent<BossProjectile>().SetTarget(target);
        //shotProjectile1.GetComponent<BossProjectile>().SetDirection(dir);
        //shotProjectile1.GetComponent<BossProjectile>().RotateDirection(sideProjectileAngle);
        //shotProjectile1.GetComponent<BossProjectile>().MoveToTargetByTransform();

        //var shotProjectile2 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        ////shotProjectile2.GetComponent<BossProjectile>().SetTarget(target);
        //shotProjectile2.GetComponent<BossProjectile>().SetDirection(dir);
        //shotProjectile2.GetComponent<BossProjectile>().RotateDirection(-sideProjectileAngle);
        //shotProjectile2.GetComponent<BossProjectile>().MoveToTargetByTransform();
    }

    private IEnumerator EAnimateShotInARow()
    {
        var count = 0;

        while (true)
        {
            GetComponent<Animator>().SetTrigger("Attack_0_Event");

            if (++count >= numOfShot) break;

            yield return new WaitForSeconds(shotInterval);
        }
    }

    private IEnumerator EShootInARow()
    {
        var count = 0;
        var target = PlayerManager.instance.playerObject.transform;

        while(true)
        {
            var dir = (target.position - transform.position).normalized;

            ShootOnce(dir);

            if (++count >= numOfShot) break;

            yield return new WaitForSeconds(shotInterval);
        }
    }

    // Use in Animation Event
    private void Shoot()
    {
        var target = PlayerManager.instance.playerObject.transform;
        var dir = (target.position - transform.position).normalized;

        ShootOnce(dir);
    }

    private void ShootOnce(Vector2 dir, float angle = 0)
    {
        var shotProjectile1 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile1.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile1.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile1.GetComponent<BossProjectile>().RotateDirection(angle);
        shotProjectile1.GetComponent<BossProjectile>().MoveToTargetByTransform();
    }
}
