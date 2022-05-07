using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallSelector : ObjectSelector
{
    public SpecialWallSelector(int code, params GameObject[] objects) : base(code, objects)
    {
    }

    public SpecialWallSelector(int min, int max, params GameObject[] objects) : base(min, max, objects)
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
