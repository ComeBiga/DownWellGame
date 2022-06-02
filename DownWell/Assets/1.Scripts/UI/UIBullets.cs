using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullets : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private List<Image> bullets;

    private PlayerAttack playerAttack;
    private Gun gun;
    private int lastCount;

    public void Init(PlayerAttack playerAttack)
    {
        this.playerAttack = playerAttack;
        bullets = new List<Image>();

        OnWeaponChanged();
        
        playerAttack.weaponReinforcer.OnReinforce += OnWeaponChanged;
    }

    public void OnWeaponChanged()
    {
        Debug.Log("OnWeaponChanged");
        gun = playerAttack.CurrentWeapon as Gun;
        gun.OnReload += UICollector.Instance.bullets.OnChange;
        gun.OnShoot += UICollector.Instance.bullets.OnChange;

        lastCount = gun.CurrentNumOfBullet;

        DisplayBulletImages();
    }

    public void OnChange()
    {
        gun = playerAttack.CurrentWeapon as Gun;

        //Debug.Log($"Bullet : {gun.CurrentNumOfBullet}");
        
        if(gun.CurrentNumOfBullet > lastCount)
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
            if (i >= bullets.Count)
            {
                lastCount = bullets.Count;
                return;
            }

            bullets[i].color = Color.white;
        }

        lastCount += amount;
    }

    public void Decrease(int amount = 1)
    {
        for (int i = lastCount; i > lastCount - amount; i--)
        {
            if(i <= 0)
            {
                lastCount = 0;
                return;
            }
            
            bullets[i - 1].color = Color.clear;
        }

        lastCount -= amount;
    }

    private void DisplayBulletImages()
    {
        Debug.Log($"{bullets.Count}, {gun.CapacityOfMagazine}");
        if (bullets.Count >= gun.CapacityOfMagazine) return;

        foreach(var bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }

        for (int i = 0; i < gun.CapacityOfMagazine; i++)
        {
            if(i < bullets.Count)
            {
                bullets[i].gameObject.SetActive(true);
            }
            else
            {
                var blt = Instantiate(bullet, transform);
                bullets.Add(blt.GetComponent<Image>());
            }
        }
    }
}
