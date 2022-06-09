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

        //�̹� ���� ����ġ�� �ٽ� ������ ���� ������ ����
        if (!GetComponent<Animator>().GetBool("On"))
        {
            caveLight.GetComponent<Animator>().SetBool("On", true);
            caveLight.GetComponent<CaveLight>().activeOnce = true;
        }
        GetComponent<Animator>().SetBool("On", true);
    }
}