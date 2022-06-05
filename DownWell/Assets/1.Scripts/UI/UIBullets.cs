using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullets : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    //private List<Image> bullets;
    private List<GameObject> bulletUIs;

    private PlayerAttack playerAttack;
    private Gun gun;
    private int lastCount;

    public void Init(PlayerAttack playerAttack)
    {
        this.playerAttack = playerAttack;
        //bullets = new List<Image>();
        bulletUIs = new List<GameObject>();

        OnWeaponChanged();
        
        playerAttack.weaponReinforcer.OnReinforce += OnWeaponChanged;
    }

    public void OnWeaponChanged()
    {
        //Debug.Log("OnWeaponChanged");
        gun = playerAttack.CurrentWeapon as Gun;
        gun.OnReload += OnChange;
        gun.OnShoot += OnChange;

        lastCount = gun.CurrentNumOfBullet;

        //DisplayBulletImages();
        DisplayBulletUIs();
    }

    public void OnChange()
    {
        gun = playerAttack.CurrentWeapon as Gun;

        //Debug.Log($"Bullet : {gun.CurrentNumOfBullet}");
        //Debug.Log($"lastCount : {lastCount}");
        //Debug.Log($"OnChange()===============");
        //DebugBullet();

        if (gun.CurrentNumOfBullet > lastCount)
        {
            Reload(gun.CurrentNumOfBullet);
        }
        else if(gun.CurrentNumOfBullet < lastCount)
        {
            Shoot(gun.CurrentNumOfBullet);
        }
        else
        {
            return;
        }
    }

    public void Reload(int currentBulletCount)
    {
        Increase(currentBulletCount - lastCount);
    }

    public void Shoot(int currentBulletCount)
    {
        Decrease(lastCount - currentBulletCount);
    }

    public void Increase(int amount = 1)
    {
        for(int i = lastCount; i < lastCount + amount; i++)
        {
            if (i >= bulletUIs.Count)
            {
                lastCount = bulletUIs.Count;
                return;
            }

            bulletUIs[i].SetActive(true);
            //bullets[i].color = Color.white;
        }

        lastCount += amount;
    }

    public void Decrease(int amount = 1)
    {
        //Debug.Log($"Decrease===============");
        //foreach(var bullet in bulletUIs)
        //{
        //    Debug.Log($"bulletUI 0 : {bullet}");
        //}

        for (int i = lastCount; i > lastCount - amount; i--)
        {
            //Debug.Log($"lastCount : {lastCount}, amount : {amount}, i : {i}");

            if(i <= 0)
            {
                lastCount = 0;
                return;
            }

            //Debug.Log($"bulletUIs : { bulletUIs[i-1] }, bulletUIs.Count : {bulletUIs.Count}");
            bulletUIs[i - 1].SetActive(false);
            //bullets[i - 1].color = Color.clear;
        }

        lastCount -= amount;
    }

    //private void DisplayBulletImages()
    //{
    //    //Debug.Log($"{bullets.Count}, {gun.CapacityOfMagazine}");
    //    if (bullets.Count >= gun.CapacityOfMagazine) return;

    //    Debug.Log($"DisplayBulletImages");

    //    foreach(var bullet in bullets)
    //    {
    //        bullet.gameObject.SetActive(false);
    //    }

    //    for (int i = 0; i < gun.CapacityOfMagazine; i++)
    //    {
    //        if(i < bullets.Count)
    //        {
    //            bullets[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            var blt = Instantiate(bullet, transform);
    //            bullets.Add(blt.GetComponent<Image>());
    //        }
    //    }
    //}

    private void DisplayBulletUIs()
    {
        //Debug.Log($"bulletUIs.Count : {bulletUIs.Count}");

        if (bulletUIs.Count >= gun.CapacityOfMagazine) return;

        foreach (var bullet in bulletUIs)
        {
            bullet.SetActive(false);
        }

        for (int i = 0; i < gun.CapacityOfMagazine; i++)
        {
            if (i < bulletUIs.Count)
            {
                bulletUIs[i].SetActive(true);
            }
            else
            {
                //Debug.Log($"bullet : {bullet}");
                var blt = Instantiate(bullet, transform);
                //Debug.Log($"Instantiated bullet : {blt}");
                bulletUIs.Add(blt);
            }
        }

        //Debug.Log($"bulletUIs.Count : {bulletUIs.Count}");
    }

    public void DebugBullet()
    {
        Debug.Log($"HashCode : {this.GetHashCode()}");
        int i = 0;
        foreach (var bullet in bulletUIs)
        {
            Debug.Log($"bulletUI {i} : {bullet}");
            i++;
        }
    }
}
