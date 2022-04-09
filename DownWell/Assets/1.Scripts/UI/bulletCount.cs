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
                anim.SetInteger("shootNum", PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().currentProjectile);
                break;
        }
    }

    public void bulletReload()
    {
        anim.SetInteger("shootNum", 8);
        anim.SetTrigger("reload");
    }

    /*public void countBullet()
    {
        c_bullet.text = "X " + PlayerManager.instance.player.GetComponent<PlayerCombat>().currentProjectile;
    }*/
}
