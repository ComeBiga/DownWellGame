using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int tileCode = 0;
}

namespace LevelEditor
{
    public enum TileStyle { Empty, Wall, Block, Platform, Enemy_Slime, Enemy_Bat, Enemy_Turtle }
}
