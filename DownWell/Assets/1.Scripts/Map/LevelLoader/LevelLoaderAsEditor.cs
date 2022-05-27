using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoaderAsEditor : LevelLoader
{
    public override Level Load(string path)
    {
        string jsonStr = File.ReadAllText(path);
        var lv = JsonToLevel(jsonStr);

        return lv;
    }

    public override List<Level> LoadAll(string path)
    {
        Debug.Log(path);
        string[] directories = Directory.GetFiles(path, "*.json");
        List<Level> lvList = new List<Level>();

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var lvs = JsonToLevel(jsonStr);

            lvList.Add(lvs);
        }

        return lvList;
    }
}
