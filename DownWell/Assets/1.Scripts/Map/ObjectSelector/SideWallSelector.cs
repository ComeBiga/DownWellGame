using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWallSelector : ObjectSelector
{
    public SideWallSelector(int code, params GameObject[] objects) : base(code, objects)
    {
    }

    public SideWallSelector(int min, int max, params GameObject[] objects) : base(min, max, objects)
    {
    }

    protected override GameObject Select(int tileCode)
    {
        //var obj = Find(tileCode);
        // Sprite result;

        // if (obj != null && 
        //     SetBlockAndPlatformSprite(obj.GetComponent<Wall>().name, out result))
        //     obj.GetComponent<SpriteRenderer>().sprite = result;

        return null; //Instantiate(obj);
    }

    // private bool SetBlockAndPlatformSprite(string name, out Sprite sprite)
    // {
    //     Sprite result;
    //     //StageDatabase stage = StageManager.instance.Current;
    //     StageDatabase stage = currentStage;

    //     switch(name)
    //     {
    //         case "Block":
    //             result = stage.BlockSprites[0];
    //             break;
    //         case "Platform":
    //             result = stage.PlatformSprites[0];
    //             break;
    //         case "Platform_L":
    //             result = stage.PlatformSprites[1];
    //             break;
    //         case "Platform_R":
    //             result = stage.PlatformSprites[2];
    //             break;
    //         case "ItemGiver":
    //             result = stage.ItemGiverSprites[0];
    //             break;
    //         case "ItemGiver_Lock":
    //             result = stage.ItemGiverLockSprites[0];
    //             break;
    //         default:
    //             sprite = null;
    //             return false;
    //     }

    //     sprite = result;
    //     return true;
    // }
}
