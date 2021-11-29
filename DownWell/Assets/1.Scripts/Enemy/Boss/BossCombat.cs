using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public GameObject projectile;
    public GameObject mucousMembrane;
    public float shotTime = 3f;
    float curTime = 0f;

    Transform target;

    System.Action ShootPattern;

    string[] normalPatterns = { "ShootNormal" };
    string[] ragePatterns = { "ShootNormalRage", "ShootMucousMembrane" };
    string[] currentPatterns;

    [Range(0,100)] public int healthRatioRageMode = 60;
    [Range(0, 30)] public float sideProjectileAngle = 15f;

    void Start()
    {
        ChangeShootPatterns(Boss.BossState.rage);
        SetPattern(ragePatterns[0]);
    }

    private void Update()
    {
        Shoot();

        if(GetComponent<Boss>().BecomeRageMode(healthRatioRageMode))
        {
            ChangeShootPatterns(Boss.BossState.rage);
        }
    }

    void Shoot()
    {
        curTime += Time.deltaTime;

        if (curTime > shotTime)
        {
            SetRandomShootPattern();
            ShootPattern.Invoke();

            curTime = 0;
        }
    }

    public void ChangeShootPatterns(Boss.BossState state)
    {
        switch(state)
        {
            case Boss.BossState.normal:
                currentPatterns = normalPatterns;
                break;
            case Boss.BossState.rage:
                currentPatterns = ragePatterns;
                break;
        }
    }

    public void SetPattern(string methodName)
    {
        ShootPattern = () => { Invoke(methodName, 0); };
    }

    void SetRandomShootPattern()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        SetPattern(currentPatterns[rand.Next(0, currentPatterns.Length)]);
    }

    void ShootNormal()
    {
        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
    }

    void ShootNormalRage()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        var dir = (target.position - transform.position).normalized;

        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        //shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile.GetComponent<BossProjectile>().MoveToTarget();

        var shotProjectile1 = Instantiate(projectile, transform.position, Quaternion.identity);
        //shotProjectile1.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile1.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile1.GetComponent<BossProjectile>().RotateDirection(sideProjectileAngle);
        shotProjectile1.GetComponent<BossProjectile>().MoveToTarget();

        var shotProjectile2 = Instantiate(projectile, transform.position, Quaternion.identity);
        //shotProjectile2.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile2.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile2.GetComponent<BossProjectile>().RotateDirection(-sideProjectileAngle);
        shotProjectile2.GetComponent<BossProjectile>().MoveToTarget();
    }

    void ShootMucousMembrane()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        var dir = (target.position - transform.position).normalized;

        var shotProjectile = Instantiate(mucousMembrane, transform.position, Quaternion.identity);
        //shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile.GetComponent<BossProjectile>().MoveToTarget();
    }
}
