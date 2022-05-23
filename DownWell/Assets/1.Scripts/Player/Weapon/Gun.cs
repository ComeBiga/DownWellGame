using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Gun", menuName = "Weapon/Gun")]
public class Gun : Weapon
{
    [Header("Gun")]
    [SerializeField] protected GameObject projectile;
    protected Transform shotPos;
    public float addedRange;

    // magazine
    public struct Magazine
    {
        public int max;
        public int current;
    }
    protected Magazine magazine;

    [Header("Magazine")]
    [SerializeField] private int capacity = 0;

    public bool Reloaded { get { return (magazine.current >= magazine.max); } }
    public bool IsEmpty { get { return (magazine.current <= 0); } }
    public int CurrentNumOfBullet { get { return magazine.current; } }
    public int CapacityOfMagazine { get { return magazine.max; } }

    public event System.Action OnReload;
    public event System.Action OnShoot;

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
        magazine.current = magazine.max;

        player.GetComponent<PlayerPhysics>().OnGrounded += Reload;
        player.GetComponent<PlayerCombatStepping>().OnStep.AddListener(Reload);
        shotPos = player.transform.GetChild(1);

        addedRange = 0f;

        // Bullet UI
        UICollector.Instance.bullets.Init();
        OnReload += UICollector.Instance.bullets.OnChange;
        OnShoot += UICollector.Instance.bullets.OnChange;
        OnReload += () => { PlayerManager.instance.playerObject.GetComponent<Effector>().GenerateInParent("Reload"); };
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
        //UICollector.Instance.bullet.countBullet();
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
            AddRange(pt);
            //Debug.Log($"AddedRange : {addedRange} (Gun.cs)");
            //pt.GetComponent<Projectile>().damage = projectileDamage;

            OnShoot.Invoke();
        }
    }

    protected void OnShootFunc()
    {
        OnShoot.Invoke();
    }

    public void Reload()
    {
        Debug.Log(Reloaded);
        if (Reloaded) return;

        magazine.current = magazine.max;

        OnReload.Invoke();

        //UICollector.Instance.bullet.bulletReload();
    }

    public override bool IsShootable()
    {
        return shootable && !IsEmpty;
    }

    protected void AddRange(GameObject projectile)
    {
        projectile.GetComponent<Projectile>().lifeDistance += addedRange;
    }

}
