using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PlayerController controller;
    PlayerHealth health;

    private PlayerCombatStepping cStep;
    private Weapon weapon;
    private PlayerAttack pAttack;

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
    public bool useLoseHealth = false;
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
        health = GetComponent<PlayerHealth>();

        shotTimer = shotDelay;

        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayerMask);

        enemyColliders = new Collider2D[3];

        currentProjectile = maxProjectile;

        // Attack
        pAttack = GetComponent<PlayerAttack>();

        // Weapon
        weapon = new Weapon(projectile, maxProjectile);

        // Stepping
        cStep = new PlayerCombatStepping(hitBox);
        cStep.stepUpSpeed = stepUpSpeed;
        cStep.unshootableTime = unshootableTime;
        cStep.onStep += weapon.Reload;
    }

    // Update is called once per frame
    void Update()
    {
        // PlayerAttack
        shotTimer += Time.deltaTime;

        // PlayerAttack
        if (!reloaded && controller.GroundCollision())
        {
            Reload();
            reloaded = true;
        }

        //StepOn();
        // =>
        cStep.Loop();
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
        Camera.main.GetComponent<CameraShake>().Shake(.03f);                           //ī�޶� ��鸲
        GetComponent<PlayerAnimation>().Shoot();                                       //ĳ���� �ִϸ��̼�
        //if (SoundManager.instance != null) SoundManager.instance.PlayEffSound("Shoot_0");  //��������Ʈ
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_0");

        GetComponent<Effector>().Generate("Shoot");                                    //�� ����Ʈ
    }
    #endregion

    #region Damage
    public void Damaged(Transform enemy)
    {
        if (isInvincible) return;

        // to OnDamaged
        if (ItemManager.instance.curItem != "") ItemManager.instance.UseItem();

        // Lose Health
        if(useLoseHealth) health.LoseHealth();

        // Knockback
        KnockBack(knuckBackSpeed, transform.position, enemy.transform.position, knuckBackDistance);

        // Event
        OnDamaged.Invoke();

        // Effect
        DamagedEffect();
        BecomeInvincible();
    }

    public void KnockBack(Vector2 speed, Vector3 playerPosition, Vector3 enemyPosition, float distance)
    {
        Vector3 knuckbackDir = playerPosition - enemyPosition;
        int direction = knuckbackDir.x > 0 ? 1 : -1;

        AddForce(speed, direction, distance);
    }

    public void AddForce(Vector2 speed, int direction, float distance)
    {
        // Y direction
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, speed.y);

        // X direction
        if(gameObject.activeSelf) StartCoroutine(EAddForceTransform(speed.x, direction, distance));
    }

    IEnumerator EAddForceTransform(float knuckBackSpeed, int direction, float distance)
    {
        InputManager.instance.blockInput = true;
        float dis = 0;

        controller.cantMove = true;

        while (true)
        {
            if (Mathf.Abs(dis) > distance || controller.HorizontalCollisions() == true)
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
    
    private void BecomeInvincible()
    {
        // Vincible
        if (gameObject.activeSelf) StartCoroutine(EBecomeInvincible());
    }

    private IEnumerator EBecomeInvincible()
    {
        isInvincible = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .3f);

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    private void DamagedEffect()
    {
        Camera.main.GetComponent<CameraShake>().Shake(.08f);

        GetComponent<Effector>().Generate("Damaged");

        //if (SoundManager.instance != null) SoundManager.instance.PlayEffSound("Shoot_1");  //��������Ʈ
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_1");

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
