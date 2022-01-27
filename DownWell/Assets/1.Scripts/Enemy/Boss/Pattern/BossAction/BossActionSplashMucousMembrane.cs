using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionSplashMucousMembrane : BossAction
{
    public override void Take()
    {
        Collider2D mmBox = transform.GetChild(2).GetComponent<Collider2D>();
        ContactFilter2D wFilter = new ContactFilter2D();
        wFilter.layerMask = 1 >> 6;

        List<Collider2D> colliders = new List<Collider2D>();
        mmBox.OverlapCollider(wFilter, colliders);

        if (colliders.Count > 0)
        {
            foreach (var col in colliders)
            {
                if (col.GetComponent<BeSplashed>() != null)
                {
                    col.GetComponent<BeSplashed>().Splash();
                }
            }
        }

        //GetComponent<Effector>().Generate("Blast");
        GetComponent<Animator>().SetTrigger("Attack_1");

        Cut();
    }
}
