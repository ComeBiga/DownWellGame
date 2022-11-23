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
    [SerializeField] private GameObject startPos;

    [Header("Ratio")]
    [Range(0, 100)]
    public int blockRatio = 100;
    [Range(0, 100)]
    public int enemyRatio = 100;

    private List<GameObject> wallObjects;
    private List<Sprite> wallSprites;
    private List<GameObject> enemyObjects;

    [Header("Background")]
    public GameObject backgroundObject;
    public float brightness = 190;

    private WallSelector ws_root;
    private EnemySelector es_root;

    [Header("ObjectPool")]
    public int backgroundPoolCount;
    public float bgPoolOffset = 30f;
    private ObjectPooler backgroundPooler = new ObjectPooler();
    private ObjectPooler wallPooler = new ObjectPooler();

    private void Start()
    {
        mapManager = MapManager.instance;

        backgroundPooler.Init(backgroundObject, backgroundPoolCount, bgParent);
        wallPooler.Init(null, 0, wallParent);

        InitSelector();
    }

    private void InitSelector()
    {
        // wall
        ws_root = new WallSelector(100, 1000);
        var spws = new SpecialWallSelector(9);
        var ssws = new StartPosSelector(8);
        var sdws = new SideWallSelector(0, 99);

        ws_root.SetNext(spws);
        spws.SetNext(ssws);
        ssws.SetNext(sdws);

        ws_root.SetTileMap(tm_Wall, tmr_Wall);

        // enemy
        es_root = new EnemySelector(2000, 3000, enemyRatio);
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

    public void SetObjects(List<GameObject> mapObjects, List<Sprite> wallSprites, List<GameObject> enemyObjects)
    {
        this.wallObjects = mapObjects;
        this.wallSprites = wallSprites;
        this.enemyObjects = enemyObjects;
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
        // wall
        ws_root.InstantiateObject(tileCode, tilePosition, wallParent, currentStage);

        // enemy
        es_root.InstantiateObject(tileCode, tilePosition, enemyParent, currentStage);
    }

    private GameObject GetTileInstance(GameObject tileObject, float Xpos, float Ypos)
    {
        Vector2 tilePosition = new Vector2(Xpos, Ypos);

        var go = backgroundPooler.GetUnusedObject();
        go.transform.position = tilePosition;

        return go;
    }

    // 벽 외에 블럭 같은 것들도 tile로 생성할 수 있을 지 고민해보기
    private void SetWallTile(int tileCode, Vector3 position, StageDatabase currentStage)
    {
        var positionInt = new Vector3Int((int)position.x, (int)position.y, (int)position.z);
        var tileBase = currentStage.TileBases[tileCode - 100];

        tm_Wall.SetTile(positionInt, tileBase);
        tmr_Wall.sharedMaterial = currentStage.Materials[0];
    }
}
