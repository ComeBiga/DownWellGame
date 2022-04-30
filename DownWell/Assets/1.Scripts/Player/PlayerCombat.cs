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

        // Stepping
        cStep = GetComponent<PlayerCombatStepping>();
    }

    #region Shoot Function (Deprecated)
    //public void Shoot()
    //{
    //    if (shotTimer >= shotDelay && shootable && currentProjectile > 0)
    //    {
    //        InstantiateProjectile();
    //        currentProjectile--;

    //        if (currentProjectile < maxProjectile) reloaded = false;

    //        //LeapOff(shotReboundSpeed);
    //        GetComponent<PlayerPhysics>().LeapOff(shotReboundSpeed);

    //        ShootEffect();

    //        bulletCount.instance.countBullet();

    //        shotTimer = 0;
    //    }
    //}

    //public void InstantiateProjectile()
    //{
    //    GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
    //    newProjectile.GetComponent<Projectile>().damage = projectileDamage;
    //}

    //public void Reload()
    //{
    //    currentProjectile = maxProjectile;
    //    bulletCount.instance.bulletReload();
    //}

    //public void ShootEffect()
    //{
    //    Camera.main.GetComponent<CameraShake>().Shake(.03f);                           //카메라 흔들림
    //    GetComponent<PlayerAnimation>().Shoot();                                       //캐릭터 애니메이션
    //    //if (SoundManager.instance != null) SoundManager.instance.PlayEffSound("Shoot_0");  //사운드이펙트
    //    if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_0");

    //    GetComponent<Effector>().Generate("Shoot");                                    //슛 이펙트
    //}
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
            if (Mathf.Abs(dis) > distance || 
                (GetComponent<PlayerPhysics>().wallCollision.CheckCollision(CollisionDirection.LEFT) ||
                 GetComponent<PlayerPhysics>().wallCollision.CheckCollision(CollisionDirection.RIGHT) )
                 )
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

        //if (SoundManager.instance != null) SoundManager.instance.PlayEffSound("Shoot_1");  //사운드이펙트
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_1");

    }

    #endregion

    #region Stepping (Deprecated)
    //void StepOn()
    //{
    //    var hitNum = hitBox.GetComponent<CircleCollider2D>().OverlapCollider(enemyFilter, enemyColliders);

    //    bool playerBound = false;

    //    foreach (var enemyCollider in enemyColliders)
    //    {
    //        //Debug.Log(enemyCollider);

    //        if (enemyCollider != null)
    //        {
    //            playerBound = true;
    //            enemyCollider.GetComponent<Enemy>().Die();

    //            if (!reloaded) Reload();
    //            StartCoroutine(SteppingUp());
    //        }
    //    }

    //    if (playerBound) LeapOff(leapSpeed);
    //}

    //IEnumerator SteppingUp()
    //{
    //    shootable = false;
    //    yield return new WaitForSeconds(unshootableTime);
    //    shootable = true;
    //}

    //public void LeapOff(float stepUpSpeed)
    //{
    //    rigidbody.velocity = new Vector2(rigidbody.velocity.x, stepUpSpeed);
    //    controller.jumping = true;
    //}

    #endregion


}
