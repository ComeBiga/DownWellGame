using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DoubleGun", menuName ="Weapon/DoubleGun")]
public class DoubleGun : Gun
{
    [SerializeField] private float projectileOffset; 

    public override void Attack()
    {
        Shoot();
    }

    public new void Shoot()
    {
        Shoot(projectile, shotPos);
    }

    public new void Shoot(GameObject projectile, Transform transform)
    {
        if (magazine.current > 0)
        {
            magazine.current--;
            var pt = GameObject.Instantiate(projectile, transform.position + Vector3.right * projectileOffset, Quaternion.identity);
            var pt2 = GameObject.Instantiate(projectile, transform.position + Vector3.left * projectileOffset, Quaternion.identity);

            InitProjectile(pt);
            InitProjectile(pt2);

            //Debug.Log($"AddedRange : {addedRange} (DoubleGun.cs)");
            //pt.GetComponent<Projectile>().damage = projectileDamage;

            OnShootFunc();
        }
    }
}
