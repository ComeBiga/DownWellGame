using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullets : MonoBehaviour
{
    public List<Image> bullets;

    private PlayerAttack playerAttack;
    private Gun gun;
    private int lastCount;

    public void Init()
    {
        playerAttack = PlayerManager.instance.playerObject.GetComponent<PlayerAttack>();
        gun = playerAttack.weapon as Gun;
        lastCount = gun.CurrentNumOfBullet;
    }

    public void OnChange()
    {
        gun = playerAttack.weapon as Gun;

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
}
