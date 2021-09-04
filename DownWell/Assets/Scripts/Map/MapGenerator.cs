using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    MapManager mapManager = MapManager.instance;

    Tile[,] tiles;

    [Range(0, 100)]
    public int randomPercent = 30;
    [Range(0, 10)]
    public int proliferationRatio = 2;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = MapManager.instance;
    }

    public Tile[, ] GenerateMap()
    {
        InitTiles();

        InitWalls();

        ExpandWall();

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

            tiles[1, i].style = left < randomPercent ? TileStyle.Wall : TileStyle.Empty;
            tiles[mapManager.width - 2, i].style = right < randomPercent ? TileStyle.Wall : TileStyle.Empty;
        }

        ProliferateTile();
    }

    void ProliferateTile()
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 1; x < mapManager.width - 1; x++)
            {
                if(tiles[x, y].style != TileStyle.Empty)
                {
                    RandomTile(x, y - 1, tiles[x, y].style);
                    RandomTile(x, y + 1, tiles[x, y].style);
                    RandomTile(x - 1, y, tiles[x, y].style);
                    RandomTile(x + 1, y, tiles[x, y].style);
                }
            }
        }
    }

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
                        tiles[x, y].style = rand.Next(0, 10) < proliferationRatio ? TileStyle.Wall : TileStyle.Empty;
                        break;
                    case TileStyle.Block:
                        tiles[x, y].style = rand.Next(0, 10) < proliferationRatio ? TileStyle.Block : TileStyle.Empty;
                        break;
                }
                
            }
        }
    }
}

public enum TileStyle { Empty, Wall, Block }

public class Tile
{
    public TileStyle style = new TileStyle();
}
