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
        Sprite result;

        if (obj != null && 
            SetBlockAndPlatformSprite(obj.GetComponent<Wall>().name, out result))
            obj.GetComponent<SpriteRenderer>().sprite = result;

        return Instantiate(obj);
    }

    private bool SetBlockAndPlatformSprite(string name, out Sprite sprite)
    {
        Sprite result;
        StageDatabase stage = StageManager.instance.Current;

        switch(name)
        {
            case "Block":
                result = stage.BlockSprites[0];
                break;
            case "Platform":
                result = stage.PlatformSprites[0];
                break;
            case "Platform_L":
                result = stage.PlatformSprites[1];
                break;
            case "Platform_R":
                result = stage.PlatformSprites[2];
                break;
            default:
                sprite = null;
                return false;
        }

        sprite = result;
        return true;
    }
}
