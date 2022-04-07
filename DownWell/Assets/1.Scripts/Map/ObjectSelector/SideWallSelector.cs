using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWallSelector : ObjectSelector
{
    public SideWallSelector(int code) : base(code)
    {
    }

    public SideWallSelector(int min, int max) : base(min, max)
    {
    }

    protected override GameObject Select(int tileCode)
    {
        var obj = Find(tileCode);
        return Instantiate(obj);
    }
}
