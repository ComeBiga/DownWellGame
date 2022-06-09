using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Wall
{
    ContactFilter2D filter;
    void Start()
    {
        filter = new ContactFilter2D();
        filter.layerMask = 1 << 3;
        filter.useLayerMask = false;
    }

    void Update()
    {
        Hit();
    }

    public void Hit()
    {
        var colliders = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(filter, colliders);

        foreach (var collider in colliders)
        {
            if (collider != null && collider.tag == "Player")
            {
                CaveLightOn();   
                return;
            }
        }
    }

    public override void Hit(int damage =0)
    {
        base.Hit(damage);
        CaveLightOn();
    }

    void CaveLightOn()
    {
        var caveLight = PlayerManager.instance.playerObject.transform.GetChild(5).gameObject;

        //이미 켜진 스위치는 다시 눌려도 불이 켜지지 않음
        if (!GetComponent<Animator>().GetBool("On"))
        {
            caveLight.GetComponent<Animator>().SetBool("On", true);
            caveLight.GetComponent<CaveLight>().activeOnce = true;
        }
        GetComponent<Animator>().SetBool("On", true);
    }
}