using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOctoTurret : MonoBehaviour
{
    public Projectile projectile;
    public float projectileSpeed = 3f;
    public float shootInterval = 2f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > shootInterval)
        {
            var newProjectile = Instantiate<Projectile>(projectile, transform.position, Quaternion.identity);

            var playerPos = PlayerManager.instance.playerObject.transform.position;
            var dir = (playerPos - transform.position).normalized;

            newProjectile.Init(projectileSpeed, dir);

            timer = 0;
        }
    }
}
