using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    MapManager mapManager;

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

    //[Header("Objects")]
    //[SerializeField] private List<GameObject> mapObjects;

    //public GameObject wallObject;
    //public GameObject blockObject;
    //public List<GameObject> platformObjects;
    //public List<GameObject> extraObjects;

    [Header("Background")]
    //public bool displayBackground = true;
    public GameObject backgroundObject;
    public float brightness = 190;
    //public List<Sprite> background;
    //public GameObject background2by2;
    //public int background2by2Ratio = 10;

    private WallSelector ws_root;
    private EnemySelector es_root;

    private void Start()
    {
        mapManager = MapManager.instance;

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
    }

    public void ClearBackgrounds()
    {
        var mo = bgParent.GetComponentsInChildren<Transform>();

        foreach (var m in mo)
        {
            if (m != bgParent)
                Destroy(m.gameObject);
        }
    }

    //public void Display(Tile[,] generatedTiles)
    //{
    //    for (int y = 0; y < mapManager.height; y++)
    //    {
    //        for (int x = 0; x < mapManager.width; x++)
    //        {
    //            Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
    //                                                , -y + offset.y);

    //            if (generatedTiles[x, y].style == TileStyle.Wall)
    //                Instantiate(tileObject[0], tilePosition, Quaternion.identity);
    //            else if (generatedTiles[x, y].style == TileStyle.Block)
    //                Instantiate(tileObject[1], tilePosition, Quaternion.identity);
    //            else if (generatedTiles[x, y].style == TileStyle.Enemy)
    //                Instantiate(tileObject[2], tilePosition, Quaternion.identity);
    //        }
    //    }
    //}

    public void SetObjects(List<GameObject> mapObjects, List<Sprite> wallSprites, List<GameObject> enemyObjects)
    {
        this.wallObjects = mapObjects;
        this.wallSprites = wallSprites;
        this.enemyObjects = enemyObjects;
    }

    //public void SetObjects(StageDatabase currentStageDB)
    //{
    //    this.wallObjects = currentStageDB.MapObjects;
    //    this.wallSprites = currentStageDB.WallSprites;
    //    this.enemyObjects = currentStageDB.EnemyObjects;

    //    //SetMapObjectSprite(currentStageDB.BlockSprites, currentStageDB.PlatformSprites);
    //}

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

        //// Background
        //DisplayBackGround(level, Ypos);

        //// Wall
        //DisplayWall(level, Ypos);

        //// Enemy
        //DisplayEnemy(level, Ypos);

        return level.height;
    }

    private void DisplayBackground(int tileCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        // Background base
        var bgo = GetTileInstance(backgroundObject, tilePosition.x, tilePosition.y);
        bgo.GetComponent<SpriteRenderer>().sprite = BackgroundHandler.GetRandomBase(currentStage.bgInfo);
        bgo.GetComponent<SpriteRenderer>().color = new Color((float)brightness/255, (float)brightness /255, (float)brightness/255);

        // Background Decoration
        var decos = currentStage.bgInfo.deco;

        foreach (var deco in decos)
        {
            Sprite decoSprite;

            if (BackgroundHandler.Decorate(deco, out decoSprite))
            {
                bgo.GetComponent<SpriteRenderer>().sprite = decoSprite;
                bgo.GetComponent<SpriteRenderer>().sortingOrder = 2;
                break;
            }
        }
    }

    private void DisplayObject(int tileCode, Vector3 tilePosition, StageDatabase currentStage)
    {
        // wall
        //ws_root.InstantiateObject(tileCode, tilePosition, transform, currentStage);
        ws_root.InstantiateObject(tileCode, tilePosition, wallParent, currentStage);

        // enemy
        es_root.InstantiateObject(tileCode, tilePosition, enemyParent, currentStage);
    }

    private GameObject GetTileInstance(GameObject tileObject, float Xpos, float Ypos)
    {
        Vector2 tilePosition = new Vector2(Xpos, Ypos);

        var go = Instantiate(backgroundObject, tilePosition, Quaternion.identity, bgParent);

        return go;
    }

    //private void SetMapObjectSprite(List<Sprite> blockSprites, List<Sprite> platformSprites)
    //{
    //    // block
    //    blockObject.GetComponent<SpriteRenderer>().sprite = blockSprites[0];

    //    // platform
    //    for(int i = 0; i < platformObjects.Count; i++)
    //    {
    //        platformObjects[i].GetComponent<SpriteRenderer>().sprite = platformSprites[i];
    //    }
    //}

    #region Deprecated

    private void DisplayWall(Level level, int Ypos)
    {
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                int currentTile = level.tiles[y * mapManager.width + x];
                //Debug.Log(currentTile);
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x,
                                                    -y + offset.y + Ypos);

                ws_root.InstantiateObject(currentTile, tilePosition, wallParent, StageManager.instance.Current);
            }
        }
    }

    private void DisplayEnemy(Level level, int Ypos)
    {
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                int currentTile = level.tiles[y * mapManager.width + x];
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y + Ypos);

                es_root.InstantiateObject(currentTile, tilePosition, enemyParent, StageManager.instance.Current);
            }
        }
    }

    private void DisplayBackGround(Level level, int Ypos)
    {
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                // Background base
                var bgo = GetTileInstance(backgroundObject, -mapManager.width / 2 + x + offset.x, -y + offset.y + Ypos);
                bgo.GetComponent<SpriteRenderer>().sprite = BackgroundHandler.GetRandomBase(StageManager.instance.Current.bgInfo);

                // Background Decoration
                var decos = StageManager.instance.Current.bgInfo.deco;

                foreach (var deco in decos)
                {
                    Sprite decoSprite;

                    if (BackgroundHandler.Decorate(deco, out decoSprite))
                    {
                        bgo.GetComponent<SpriteRenderer>().sprite = decoSprite;
                        break;
                    }
                }
            }
        }
    }


    // Background 2 by 2
    //for (int y = 0; y < level.height; y+=3)
    //{
    //    for (int x = 0; x < mapManager.width; x+=3)
    //    {
    //        Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
    //                                            , -y + offset.y + Ypos);

    //        if (rand.Next(0, 100) < background2by2Ratio)
    //        {
    //            Instantiate(background2by2, tilePosition, Quaternion.identity, transform);
    //        }
    //    }
    //}

    //public void Display(int[,] generatedLevel, int[,] generatedStageGround)
    //{
    //    for (int y = 0; y < mapManager.height; y++)
    //    {
    //        for (int x = 0; x < mapManager.width; x++)
    //        {
    //            Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
    //                                                , -y + offset.y);

    //            if(generatedLevel[x, y] >= 100 && generatedLevel[x, y] <= 1000)
    //            {
    //                var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == 1);
    //                //Debug.Log(generatedLevel[x, y]);
    //                GameObject wall;
    //                if (wallObject != null)
    //                {
    //                    wall = Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
    //                    wall.GetComponent<SpriteRenderer>().sprite = wallSprites[generatedLevel[x, y] - 100];
    //                }
    //            }
    //            //else if(generatedLevel[x, y] > 10 && generatedLevel[x, y] < 100)
    //            //{
    //            //    var enemyObject = enemyObjects.Find(g => g.GetComponent<Enemy>().info.code == generatedLevel[x, y]);
    //            //    Instantiate(enemyObject, tilePosition, Quaternion.identity, parent);
    //            //}
    //            else if(generatedLevel[x, y] <= 10)
    //            {
    //                var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == generatedLevel[x, y]);

    //                if (wallObject != null)
    //                    Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
    //            }
    //        }
    //    }

    //    for (int y = 0; y < mapManager.height; y++)
    //    {
    //        for (int x = 0; x < mapManager.width; x++)
    //        {
    //            Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
    //                                                , -y + offset.y);

    //            if (generatedLevel[x, y] > 2000 && generatedLevel[x, y] <= 3000)
    //            {
    //                var enemyObject = enemyObjects.Find(g => g.GetComponent<Enemy>().info.code == generatedLevel[x, y]);
    //                Instantiate(enemyObject, tilePosition, Quaternion.identity, parent);
    //            }
    //        }
    //    }

    //    // StageGround
    //    //for(int y = 0; y < generatedStageGround.GetLength(0); y++)
    //    //{
    //    //    for(int x = 0; x < generatedStageGround.GetLength(1); x++)
    //    //    {
    //    //        Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
    //    //                                            , -y + offset.y -mapManager.height);

    //    //        if(generatedStageGround[y, x] == 1)
    //    //            Instantiate(wallObjects[0], tilePosition, Quaternion.identity, parent);

    //    //    }
    //    //}

    //    List<Level> stageGrounds = LoadLevel.instance.GetObjects("StageGround");
    //    Level stageGround = stageGrounds[0];

    //    for(int y = 0; y < stageGround.height; y++)
    //    {
    //        for(int x= 0; x < stageGround.width; x++)
    //        {
    //            Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
    //                                                , -y + offset.y - mapManager.height);

    //            if (stageGround.tiles[y * stageGround.width + x] >= 100)
    //            {
    //                var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == 1);
    //                //Debug.Log(generatedLevel[x, y]);
    //                GameObject wall;
    //                if (wallObject != null)
    //                {
    //                    wall = Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
    //                    wall.GetComponent<SpriteRenderer>().sprite = wallSprites[stageGround.tiles[y * stageGround.width + x] - 100];
    //                }
    //            }
    //            else if (stageGround.tiles[y * stageGround.width + x] == 2)
    //            {
    //                var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.name == "StageEnd");

    //                if (wallObject != null)
    //                    Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
    //            }

    //            //if (stageGround.tiles[y * stageGround.width + x] == 1)
    //            //    Instantiate(wallObjects[0], tilePosition, Quaternion.identity, parent);
    //        }
    //    }
    //}

    #endregion
}
