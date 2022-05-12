using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletCount : MonoBehaviour
{
    #region Singleton
    public static bulletCount instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    //public Text c_bullet;

    public GameObject[] bulletType;
    public GameObject curBulletType;

    Animator anim;

    void Start()
    {
        if (curBulletType == null)
            curBulletType = bulletType[0];

        anim = curBulletType.GetComponent<Animator>();

    }

    void Update()
    {
        //countBullet();
    }


    public void countBullet()
    {
        switch(curBulletType.name)
        {
            case "type1":
                Gun g = PlayerManager.instance.playerObject.GetComponent<PlayerAttack>().weapon as Gun;
                Debug.Log($"Bullet Count : {g.CurrentNumOfBullet}");
                anim.SetInteger("shootNum", g.CurrentNumOfBullet);//GetComponent<PlayerAttack>().weapon.CurrentNumOfBullet);
                break;
        }
    }

    public void bulletReload()
    {
        Gun g = PlayerManager.instance.playerObject.GetComponent<PlayerAttack>().weapon as Gun;
        Debug.Log($"Capacity Of Magazine : {g.CapacityOfMagazine}");
        anim.SetInteger("shootNum", g.CapacityOfMagazine);//GetComponent<PlayerAttack>().weapon.CapacityOfMagazine);
        anim.SetTrigger("reload");
    }

    /*public void countBullet()
    {
        c_bullet.text = "X " + PlayerManager.instance.player.GetComponent<PlayerCombat>().currentProjectile;
    }*/
}
