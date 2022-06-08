using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseImmediatelyItem : UseItem
{
    protected override void OnPickedUp()
    {
        Use();
    }

    public override void Use()
    {

    }
}
