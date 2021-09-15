using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    MapManager mapManager;

    [Range(0, 100)]
    public int wallRatio = 15;

    int[,] map;

    int[,] wall_2_2 = new int[4, 4] { { 1, 0, 0, 0 },
                                      { 1, 1, 1, 1 },
                                      { 1, 1, 1, 0 },
                                      { 1, 0, 0, 0 } };

    int[,] wall_2_3 = new int[6, 6] { { 1, 0, 0, 0, 0, 0 },
                                      { 1, 1, 1, 1, 0, 0 },
                                      { 1, 1, 1, 1, 1, 0 },
                                      { 1, 1, 1, 1, 1, 0 },
                                      { 1, 0, 0, 1, 1, 0 },
                                      { 1, 0, 0, 0, 1, 0 } };

    // Start is called before the first frame update
    void Start()
    {
        mapManager = MapManager.instance;
    }

    void InitMap()
    {
        map = new int[mapManager.width, mapManager.height];

        for(int y = 0; y < mapManager.height; y++)
        {
            for(int x = 0; x < mapManager.width; x++)
            {
                map[x, y] = 0;
            }
        }

        InitWall();
    }

    void InitWall()
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            map[0, y] = 1;
            map[mapManager.width - 1, y] = 1;
        }
    }

    public int[, ] GenerateLevel()
    {
        InitMap();

        RandomPlaceWall();

        return map;
    }

    void RandomPlaceWall()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            if (rand.Next(0, 100) < wallRatio)
                FillWall(0, y);
            //map[mapManager.width - 1, y] = 1;
        }
    }

    void FillWall(int _x, int _y)
    {
        int wallWidth = wall_2_3.GetLength(0);
        int wallHeight = wall_2_3.GetLength(1);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x; x < _x + wallWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    map[x, y] = wall_2_3[y - _y, x - _x];
                }
            }
        }
    }
}
