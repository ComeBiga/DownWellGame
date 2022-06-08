using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Wall
{
    public override void Hit(int damage =0)
    {
        var caveLight = PlayerManager.instance.playerObject.transform.GetChild(5).gameObject;
        base.Hit(damage);

        //�̹� ���� ����ġ�� �ٽ� ������ ���� ������ ����
        if (!GetComponent<Animator>().GetBool("On"))
        {
            caveLight.GetComponent<Animator>().SetBool("On", true);
            caveLight.GetComponent<CaveLight>().activeOnce = true;
        }
        GetComponent<Animator>().SetBool("On", true);
    }
}