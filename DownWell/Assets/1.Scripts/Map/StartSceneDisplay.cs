using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapDisplay))]
public class StartSceneDisplay : MonoBehaviour
{
    LoadLevel levelLoader;
    MapDisplay md;

    public StageDatabase stageDB;

    private void Start()
    {
        levelLoader = GetComponent<LoadLevel>();
        md = GetComponent<MapDisplay>();

        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        yield return null;

        Level lv = levelLoader.LoadAndGet("StageStart");

        md.DisplayByDatabase(lv, stageDB);
    }
}
