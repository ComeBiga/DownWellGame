using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosSelector : ObjectSelector
{
    public StartPosSelector(int code, params GameObject[] objects) : base(code, objects)
    {
    }

    public StartPosSelector(int min, int max, params GameObject[] objects) : base(min, max, objects)
    {
    }

    protected override GameObject Select(int tileCode)
    {
        var startPosObject = Instantiate(new GameObject());
        startPosObject.name = "StageStart";
        startPosObject.tag = "StageStart";

        if(GameManager.instance != null) 
            GameManager.instance.startPos = startPosObject.transform;

        return startPosObject;
    }
}
