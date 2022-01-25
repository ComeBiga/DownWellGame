using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushManager : MonoBehaviour
{
    #region Singleton
    public static BrushManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public BrushDatabase brushDB;
    public StageDatabase stage;

    public LevelObject currentBrush;
    public List<LevelObject> wallBrushes;
    public List<LevelObject> enemyBrushes;
    public LevelObject eraserBrush;

    private void Start()
    {
        currentBrush = eraserBrush;
    }

    public void ChangeBrush(LevelObject brush)
    {
        currentBrush = brush;
    }

    public List<LevelObject> GetWallObjects()
    {
        List<LevelObject> objs = new List<LevelObject>();

        var mo = stage.MapObjects;

        foreach(var lo in mo)
        {
            objs.Add(lo.GetComponent<Wall>().info);
        }

        return objs;
    }

    public List<LevelObject> GetEnemyObjects()
    {
        List<LevelObject> objs = new List<LevelObject>();

        var eo = stage.EnemyObjects;

        foreach (var lo in eo)
        {
            objs.Add(lo.GetComponent<Enemy>().info);
        }

        return objs;
    }

    public void ChangeWallBrush(int brushCode)
    {
        currentBrush = wallBrushes.Find(t => t.code == brushCode);
    }

    public void ChangeEnemyBrush(int brushCode)
    {
        //currentBrush = enemyBrushes.Find(t => t.code == brushCode);
        currentBrush = enemyBrushes[brushCode];
    }

    public void ChangeToEraser()
    {
        currentBrush = eraserBrush;
    }

    public void PaintTile(TileInfo tileInfo)
    {
        tileInfo.GetComponent<SpriteRenderer>().sprite = currentBrush.sprite;
        tileInfo.tileCode = currentBrush.code;
    }
}
