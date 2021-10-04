using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    MapManager mapManager;

    public Transform parent;
    public Vector3 offset;
    public List<GameObject> wallObjects;
    public List<GameObject> enemyObjects;

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

    public void Display(int[,] generatedLevel, int[,] generatedStageGround)
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y);

                if(generatedLevel[x, y] > 10)
                {
                    var enemyObject = enemyObjects.Find(g => g.GetComponent<Enemy>().info.code == generatedLevel[x, y]);
                    Instantiate(enemyObject, tilePosition, Quaternion.identity, parent);
                }
                else
                {
                    var wallObject = wallObjects.Find(g => g.GetComponent<Wall>().info.code == generatedLevel[x, y]);

                    if(wallObject != null)
                        Instantiate(wallObject, tilePosition, Quaternion.identity, parent);
                }
            }
        }

        // StageGround
        for(int y = 0; y < generatedStageGround.GetLength(0); y++)
        {
            for(int x = 0; x < generatedStageGround.GetLength(1); x++)
            {
                Vector2 tilePosition = new Vector2(-mapManager.width / 2 + x + offset.x
                                                    , -y + offset.y -mapManager.height);

                if(generatedStageGround[y, x] == 1)
                    Instantiate(wallObjects[0], tilePosition, Quaternion.identity, parent);

            }
        }
    }
}
