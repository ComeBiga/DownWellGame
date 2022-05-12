using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Gun", menuName = "Weapon/Gun")]
public class Gun : Weapon
{
    [Header("Gun")]
    [SerializeField] private GameObject projectile;
    private Transform shotPos;

    // magazine
    public struct Magazine
    {
        public int max;
        public int current;
    }
    private Magazine magazine;

    [Header("Magazine")]
    [SerializeField] private int capacity = 0;

    public bool Reloaded { get { return (magazine.current >= magazine.max); } }
    public bool IsEmpty { get { return (magazine.current <= 0); } }
    public int CurrentNumOfBullet { get { return magazine.current; } }
    public int CapacityOfMagazine { get { return magazine.max; } }

    //public Gun(GameObject projectile, int capacity)
    //{
    //    // magazine
    //    magazine.max = capacity;
    //    magazine.current = 0;

    //    shootable = false;
    //}

    //private void Start()
    //{
    //    magazine.max = capacity;
    //    magazine.current = 0;

    //    GetComponent<PlayerPhysics>().OnGrounded += Reload;
    //}

    public override void Init(GameObject player)
    {
        base.Init(player);

        magazine.max = capacity;
        magazine.current = 0;

        player.GetComponent<PlayerPhysics>().OnGrounded += Reload;
        shotPos = player.transform.GetChild(1);
        Debug.Log(shotPos.name);
    }

    public override void Attack()
    {
        Shoot();
    }

    public override void Effect()
    {
        // ShotRebound
        player.GetComponent<PlayerPhysics>().LeapOff(shotRebound);

        // Camera
        Camera.main.GetComponent<CameraShake>().Shake(.03f);

        // Animation
        player.GetComponent<PlayerAnimation>().Shoot();

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Shoot_0");

        // FX
        player.GetComponent<Effector>().Generate("Shoot");

        // UI
        UICollector.Instance.bullet.countBullet();
    }

    public void Shoot()
    {
        Shoot(projectile, shotPos);
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
        if (!Reloaded) magazine.current = magazine.max;

        UICollector.Instance.bullet.bulletReload();
    }

    public override bool IsShootable()
    {
        return shootable && !IsEmpty;
    }

}
