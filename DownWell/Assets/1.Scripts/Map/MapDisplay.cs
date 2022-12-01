using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class MapDisplay : MonoBehaviour
{
    MapManager mapManager;

    public Tilemap tm_Wall;
    public Tilemap tm_Background;

    public TilemapRenderer tmr_Wall;
    public TilemapRenderer tmr_Background;

    [Header("Parents")]
    public Transform parent;
    public Transform wallParent;
    public Transform enemyParent;
    public Transform bgParent;

    [Header("Position")]
    public Vector3 offset;

    [Header("Ratio")]
    [Range(0, 100)]
    public int blockRatio = 100;
    [Range(0, 100)]
    public int enemyRatio = 100;

    [Header("Background")]
    public GameObject backgroundObject;
    public float brightness = 190;

    [Header("ObjectPool")]
    public int backgroundPoolCount;
    public float bgPoolOffset = 30f;
    private ObjectPooler backgroundPooler = new ObjectPooler();
    private ObjectPooler wallPooler = new ObjectPooler();

    private Dictionary<int, Wall> mapObjects = new Dictionary<int, Wall>(50);

    private void Start()
    {
        mapManager = MapManager.instance;

        LoadMapObjects();

        backgroundPooler.Init(backgroundObject, backgroundPoolCount, bgParent);
        wallPooler.Init(null, 0, wallParent);
    }

    private void LoadMapObjects()
    {
        var loadedMapObjects = Resources.LoadAll<Wall>("MapObjects");

        for(int i = 0; i< loadedMapObjects.Length; ++i)
        {
            var value = loadedMapObjects[i];
            var key = value.info.code;

            mapObjects.Add(key, value);
        }
    }

    public void ClearAll()
    {
        ClearWalls();
        ClearEnemies();
        ClearBackgrounds();
    }

    public void ClearWalls()
    {
        var mo = wallParent.GetComponentsInChildren<Transform>();

        foreach (var m in mo)
        {
            if (m != wallParent)
                Destroy(m.gameObject);
        }
    }

    public void ClearEnemies()
    {
        var mo = enemyParent.GetComponentsInChildren<Transform>();

        foreach (var m in mo)
        {
            if (m != enemyParent)
                Destroy(m.gameObject);
        }

        tm_Wall.ClearAllTiles();
    }

    public void ClearBackgrounds()
    {
        var mo = bgParent.GetComponentsInChildren<Transform>();

        for (int i = 1; i < mo.Count(); i++)
        {
            if(mo[i]?.gameObject.activeSelf == true)
                backgroundPooler.SetUnused(mo[i]?.gameObject);
        }
    }

    private void Update() 
    {    
        ClearBackgroundOutOfScreen();
    }

    private void ClearBackgroundOutOfScreen()
    {
        var mo = bgParent.GetComponentsInChildren<Transform>();
        mo = mo.Where(o => o.gameObject.activeSelf == true).ToArray();

        for (int i = 1; i < mo.Count(); i++)
        {
            var orthoSize = Camera.main.orthographicSize;
            var pivot = PlayerManager.instance.playerObject.transform.position.y + (orthoSize + bgPoolOffset);
            if(mo[i].localPosition.y > pivot)
            {
                backgroundPooler.SetUnused(mo[i].gameObject);
            }
        }
    }

    public void DisplayByDatabase(Level level, StageDatabase stageDB)
    {
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < level.width; x++)
            {
                int currentTile = level.tiles[y * level.width + x];
                Vector2 tilePosition = new Vector2(-level.width / 2 + x + offset.x,
                                                    -y + offset.y);

                DisplayBackground(currentTile, tilePosition, stageDB);

                DisplayObject(currentTile, tilePosition, stageDB);
            }
        }
    }

    public int Display(Level level, int Ypos)
    {
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                int currentTile = level.tiles[y * mapManager.width + x];
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x,
                                                    -y + offset.y + Ypos);

                DisplayBackground(currentTile, tilePosition, StageManager.instance.Current);

                DisplayObject(currentTile, tilePosition, StageManager.instance.Current);
            }
        }

        return level.height;
    }

    private void DisplayBackground(int tileCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        // Background base
        var tileIndex = CatDown.Random.Get().Next(20, 23);
        tm_Background.SetTile(new Vector3Int((int)tilePosition.x, (int)tilePosition.y, (int)tilePosition.z), currentStage.TileBases[tileIndex]);
        tm_Background.color = new Color((float)brightness/255, (float)brightness /255, (float)brightness/255);
        tmr_Background.sharedMaterial = currentStage.Materials[1];
    }

    private void DisplayObject(int tileCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        if(tileCode >= 0 && tileCode < 1000)
        {
            // wall
            CreateWall(tileCode, tilePosition, currentStage);
            CreateObjects(tileCode, tilePosition, currentStage);
        }
        else if(tileCode >= 2000 && tileCode < 3000)
        {
            // enemy
            CreateEnemy(tileCode, tilePosition, currentStage);
        }
    }

    private GameObject GetTileInstance(GameObject tileObject, float Xpos, float Ypos)
    {
        Vector2 tilePosition = new Vector2(Xpos, Ypos);

        var go = backgroundPooler.GetUnusedObject();
        go.transform.position = tilePosition;

        return go;
    }

    private void CreateWall(int tileCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        if(tileCode < 100 || tileCode > 200)
            return;

        var position = new Vector3Int((int)tilePosition.x, (int)tilePosition.y, (int)tilePosition.z);
        var tileBase = currentStage.TileBases[tileCode - 100];

        tm_Wall.SetTile(position, tileBase);
        tmr_Wall.sharedMaterial = currentStage.Materials[0];
    }

    private void CreateObjects(int tileCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        Wall obj = null;
        if(mapObjects.TryGetValue(tileCode, out obj) == false)
            return;

        var instanceObj = Instantiate(obj, tilePosition, Quaternion.identity, wallParent);

        if(currentStage.GetMapObjectInfo(tileCode) != null)
            instanceObj.GetComponent<SpriteRenderer>().sprite = currentStage.GetMapObjectInfo(tileCode).sprite;
    }

    private void CreateEnemy(int enemyCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        if(CatDown.Random.Get().Next(100) < enemyRatio)
        {
            var obj = DataManager.GetEnemy(enemyCode);
            Instantiate(obj, tilePosition, Quaternion.identity, enemyParent);
        }
    }
}
