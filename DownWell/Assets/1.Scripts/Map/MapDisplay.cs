using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    MapManager mapManager;

    public Transform parent;
    public Vector3 offset;

    [Header("Ratio")]
    [Range(0, 100)]
    public int blockRatio = 100;
    [Range(0, 100)]
    public int enemyRatio = 100;

    [Header("Objects")]
    public List<GameObject> wallObjects;
    public List<Sprite> wallSprites;
    public List<GameObject> enemyObjects;

    [Header("Background")]
    public bool displayBackground = true;
    public GameObject backgroundObject;
    public List<Sprite> background;
    public GameObject background2by2;
    public int background2by2Ratio = 10;

    public static int enemyCount;

    private void Start()
    {
        mapManager = MapManager.instance;

        enemyCount = enemyObjects.Count;
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

    public int Display(Level level, int Ypos)
    {
        Debug.Log("Display");
        Debug.Log(level.width);
        Debug.Log("name:"+level.name);

        if(displayBackground) DisplayBackGround(level, Ypos);

        // Wall
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                int currentTile = level.tiles[y * mapManager.width + x];
                //Debug.Log(currentTile);
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x,
                                                    -y + offset.y + Ypos);

                if (currentTile >= 100 && currentTile <= 1000)
                {
                    var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == 1);
                    //Debug.Log(generatedLevel[x, y]);
                    GameObject wall;
                    if (wallObject != null)
                    {
                        wall = Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
                        //Debug.Log(wall);
                        wall.GetComponent<SpriteRenderer>().sprite = wallSprites[currentTile - 100];
                    }
                }
                else if(currentTile == 9)
                {
                    if (BossStageManager.instance.BossStage)
                    {
                        var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == currentTile);

                        if (wallObject != null)
                            Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
                    }
                }
                else if (currentTile <= 10)
                {
                    var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == currentTile);

                    if (wallObject != null)
                        Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
                }
            }
        }

        // Enemy
        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                int currentTile = level.tiles[y * mapManager.width + x];
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y + Ypos);

                if (currentTile > 2000 && currentTile <= 3000)
                {
                    string seed = (Time.time + Random.value).ToString();
                    System.Random rand = new System.Random(seed.GetHashCode());

                    if (rand.Next(0, 100) < enemyRatio)
                    {
                        var enemyObject = enemyObjects.Find(g => g.GetComponent<Enemy>().info.code == currentTile);
                        Instantiate(enemyObject, tilePosition, Quaternion.identity, parent);
                    }
                }
            }
        }

        return level.height;
    }

    public void DisplayBackGround(Level level, int Ypos)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                var randIndex = rand.Next(0, background.Count);
                var randTile = background[randIndex];

                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y + Ypos);

                var bgo = Instantiate(backgroundObject, tilePosition, Quaternion.identity, transform);
                bgo.GetComponent<SpriteRenderer>().sprite = randTile;

                if (rand.Next(0, 100) < background2by2Ratio)
                {
                    bgo = Instantiate(background2by2, tilePosition, Quaternion.identity, transform);
                }
            }
        }

        for (int y = 0; y < level.height; y+=5)
        {
            for (int x = 0; x < mapManager.width; x+=5)
            {
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y + Ypos);

                if (rand.Next(0, 100) < background2by2Ratio)
                {
                    Instantiate(background2by2, tilePosition, Quaternion.identity, transform);
                }
            }
        }
    }

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
}
