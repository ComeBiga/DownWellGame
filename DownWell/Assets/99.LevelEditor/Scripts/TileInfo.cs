using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int tileCode = 0;
    public LevelObject current;

    public void UpdateTileInfo()
    {
        if (tileCode > 10 && tileCode < 20)
            current = BrushManager.instance.enemyBrushes.Find(c => c.code == tileCode);
    }
}

namespace LevelEditor
{
    public enum TileStyle { Empty, Wall, Block, Platform, Enemy_Slime, Enemy_Bat, Enemy_Turtle }
}
