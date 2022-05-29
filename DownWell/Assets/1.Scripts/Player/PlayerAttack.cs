using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<Weapon> lineOfWeapons;
    [HideInInspector] public Weapon weapon;
    public WeaponReinforcer weaponReinforcer;

    public Weapon CurrentWeapon { get { return weaponReinforcer.Current; } }

    // shot cooldown
    public float coolDownTime = 1f;
    private float timer = 0f;

    public void Shoot()
    {
        if(timer >= CurrentWeapon.coolDownTime && CurrentWeapon.IsShootable())//weapon.shootable && !weapon.IsEmpty)
        {
            CurrentWeapon.Attack();//Shoot(projectile, transform);

            //GetComponent<PlayerPhysics>().LeapOff(weapon.shotRebound);

            // Effect
            CurrentWeapon.Effect();

            timer = 0;
        }
    }

    public void ReinforceWeapon()
    {
        //Weapon improved;
        if(weaponReinforcer.Reinforce())
        {
            //weapon = improved;
        }
    }

    private void Start()
    {
        //weapon = new Weapon(projectile, capacity);

        //GetComponent<PlayerPhysics>().OnGrounded += ReLoad;
        InitWeapons();
    }

    private void Update()
    {
        if(timer <= CurrentWeapon.coolDownTime) timer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (weaponReinforcer.Reinforce())
            {
                weaponReinforcer.ReinforceRange(2f);
            }
            //Debug.Log("Weapon reinforced!");
        }

        //if(!weapon.Reloaded && GetComponent<PlayerController>().GroundCollision()) weapon.Reload();
    }

    private void InitWeapons()
    {
        foreach(var w in lineOfWeapons)
        {
            w.Init(this.gameObject);
        }

        weaponReinforcer = new WeaponReinforcer(lineOfWeapons);
        //weapon = weaponReinforcer.Current;

        // UI
        UICollector.Instance.bullets.Init(this);
    }
}
