using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionShootRage : BossActionShootNormal
{

    public override void Take()
    {
        GetComponent<Animator>().SetTrigger("Attack_0");
    }
}
