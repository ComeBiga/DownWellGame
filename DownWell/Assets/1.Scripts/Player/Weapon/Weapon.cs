using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    // magazine
    public struct Magazine
    {
        public int max;
        public int current;
    }
    private Magazine magazine;

    public bool shootable;

    public bool Reloaded { get { return (magazine.current >= magazine.max); } }

    public Weapon(GameObject projectile, int capacity)
    {
        // magazine
        magazine.max = capacity;
        magazine.current = 0;

        shootable = true;
    }

    public void Shoot(GameObject projectile, Transform transform)
    {
        if (magazine.current > 0)
        {
            magazine.current--;
            var pt = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
            //pt.GetComponent<Projectile>().damage = projectileDamage;
        }
    }

    public void Reload()
    {
        if(!Reloaded) magazine.current = magazine.max;
    }
}
