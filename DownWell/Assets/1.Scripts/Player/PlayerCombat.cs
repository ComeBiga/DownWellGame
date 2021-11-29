using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PlayerController controller;

    [Header("Shoot")]
    public GameObject projectile;
    public int projectileDamage = 4;
    public int maxProjectile = 8;
    public int currentProjectile;
    bool reloaded = true;
    public float shotDelay = 1f;
    public float shotReboundSpeed = 1f;
    float shotTimer = 0f;

    [Header("Stepping")]
    public GameObject hitBox;
    public LayerMask enemyLayerMask;
    public float stepUpSpeed = 7f;
    public float unshootableTime = 1f;
    public bool shootable = true;
    ContactFilter2D enemyFilter;
    Collider2D[] enemyColliders;

    [Header("Damaged")]
    public float leapSpeed;
    public Vector2 knuckBackSpeed;
    public float knuckBackDistance;
    public float invincibleTime;
    bool isInvincible = false;
    public bool IsInvincible { get; }

    public UnityEvent OnDamaged;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();

        shotTimer = shotDelay;

        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayerMask);

        enemyColliders = new Collider2D[3];

        currentProjectile = maxProjectile;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        if (!reloaded && controller.GroundCollision())
        {
            Reload();
            reloaded = true;
        }

        StepOn();
    }

    #region Shoot Function
    public void Shoot()
    {
        if (shotTimer >= shotDelay && shootable && currentProjectile > 0)
        {
            InstantiateProjectile();
            currentProjectile--;

            if (currentProjectile < maxProjectile) reloaded = false;

            LeapOff(shotReboundSpeed);

            ShootEffect();

            bulletCount.instance.countBullet();

            shotTimer = 0;
        }
    }

    public void InstantiateProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().damage = projectileDamage;
    }

    public void Reload()
    {
        currentProjectile = maxProjectile;
        bulletCount.instance.bulletReload();
    }

    public void ShootEffect()
    {
        Camera.main.GetComponent<CameraShake>().Shake(.03f);                           //카메라 흔들림
        GetComponent<PlayerAnimation>().Shoot();                                       //캐릭터 애니메이션
        if (SoundManager.instance != null) SoundManager.instance.PlayEffSound("gun");  //사운드이펙트
        GetComponent<Effector>().Generate("Shoot");                                    //슛 이펙트
    }
    #endregion

    #region Damage
    public void Damaged(Transform enemy)
    {
        if (isInvincible) return;

        KnuckBack(knuckBackSpeed, transform.position, enemy.transform.position, knuckBackDistance);

        StartCoroutine(BecomeVincible());

        DamagedEffect();
    }

    public void KnuckBack(Vector2 speed, Vector3 playerPosition, Vector3 enemyPosition, float distance)
    {
        Vector3 knuckbackDir = playerPosition - enemyPosition;
        int direction = knuckbackDir.x > 0 ? 1 : -1;

        KnuckBack(speed, direction, distance);
    }

    public void KnuckBack(Vector2 speed, int direction, float distance)
    {
        //rigidbody.velocity = new Vector2(knuckBackSpeed * direction, rigidbody.velocity.y + knuckBackSpeed);

        //StartCoroutine(KnuckBacking(knuckBackSpeed, direction));
        //rigidbody.velocity = new Vector2(0, 0);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, speed.y);
        //rigidbody.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
        StartCoroutine(AddForceTransform(speed.x, direction, distance));
        controller.cantMove = true;
    }

    IEnumerator AddForceTransform(float knuckBackSpeed, int direction, float distance)
    {
        InputManager.instance.blockInput = true;
        float dis = 0;

        while (true)
        {
            if (Mathf.Abs(dis) > distance)
                break;

            var forceX = knuckBackSpeed * direction * Time.deltaTime;
            transform.position += new Vector3(forceX, 0, 0);
            dis += forceX;
            //Debug.Log(dis);

            yield return null;
        }

        InputManager.instance.blockInput = false;
        controller.cantMove = false;
    }

    IEnumerator BecomeVincible()
    {
        isInvincible = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .3f);

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void DamagedEffect()
    {
        Camera.main.GetComponent<CameraShake>().Shake(.08f);
    }

    #endregion

    #region unused Code
    //void BecomeVincible()
    //{
    //    isInvincible = false;
    //    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    //}

    //IEnumerator BecomeInvincible()
    //{
    //    isInvincible = true;
    //    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .3f);

    //    yield return new WaitForSeconds(invincibleTime);

    //    isInvincible = false;
    //    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    //}
    #endregion

    #region Stepping
    void StepOn()
    {
        var hitNum = hitBox.GetComponent<CircleCollider2D>().OverlapCollider(enemyFilter, enemyColliders);

        bool playerBound = false;

        foreach (var enemyCollider in enemyColliders)
        {
            //Debug.Log(enemyCollider);

            if (enemyCollider != null)
            {
                playerBound = true;
                enemyCollider.GetComponent<Enemy>().Die();

                if (!reloaded) Reload();
                StartCoroutine(SteppingUp());
            }
        }

        if (playerBound) LeapOff(leapSpeed);
    }

    IEnumerator SteppingUp()
    {
        shootable = false;
        yield return new WaitForSeconds(unshootableTime);
        shootable = true;
    }

    public void LeapOff(float stepUpSpeed)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, stepUpSpeed);
        controller.jumping = true;
    }

    #endregion


}
