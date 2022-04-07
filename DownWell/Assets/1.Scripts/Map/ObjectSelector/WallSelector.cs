using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSelector : ObjectSelector
{
    public WallSelector(int code) : base(code)
    {
        
    }

    public WallSelector(int min, int max) : base(min, max)
    {

    }

    protected override GameObject Select(int tileCode)
    {
        GameObject obj = Find(1);
        var wall = Instantiate(obj);
        wall.GetComponent<SpriteRenderer>().sprite = StageManager.instance.Current.WallSprites[tileCode - 100];
        
        return wall;
    }

}
