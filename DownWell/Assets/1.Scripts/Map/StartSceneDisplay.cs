using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapDisplay))]
public class StartSceneDisplay : MonoBehaviour
{
    LoadLevel levelLoader;
    MapDisplay md;

    public StageDatabase stageDB;
    public string startSceneLevelPath = "Levels/StageStart/";

    private void Start()
    {
        levelLoader = GetComponent<LoadLevel>();
        md = GetComponent<MapDisplay>();

        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        yield return null;

        //var lv = levelLoader.LoadAndGetLevel("StageStart");
        var lvs = levelLoader.GetLevels(startSceneLevelPath);


        md.DisplayByDatabase(lvs[0], stageDB);
    }
}
