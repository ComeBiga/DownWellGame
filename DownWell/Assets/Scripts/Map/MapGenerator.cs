using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject wall;

    [Range(0, 100)]
    public int randomPercent = 30;
    [Range(0, 10)]
    public int proliferationRatio = 2;

    Tile[,] tiles;
    public Tile[,] Tiles { get { return tiles; } }

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tiles[x, y] = new Tile();
            }
        }

        GenerateMap();

        Display();
    }

    void InitTiles()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                tiles[x, y].style = TileStyle.Empty;
            }
        }
    }

    void InitWalls()
    {
        for(int i = 0; i< height; i++)
        {
            tiles[0, i].style = TileStyle.Wall;
            tiles[width - 1, i].style = TileStyle.Wall;
        }
    }

    void GenerateMap()
    {
        InitTiles();
        InitWalls();

        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int i = 0; i < height; i++)
        {
            tiles[1, i].style = rand.Next(0, 100) < randomPercent ? TileStyle.Wall : TileStyle.Empty;
            tiles[width - 2, i].style = rand.Next(0, 100) < randomPercent ? TileStyle.Wall : TileStyle.Empty;
        }

        ProliferateTile();
    }

    void ProliferateTile()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 1; x < width; x++)
            {
                if(tiles[x, y].style == TileStyle.Wall)
                {
                    RandomTile(x, y - 1);
                    RandomTile(x, y + 1);
                    RandomTile(x - 1, y);
                    RandomTile(x + 1, y);
                }
            }
        }
    }

    void VisitNextTile(int tileX, int tileY, int proliferationRatio)
    {

    }

    int RandomTile(int x, int y)
    {
        string seed = (Time.time + Random.value).ToString();

        System.Random rand = new System.Random(seed.GetHashCode());

        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (tiles[x, y].style != TileStyle.Wall)
            {
                tiles[x, y].style = rand.Next(0, 10) < proliferationRatio ? TileStyle.Wall : TileStyle.Empty;
                return 1;
            }
        }

        return 0;
    }

    void Display()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (tiles[x, y].style == TileStyle.Wall)
                    Instantiate(wall, new Vector2(-width / 2 + x, -y), Quaternion.identity);
            }
        }
    }
}

public enum TileStyle { Empty, Wall }

public class Tile
{
    public TileStyle style = new TileStyle();
}
