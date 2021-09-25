using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public int projectileDamage = 4;
    public float shotDelay = 1f;

    float shotTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        //if (Input.GetButton("Jump") && !GetComponent<PlayerController>().grounded)
        //    Shoot();
    }

    public void Shoot()
    {
        if(shotTimer >= shotDelay)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<Projectile>().damage = projectileDamage;

            GetComponent<PlayerController>().ShotRebound();
            StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake());

            GetComponent<PlayerAnimation>().Shoot();

            shotTimer = 0;
        }
    }
}
