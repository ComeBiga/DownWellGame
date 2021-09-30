using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int[] tiles;

    public Level() { }

    public void Print()
    {
        foreach(var tile in tiles)
        {
            Debug.Log(" " + tile);
        }
    }
}
