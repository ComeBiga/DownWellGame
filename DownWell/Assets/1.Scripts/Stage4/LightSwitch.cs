using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Wall
{
    public override void Hit(int damage =0)
    {
        var caveLight = PlayerManager.instance.playerObject.transform.GetChild(5).gameObject.GetComponent<Animator>();
        base.Hit(damage);

        //이미 켜진 스위치는 다시 눌려도 불이 켜지지 않음
        if (!GetComponent<Animator>().GetBool("On"))
            caveLight.SetBool("On", true);

        GetComponent<Animator>().SetTrigger("On");
    }
}