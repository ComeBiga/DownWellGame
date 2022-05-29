using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TrippleGun", menuName = "Weapon/TrippleGun")]
public class TrippleGun : Gun
{
    [SerializeField] private float angleOffset;

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
            var pt = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
            var pt2 = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
            var pt3 = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);

            InitProjectile(pt);
            InitProjectile(pt2);
            InitProjectile(pt3);

            pt2.GetComponent<ProjectileMovement>().SetDirection(RotateDirection(Vector3.down, -angleOffset));
            pt2.GetComponent<ProjectileMovement>().Rotate(-angleOffset);

            pt3.GetComponent<ProjectileMovement>().SetDirection(RotateDirection(Vector3.down, angleOffset));
            pt3.GetComponent<ProjectileMovement>().Rotate(angleOffset);
            //Debug.Log($"AddedRange : {addedRange} (DoubleGun.cs)");
            //pt.GetComponent<Projectile>().damage = projectileDamage;

            OnShootFunc();
        }
    }

    private Vector3 RotateDirection(Vector3 from, float angle)
    {
        var qtAngle = Quaternion.Euler(0, 0, angle);
        var dir = (qtAngle * from).normalized;

        return dir;
    }
}
