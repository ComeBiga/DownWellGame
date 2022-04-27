using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerAttack : MonoBehaviour
{
    private Weapon weapon;
    public GameObject projectile;
    public int capacity = 8;
    public float coolDownTime = 1f;

    public bool shootable = true;

    private float timer = 0f;

    private void Start()
    {
        weapon = new Weapon(projectile, capacity);
    }

    private void Update()
    {
        if(timer < coolDownTime) timer += Time.deltaTime;

        if(!weapon.Reloaded) weapon.Reload();
    }

    public void Loop()
    {
        if(timer > coolDownTime && shootable)
        {
            weapon.Shoot(projectile, transform);

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
}
