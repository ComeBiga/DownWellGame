using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    MapManager mapManager;

    [Range(0, 100)]
    public int wallRatio = 15;

    int[,] map;

    #region LeftWall
    List<int[,]> wallListLeft = new List<int[,]>();

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

    #endregion

    #region RightWall
    List<int[,]> wallListRight = new List<int[,]>();

    int[,] R_wall_2_2 = new int[4, 4] { { 0, 0, 0, 1 },
                                        { 0, 0, 1, 1 },
                                        { 0, 0, 1, 1 },
                                        { 0, 0, 0, 1 } };

    int[,] R_wall_2_3 = new int[6, 6] { { 0, 1, 1, 0, 0, 1 },
                                        { 0, 1, 1, 1, 1, 1 },
                                        { 0, 0, 1, 1, 1, 1 },
                                        { 0, 0, 0, 1, 1, 1 },
                                        { 0, 0, 0, 0, 0, 1 },
                                        { 0, 0, 0, 0, 0, 1 } };
    #endregion

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

    void InitwallListLeft()
    {
        wallListLeft.Add(wall_2_2);
        wallListLeft.Add(wall_2_3);
    }

    void InitwallListRight()
    {
        wallListRight.Add(R_wall_2_2);
        wallListRight.Add(R_wall_2_3);
    }

    public int[, ] GenerateLevel()
    {
        InitMap();

        InitwallListLeft();
        InitwallListRight();

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
                FillWallLeft(0, y);

            if (rand.Next(0, 100) < wallRatio)
                FillWallRight(mapManager.width, y);
        }
    }

    void FillWallLeft(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] wallRandom = wallListLeft[rand.Next(0, wallListLeft.Count)];

        int wallWidth = wallRandom.GetLength(0);
        int wallHeight = wallRandom.GetLength(1);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x; x < _x + wallWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    map[x, y] = wallRandom[y - _y, x - _x];
                }
            }
        }
    }

    void FillWallRight(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] wallRandom = wallListRight[rand.Next(0, wallListRight.Count)];

        int wallWidth = wallRandom.GetLength(0);
        int wallHeight = wallRandom.GetLength(1);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x - wallWidth; x < _x; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    map[x, y] = wallRandom[y - _y, x - (_x - wallWidth)];
                }
            }
        }
    }
}
