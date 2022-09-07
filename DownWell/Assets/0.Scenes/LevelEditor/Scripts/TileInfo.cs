using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int tileCode = 0;
    public Sprite sprite;

    public void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void SetInfo(LevelObject currentBrush)
    {
        tileCode = currentBrush.code;
        GetComponent<SpriteRenderer>().sprite = currentBrush.sprite;
    }

    public void Set(int tileCode)
    {
        this.tileCode = tileCode;
    }

    public void Set(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public void Set(int tileCode, Sprite sprite)
    {
        Set(tileCode);
        Set(sprite);
    }

    public void SetTile(int tileCode)
    {
        // sprite set
        if (tileCode > 2000)
            GetComponent<SpriteRenderer>().sprite = BrushManager.instance.EnemyPallet.Find(b => b.code == tileCode).sprite;
        else if (tileCode >= 100)
            GetComponent<SpriteRenderer>().sprite = BrushManager.instance.WallPallet.Find(b => b.code == 1).sprite;
        else if (tileCode > 0)
            GetComponent<SpriteRenderer>().sprite = BrushManager.instance.WallPallet.Find(b => b.code == tileCode).sprite;
        else
            GetComponent<SpriteRenderer>().sprite = BrushManager.instance.eraserBrush.sprite;

        Set(tileCode);
    }

    public void Delete()
    {
        //Debug.Log(this.gameObject.name);
        //Destroy(this.gameObject);
        DestroyImmediate(this.gameObject);
    }
}

namespace LevelEditor
{
    public enum TileStyle { Empty, Wall, Block, Platform, Enemy_Slime, Enemy_Bat, Enemy_Turtle }
}
