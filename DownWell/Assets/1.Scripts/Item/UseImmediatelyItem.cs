using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseImmediatelyItem : UseItem
{
    public override void OnPickedUp()
    {
        Use();
    }

    public override void Use()
    {

    }
}
