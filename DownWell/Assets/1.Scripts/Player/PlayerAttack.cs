using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<Weapon> lineOfWeapons;
    [HideInInspector] public Weapon weapon;
    public WeaponReinforcer weaponReinforcer;

    // shot cooldown
    public float coolDownTime = 1f;
    private float timer = 0f;

    public void Shoot()
    {
        if(timer >= coolDownTime && weapon.IsShootable())//weapon.shootable && !weapon.IsEmpty)
        {
            weapon.Attack();//Shoot(projectile, transform);

            //GetComponent<PlayerPhysics>().LeapOff(weapon.shotRebound);

            // Effect
            weapon.Effect();

            timer = 0;
        }
    }

    public void ReinforceWeapon()
    {
        Weapon improved;
        if(weaponReinforcer.Reinforce(out improved))
        {
            weapon = improved;
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
        if(timer < coolDownTime) timer += Time.deltaTime;

        //if(!weapon.Reloaded && GetComponent<PlayerController>().GroundCollision()) weapon.Reload();
    }

    private void InitWeapons()
    {
        foreach(var w in lineOfWeapons)
        {
            w.Init(this.gameObject);
        }

        weaponReinforcer = new WeaponReinforcer(lineOfWeapons);
        weapon = weaponReinforcer.Current;
    }
}
