using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Brush", menuName = "Brush")]
public class Brush : ScriptableObject
{
    public int code;
    public string brushName;
    public Sprite sprite;

    public void Paint(TileInfo tileInfo)
    {
        tileInfo.GetComponent<SpriteRenderer>().sprite = sprite;
        tileInfo.tileCode = code;
    }
}
