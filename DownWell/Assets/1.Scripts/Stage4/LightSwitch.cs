using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Wall
{
    public override void Hit(int damage =0)
    {
        var caveLight = PlayerManager.instance.playerObject.transform.GetChild(5).gameObject.GetComponent<Animator>();
        base.Hit(damage);

        //�̹� ���� ����ġ�� �ٽ� ������ ���� ������ ����
        if (!GetComponent<Animator>().GetBool("On"))
            caveLight.SetBool("On", true);

        GetComponent<Animator>().SetTrigger("On");
    }
}