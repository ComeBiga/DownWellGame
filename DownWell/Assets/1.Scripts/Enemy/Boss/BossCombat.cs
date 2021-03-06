using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public bool active = false;

    public GameObject projectile;
    public GameObject mucousMembrane;
    public float shotTime = 3f;
    float curTime = 0f;
    bool shootable = true;

    Transform target;

    System.Action ShootPattern;

    [SerializeField] string[] normalPatterns = { "ShootNormal" };
    [SerializeField] string[] ragePatterns = { "ShootNormalRage", "ShootMucousMembrane" };
    string[] currentPatterns;

    [Range(0,100)] public int healthRatioRageMode = 60;
    [Range(0, 30)] public float sideProjectileAngle = 15f;

    public GameObject boxAttack;

    void Start()
    {
        ChangeShootPatterns(Boss.BossState.rage);
        SetPattern(ragePatterns[0]);
    }

    private void Update()
    {
        //if(active)
        //    Shoot();

        //if(GetComponent<Boss>().BecomeRageMode(healthRatioRageMode))
        //{
        //    ChangeShootPatterns(Boss.BossState.rage);
        //}
    }

    void Shoot()
    {
        if(shootable)
            curTime += Time.deltaTime;

        if (curTime > shotTime)
        {
            SetRandomShootPattern();
            ShootPattern.Invoke();
            Debug.Log(ShootPattern.Method.GetBaseDefinition().Name);

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
        //Debug.Log(methodName);
        ShootPattern = () => { Invoke(methodName, 0); };
    }

    void SetRandomShootPattern()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        var rp = currentPatterns[rand.Next(0, currentPatterns.Length)];
        //Debug.Log($"rp : {rp}");

        SetPattern(rp);
    }

    void ShootNormal()
    {
        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shotProjectile.GetComponent<BossProjectile>().SetTarget(target);

        GetComponent<Animator>().SetTrigger("Attack_0");
    }

    void ShootNormalRage()
    {
        //Debug.Log("ShootNormalRage");
        GetComponent<Animator>().SetTrigger("Attack_0");
    }

    void ShootNormalRagebyVelocity()
    {
        //Debug.Log("ShootNormalRagebyVelocity");
        target = GameObject.FindGameObjectWithTag("Player").transform;
        var dir = (target.position - transform.position).normalized;

        var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile.GetComponent<BossProjectile>().MoveToTarget();

        var shotProjectile1 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile1.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile1.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile1.GetComponent<BossProjectile>().RotateDirection(sideProjectileAngle);
        shotProjectile1.GetComponent<BossProjectile>().MoveToTarget();

        var shotProjectile2 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        //shotProjectile2.GetComponent<BossProjectile>().SetTarget(target);
        shotProjectile2.GetComponent<BossProjectile>().SetDirection(dir);
        shotProjectile2.GetComponent<BossProjectile>().RotateDirection(-sideProjectileAngle);
        shotProjectile2.GetComponent<BossProjectile>().MoveToTarget();
    }

    // Used by Animation Key Event
    //void ShootNormalRageByTransform()
    //{
    //    Debug.Log("ShootNormalRagebyTransform");
    //    target = GameObject.FindGameObjectWithTag("Player").transform;
    //    var dir = (target.position - transform.position).normalized;

    //    var shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform);
    //    //shotProjectile.GetComponent<BossProjectile>().SetTarget(target);
    //    shotProjectile.GetComponent<BossProjectile>().SetDirection(dir);
    //    shotProjectile.GetComponent<BossProjectile>().MoveToTargetByTransform();

    //    var shotProjectile1 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
    //    //shotProjectile1.GetComponent<BossProjectile>().SetTarget(target);
    //    shotProjectile1.GetComponent<BossProjectile>().SetDirection(dir);
    //    shotProjectile1.GetComponent<BossProjectile>().RotateDirection(sideProjectileAngle);
    //    shotProjectile1.GetComponent<BossProjectile>().MoveToTargetByTransform();

    //    var shotProjectile2 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
    //    //shotProjectile2.GetComponent<BossProjectile>().SetTarget(target);
    //    shotProjectile2.GetComponent<BossProjectile>().SetDirection(dir);
    //    shotProjectile2.GetComponent<BossProjectile>().RotateDirection(-sideProjectileAngle);
    //    shotProjectile2.GetComponent<BossProjectile>().MoveToTargetByTransform();
    //}

    void ShootMucousMembrane()
    {
        Collider2D mmBox = transform.GetChild(2).GetComponent<Collider2D>();
        ContactFilter2D wFilter = new ContactFilter2D();
        wFilter.layerMask = 1 >> 6;

        List<Collider2D> colliders = new List<Collider2D>();
        mmBox.OverlapCollider(wFilter, colliders);

        if(colliders.Count > 0)
        {
            foreach(var col in colliders)
            {
                if(col.GetComponent<BeSplashed>() != null)
                {
                    col.GetComponent<BeSplashed>().Splash();
                }
            }
        }

        //GetComponent<Effector>().Generate("Blast");
        GetComponent<Animator>().SetTrigger("Attack_1");
    }

    #region blast
    void BoxingAttack()
    {
        shootable = false;        

        StartCoroutine(IBoxingAttack());
    }

    void Shootable()
    {
        shootable = true;
    }

    // Move position left, right to shoot blast
    IEnumerator IBoxingAttack()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        var posIndex = rand.Next(-1, 2) * 2;
        Debug.Log(posIndex);
        float dis = 0f;

        while (true)
        {
            if (Mathf.Approximately(transform.position.x, posIndex))
                break;

            var xPos = Mathf.MoveTowards(transform.position.x, posIndex, Time.deltaTime * 2);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

            yield return null;
        }

        // Animate and key event
        GetComponent<Animator>().SetBool("Attack_2", true);

        //Invoke("Shootable", 3f);
        //var boxObject = Instantiate(boxAttack, transform);
        //boxObject.transform.position = new Vector3(posIndex * 2, boxObject.transform.position.y, boxObject.transform.position.z);
    }

    // Instantiate Blast Object
    void InstantiateAttackBox()
    {
        var boxObject = Instantiate(boxAttack, transform);
    }

    public void EndBoxingAttack()
    {
        StartCoroutine(IEndBoxing());
    }

    IEnumerator IEndBoxing()
    {
        GetComponent<Animator>().SetBool("Attack_2", false);

        while (true)
        {
            if (Mathf.Approximately(transform.position.x, 0))
                break;

            var xPos = Mathf.MoveTowards(transform.position.x, 0, Time.deltaTime * 2);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

            yield return null;
        }

        
        shootable = true;
        //Invoke("Shootable", 3f);
        //var boxObject = Instantiate(boxAttack, transform);
        //boxObject.transform.position = new Vector3(posIndex * 2, boxObject.transform.position.y, boxObject.transform.position.z);
    }
    #endregion
}
