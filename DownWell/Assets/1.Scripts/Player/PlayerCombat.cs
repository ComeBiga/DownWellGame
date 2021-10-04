using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [Header("Shoot")]
    public GameObject projectile;
    public int projectileDamage = 4;
    public float shotDelay = 1f;
    public float shotReboundSpeed = 1f;
    float shotTimer = 0f;

    [Header("Stepping")]
    public GameObject hitBox;
    public LayerMask enemyLayerMask;
    public float stepUpSpeed = 7f;
    ContactFilter2D enemyFilter;
    Collider2D[] enemyColliders;

    [Header("Damaged")]
    public float leapSpeed;
    public float knuckBackSpeed;
    public float invincibleTime;
    bool isInvincible = false;
    public bool IsInvincible { get; }

    public UnityEvent OnDamaged;

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = shotDelay;

        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayerMask);

        enemyColliders = new Collider2D[3];
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        //if (Input.GetButton("Jump") && !GetComponent<PlayerController>().grounded)
        //    Shoot();

        StepOn();
    }

    #region Shoot Function
    public void Shoot()
    {
        if (shotTimer >= shotDelay)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().damage = projectileDamage;

            //GetComponent<PlayerController>().ShotRebound();
            GetComponent<PlayerController>().LeapOff(shotReboundSpeed);
            Camera.main.GetComponent<CameraShake>().Shake();

            GetComponent<PlayerAnimation>().Shoot();

            shotTimer = 0;
        }
    }
    #endregion

    public void Damaged(Enemy enemy)
    {
        if (isInvincible) return;

        Debug.Log("Player Damaged");

        if (!isInvincible) OnDamaged.Invoke();

        Vector3 knuckbackDir = transform.position - enemy.transform.position;
        int direction = knuckbackDir.x > 0 ? 1 : -1;
        GetComponent<PlayerController>().KnuckBack(knuckBackSpeed, direction);

        StartCoroutine(BecomeInvincible());

        Camera.main.GetComponent<CameraShake>().Shake();
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .3f);

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

    }

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
            }
        }

        if (playerBound) GetComponent<PlayerController>().LeapOff(leapSpeed);
    }
}
