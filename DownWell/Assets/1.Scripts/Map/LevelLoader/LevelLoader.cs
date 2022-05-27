using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelLoader
{
    public LevelLoader()
    {

    }

    public abstract List<Level> LoadAll(string path);
    public abstract Level Load(string path);

    protected Level JsonToLevel(string jsonData)
    {
        return JsonUtility.FromJson<Level>(jsonData);
    }
}
