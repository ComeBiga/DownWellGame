using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleProjectile : BossProjectile
{
    protected override void Start()
    {
        filter = new ContactFilter2D();
        filter.SetLayerMask(1 << 3);
    }
}
