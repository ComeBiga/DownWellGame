using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionShootNormal : BossAction
{
    [SerializeField] protected GameObject projectile;
    //[SerializeField] protected Transform target;

    public override void Take()
    {
        GetComponent<Animator>().SetTrigger("Attack_0");
        // Event
        //onEvent.Invoke();

        //Debug.Log("Before Cut");
        Cut();
        //Debug.Log("After Cut");
    }

    void ShootNormalByTransform()
    {
        var target = GameObject.FindGameObjectWithTag("Player").transform;
        var dir = (target.position - transform.position).normalized;

        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile.GetComponent<BossProjectile>().MoveToTargetByTransform();
    }
}
