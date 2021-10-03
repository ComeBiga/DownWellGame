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
    public GameObject space;

    [HideInInspector]
    public List<TileInfo> tiles;

    private int canvasWidth;
    private int canvasHeight;

    private void Start()
    {
        //tiles = levelTiles.GetComponentsInChildren<TileInfo>();

        //LoadLevel(tiles);
        InitCanvas(width, height);
    }

    public Vector2 getCanvasSize()
    {
        return new Vector2(canvasWidth, canvasHeight);
    }

    void InitCanvas(int width, int height)
    {
        // 카메라 위치 조정
        int longerSize = width >= height ? width : height;
        Camera.main.orthographicSize = longerSize * .5f;
        Camera.main.transform.position = 
            (longerSize % 2 == 0) ? new Vector3(-.5f, .5f, Camera.main.transform.position.z) : new Vector3(0, 0, Camera.main.transform.position.z);

        canvasWidth = width;
        canvasHeight = height;

        tiles = new List<TileInfo>();

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                var pos = new Vector2(x - width / 2, height / 2 - y);
                var madeSpace = Instantiate(space, pos, Quaternion.identity, levelTiles.transform);
                //tiles[y * width + x] = madeSpace.GetComponent<TileInfo>();
                tiles.Add(madeSpace.GetComponent<TileInfo>());
            }
        }
    }

    public void ResetCanvas(int resizeWidth, int resizeHeight)
    {
        if (resizeWidth == canvasWidth && resizeHeight == canvasHeight)
        {
            ClearCanvas();
            return;
        }
        else
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                Destroy(tiles[i].gameObject);
            }

            InitCanvas(resizeWidth, resizeHeight);
        }
    }

    void ClearCanvas()
    {
        for (int y = 0; y < canvasHeight; y++)
        {
            for (int x = 0; x < canvasWidth; x++)
            {
                tiles[y * width + x].GetComponent<TileInfo>().tileCode = 0;
            }
        }
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
        ResetCanvas(level.width, level.height);

        width = canvasWidth;
        height = canvasHeight;

        for (int y = 0; y < level.height; y++)
        {
            for (int x = 0; x < level.width; x++)
            {
                ChangeTile(tiles[y * level.width + x].transform, level.tiles[y * level.width + x]);
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
