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
}

namespace LevelEditor
{
    public enum TileStyle { Empty, Wall, Block, Platform, Enemy_Slime, Enemy_Bat, Enemy_Turtle }
}
