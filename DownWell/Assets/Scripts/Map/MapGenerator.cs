using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject wall;
    public int randomPercent = 30;

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

        System.Random rand = new System.Random((int)System.DateTime.Now.Ticks);

        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                tiles[x, y].style = rand.Next(0, 100) < randomPercent ? TileStyle.Wall : TileStyle.Empty;
            }
        }
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
