using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallSelector : ObjectSelector
{
    public SpecialWallSelector(int code) : base(code)
    {
    }

    public SpecialWallSelector(int min, int max) : base(min, max)
    {
    }

    protected override GameObject Select(int tileCode)
    {
        if (BossStageManager.instance.IsBossStage)
        {
            var obj = Find(tileCode);
            return Instantiate(obj);
        }
        else
            return null;
    }
}
