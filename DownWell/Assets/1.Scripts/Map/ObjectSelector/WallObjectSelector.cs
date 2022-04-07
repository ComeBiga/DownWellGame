using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObjectSelector : ObjectSelector
{
    protected WallObjectSelector(int code) : base(code)
    {
    }

    protected WallObjectSelector(int min, int max) : base(min, max)
    {
    }
}
