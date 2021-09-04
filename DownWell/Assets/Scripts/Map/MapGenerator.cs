using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    MapManager mapManager = MapManager.instance;

    Tile[,] tiles;

    [Header("Wall prop")]
    [Range(0, 100)]
    public int wallRate = 30;
    [Range(0, 100)]
    public int wallProliferation = 2;
    [Range(0, 8)]
    public int wallSmoothRate = 2;
    public int wallSmoothTime = 3;
    
    [Header("Block prop")]
    [Range(0, 100)]
    public int blockRate = 30;
    [Range(0, 100)]
    public int blockProliferation = 2;
    [Range(0, 8)]
    public int blockSmoothRate = 2;
    public int blockSmoothTime = 3;


    // Start is called before the first frame update
    void Start()
    {
        mapManager = MapManager.instance;
    }

    public Tile[, ] GenerateMap()
    {
        InitTiles();

        InitWalls();

        //ExpandWall();
        RandomFillMap();

        for (int i = 0; i < wallSmoothTime; i++)
            SmoothMap(TileStyle.Wall, wallSmoothRate, true);

        //GenerateBlock();

        return tiles;
    }

    void InitTiles()
    {
        // Init tiles
        tiles = new Tile[mapManager.width, mapManager.height];
        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                tiles[x, y] = new Tile();
            }
        }

        // Init tiles.style
        for (int y = 0; y < mapManager.height; y++)
        {
            for(int x = 0; x < mapManager.width; x++)
            {
                tiles[x, y].style = TileStyle.Empty;
            }
        }
    }

    void InitWalls()
    {
        for (int i = 0; i< mapManager.height; i++)
        {
            tiles[0, i].style = TileStyle.Wall;
            tiles[mapManager.width - 1, i].style = TileStyle.Wall;
        }
    }

    void ExpandWall()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int i = 0; i < mapManager.height; i++)
        {
            int left = rand.Next(0, 100);
            int right = rand.Next(0, 100);

            //Debug.Log(i + " : " + left + ", " + right);

            tiles[1, i].style = left < wallRate ? TileStyle.Wall : TileStyle.Empty;
            tiles[mapManager.width - 2, i].style = right < wallRate ? TileStyle.Wall : TileStyle.Empty;
        }

        ProliferateTile(TileStyle.Wall);

        for (int i = 0; i < wallSmoothTime; i++)
            SmoothMap(TileStyle.Wall, wallSmoothRate);
    }

    void ProliferateTile(TileStyle styleTo)
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 1; x < mapManager.width - 1; x++)
            {
                //EliminateTile(x, y, styleTo);

                if(tiles[x, y].style == styleTo)
                {
                    //RandomTile(x, y - 1, tiles[x, y].style);
                    //RandomTile(x, y + 1, tiles[x, y].style);
                    RandomTile(x - 1, y, tiles[x, y].style);
                    RandomTile(x + 1, y, tiles[x, y].style);
                }
            }
        }
    }

    void EliminateTile(int _x, int _y, TileStyle styleTo)
    {
        int count = 0;
        for(int y = _y - 1; y <= _y + 1; y++)
        {
            for(int x = _x - 1; x <= _x + 1; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                    if ((x != _x || y != _y) && tiles[x, y].style == styleTo)
                        count++;
            }
        }

        Debug.Log(styleTo.ToString() + " : " + count);

        if (count < wallSmoothRate)
            tiles[_x, _y].style = TileStyle.Empty;
    }

    #region Procedural Cave Gen
    void RandomFillMap()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for(int y = 0; y < mapManager.height; y++)
        {
            for(int x = 1; x < mapManager.width - 1; x++)
            {
                tiles[x, y].style = rand.Next(0, 100) < wallRate ? TileStyle.Wall : TileStyle.Empty;
            }
        }
    }

    int GetSurroundTileCount(int _x, int _y, TileStyle styleTo)
    {
        int count = 0;
        for (int y = _y - 1; y <= _y + 1; y++)
        {
            for (int x = _x - 1; x <= _x + 1; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    if ((x != _x || y != _y) && tiles[x, y].style == styleTo)
                        count++;
                }
                //else
                //    count++;
            }
        }

        return count;
    }

    int GetFourDirTileCount(int x, int y, TileStyle styleTo)
    {
        int count = 0;

        count += CheckSurroundTile(x, y-1, styleTo);
        count += CheckSurroundTile(x+1, y, styleTo);
        count += CheckSurroundTile(x, y+1, styleTo);
        count += CheckSurroundTile(x-1, y, styleTo);

        return count;
    }

    int CheckSurroundTile(int x, int y, TileStyle styleTo)
    {
        if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
            if (tiles[x, y].style == styleTo) return 1;

        return 0;
    }

    void SmoothMap(TileStyle styleTo, int smoothRate, bool expandDir = false)
    {
        for(int y = 0; y < mapManager.height; y++)
        {
            for (int x = 1; x < 5; x++)
            {
                //if (tiles[x, y].style == TileStyle.Empty)
                {
                    int count;

                    if (expandDir)
                        count = GetSurroundTileCount(x, y, TileStyle.Wall);
                    else
                        count = GetFourDirTileCount(x, y, styleTo);

                    Debug.Log(new Vector2(x, y).ToString() + " - " + count);

                    if (count >= smoothRate)
                        tiles[x, y].style = styleTo;
                    else if(count < smoothRate)
                        tiles[x, y].style = TileStyle.Empty;
                }
            }
            for(int x = mapManager.width - 2; x >= 5; x--)
            {
                {
                    int count;

                    if (expandDir)
                        count = GetSurroundTileCount(x, y, TileStyle.Wall);
                    else
                        count = GetFourDirTileCount(x, y, styleTo);

                    Debug.Log(new Vector2(x, y).ToString() + " - " + count);

                    if (count >= smoothRate)
                        tiles[x, y].style = styleTo;
                    else if (count < smoothRate)
                        tiles[x, y].style = TileStyle.Empty;
                }
            }
        }
    }
    #endregion

    void RandomTile(int x, int y, TileStyle rootStyle)
    {
        string seed = (Time.time + Random.value).ToString();

        System.Random rand = new System.Random(seed.GetHashCode());

        if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
        {
            if (tiles[x, y].style == TileStyle.Empty)
            {
                switch(rootStyle)
                {
                    case TileStyle.Wall:
                        tiles[x, y].style = rand.Next(0, 100) < wallProliferation ? TileStyle.Wall : TileStyle.Empty;
                        break;
                    case TileStyle.Block:
                        tiles[x, y].style = rand.Next(0, 100) < blockProliferation ? TileStyle.Block : TileStyle.Empty;
                        break;
                }
                
            }
        }
    }

    #region Block Generation

    void GenerateBlock()
    {
        string seed = (Time.time + Random.value).ToString();

        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            for(int x = 1; x < mapManager.width - 1; x++)
            {
                if(tiles[x, y].style == TileStyle.Empty)
                {
                    tiles[x, y].style = rand.Next(0, 100) < blockRate ? TileStyle.Block : TileStyle.Empty;
                }
            }
        }

        for(int i = 0; i< 3; i++)
            ProliferateTile(TileStyle.Block);

        for (int i = 0; i < blockSmoothTime; i++)
            SmoothMap(TileStyle.Block, blockSmoothRate, true);
    }

    #endregion
}

public enum TileStyle { Empty, Wall, Block }

public class Tile
{
    public TileStyle style = new TileStyle();
}
