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
        SetCanvasActive(false);

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

    public void SetCanvasActive(bool value)
    {
        levelTiles.SetActive(value);
    }

    public void ResetCanvas()
    {
        ResetCanvas(width, height);
    }

    public void ResetCanvas(int resizeWidth, int resizeHeight)
    {
        if (resizeWidth == canvasWidth && resizeHeight == canvasHeight)
        {
            ClearCanvas();

            InitLevel();
            return;
        }
        else
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                Destroy(tiles[i].gameObject);
            }

            InitCanvas(resizeWidth, resizeHeight);

            InitLevel();
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

    void InitLevel()
    {
        for(int i = 0; i < canvasHeight; i++)
        {
            tiles[i * canvasWidth].GetComponent<TileInfo>().tileCode = 1;
            ChangeTile(tiles[i * canvasWidth].transform, 1);

            tiles[i * canvasWidth + (canvasWidth - 1)].GetComponent<TileInfo>().tileCode = 1;
            ChangeTile(tiles[i * canvasWidth + (canvasWidth - 1)].transform, 1);
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
                if (level.tiles[y * level.width + x] >= 100 && level.tiles[y * level.width + x] < 1000) level.tiles[y * level.width + x] = 1;
                ChangeTile(tiles[y * level.width + x].transform, level.tiles[y * level.width + x]);
            }
        }
    }

    void ChangeTile(Transform tile, int tileCode)
    {
        // sprite set
        if (tileCode > 2000)
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.brushDB.enemyBrushes.Find(b => b.code == tileCode).sprite;
        else if (tileCode >= 100)
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.brushDB.wallBrushes.Find(b => b.code == 1).sprite;
        else if (tileCode > 0)
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.brushDB.wallBrushes.Find(b => b.code == tileCode).sprite;
        else
            tile.GetComponent<SpriteRenderer>().sprite = BrushManager.instance.eraserBrush.sprite;

        // tilecode set
        tile.GetComponent<TileInfo>().tileCode = tileCode;
    }

    public void DesignateTileCorner(Level level)
    {
        for(int y = 0; y < level.height; y++)
        {
            for(int x = 0; x < level.width; x++)
            {
                if(level.tiles[y * level.width + x] == 1)
                {
                    var result = TileCorner(level, x, y);

                    if (result != -1) level.tiles[y * level.width + x] = result + 100;
                }
            }
        }
    }

    int TileCorner(Level level, int x, int y)
    {
        int result = -1;
        bool top = false;
        bool right = false;
        bool left = false;
        bool bottom = false;

        // left wall edge
        if (x == 0) left = true;
        // right wall edge
        if (x == level.width - 1) right = true;
        // top edge
        if (y == 0) top = true;
        // bottom edge
        if (y == level.height - 1) bottom = true;

        // 상하좌우를 체크해서 타일이 있으면 해당 위치를 true
        // top check
        if (y - 1 >= 0 && (level.tiles[(y - 1) * level.width + x] == 1 || 
            (level.tiles[(y - 1) * level.width + x] > 100 && level.tiles[(y - 1) * level.width + x] < 1000))) top = true;
        // right check
        if (x + 1 < level.width && (level.tiles[y * level.width + (x + 1)] == 1 ||
            (level.tiles[y * level.width + (x + 1)] > 100 && level.tiles[y * level.width + (x + 1)] < 1000))) right = true;
        // left check
        if (x - 1 >= 0 && (level.tiles[y * level.width + (x - 1)] == 1 ||
            (level.tiles[y * level.width + (x - 1)] > 100 && level.tiles[y * level.width + (x - 1)] < 1000))) left = true;
        // bottom check
        if (y + 1 < level.height && (level.tiles[(y + 1) * level.width + x] == 1 ||
            (level.tiles[(y + 1) * level.width + x] > 100 && level.tiles[(y + 1) * level.width + x] < 1000))) bottom = true;

        // 체크한 타일에 맞게 결과 코드를 결정
        // top-left
        if (top == false && right == true && bottom == true && left == false) result = 1;
        // top
        if (top == false && right == true && bottom == true && left == true) result = 2;
        // top-right
        if (top == false && right == false && bottom == true && left == true) result = 3;
        // left
        if (top == true && right == true && bottom == true && left == false) result = 4;
        // middle
        if (top == true && right == true && bottom == true && left == true) result = 5;
        // right
        if (top == true && right == false && bottom == true && left == true) result = 6;
        // bottom-left
        if (top == true && right == true && bottom == false && left == false) result = 7;
        // bottom
        if (top == true && right == true && bottom == false && left == true) result = 8;
        // bottom-right
        if (top == true && right == false && bottom == false && left == true) result = 9;
        // top-top
        if (top == false && right == false && bottom == true && left == false) result = 10;
        // right-right
        if (top == false && right == false && bottom == false && left == true) result = 11;
        // bottom-bottom
        if (top == true && right == false && bottom == false && left == false) result = 12;
        // left-left
        if (top == false && right == true && bottom == false && left == false) result = 13;
        // alone
        if (top == false && right == false && bottom == false && left == false) result = 0;
        // horizontal-between
        if (top == false && right == true && bottom == false && left == true) result = 14;
        // vertical-between
        if (top == true && right == false && bottom == true && left == false) result = 15;

        return result;
    }
}
