using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public GameObject projectile;
    public int capacity = 8;
    public float coolDownTime = 1f;
    public float shotRebound = 0f;

    private float timer = 0f;

    private void Start()
    {
        weapon = new Weapon(projectile, capacity);
    }

    private void Update()
    {
        if(timer < coolDownTime) timer += Time.deltaTime;

        if(!weapon.Reloaded && GetComponent<PlayerController>().GroundCollision()) weapon.Reload();
    }

    public void Shoot()
    {
        if(timer > coolDownTime && weapon.shootable && !weapon.IsEmpty)
        {
            weapon.Shoot(projectile, transform);

            GetComponent<PlayerPhysics>().LeapOff(weapon.shotRebound);

            // Effect
            Effect();

            timer = 0;
        }
    }

    private void Effect()
    {
        // Camera
        Camera.main.GetComponent<CameraShake>().Shake(.03f);

        // Animation
        GetComponent<PlayerAnimation>().Shoot();

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_0");

        // FX
        GetComponent<Effector>().Generate("Shoot");

        // UI
        bulletCount.instance.countBullet();
    }

    public void ReLoad()
    {
        weapon.Reload();
    }
}
