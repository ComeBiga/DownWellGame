using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderAsBuild : LevelLoader
{
    public override Level Load(string path)
    {
        var textData = Resources.Load(path, typeof(TextAsset));
        Level lv = JsonToLevel(textData.ToString());

        return lv;
    }

    public override List<Level> LoadAll(string path)
    {
        var textDatas = Resources.LoadAll(path, typeof(TextAsset));
        List<Level> lvList = new List<Level>();

        foreach (var textData in textDatas)
        {
            //Debug.Log(textData.ToString());
            var lvs = JsonToLevel(textData.ToString());

            lvList.Add(lvs);
        }

        return lvList;
    }
}
