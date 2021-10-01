using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    #region Singleton
    public static LevelEditorManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public int width = 11;
    public int height = 11;

    public GameObject levelTiles;

    [HideInInspector]
    public TileInfo[] tiles;

    private void Start()
    {
        tiles = levelTiles.GetComponentsInChildren<TileInfo>();

        //LoadLevel(tiles);
    }

    public void LoadLevel(TileInfo[] _tiles)
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                var tileCode = _tiles[y * width + x].tileCode;

                ChangeTile(tiles[y * width + x].transform, (int)tileCode);
            }
        }
    }

    public void LoadLevel(Level level)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                ChangeTile(tiles[y * width + x].transform, level.tiles[y * width + x]);
            }
        }
    }

    void ChangeTile(Transform tile, int tileCode)
    {
        if (tileCode > 10)
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.enemyBrushes.Find(b => b.code == tileCode).sprite;
        else if (tileCode > 0)
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.wallBrushes.Find(b => b.code == tileCode).sprite;
        else
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.eraserBrush.sprite;

        tile.GetComponent<TileInfo>().tileCode = tileCode;
    }

}
