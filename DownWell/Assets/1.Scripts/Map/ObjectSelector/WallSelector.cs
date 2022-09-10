using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSelector : ObjectSelector
{
    public WallSelector(int code, params GameObject[] objects) : base(code, objects)
    {
        
    }

    public WallSelector(int min, int max, params GameObject[] objects) : base(min, max, objects)
    {

    }

    protected override GameObject Select(int tileCode)
    {
        GameObject obj = Find(1);
        var wall = Instantiate(obj);
        wall.GetComponent<SpriteRenderer>().sprite = currentStage.WallSprites[tileCode - 100];
        
        return wall;
    }
}
