using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name;
    public int width;
    public int height;

    public int[] tiles;

    public Level() { }

    public void AssignCorner()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var index = y * width + x;
                
                if (tiles[index] == 1 || (tiles[index] > 100 && tiles[index] < 1000))
                {
                    var result = TileCorner(this, x, y);

                    if (result != -1) tiles[index] = result + 100;
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

    public void Print()
    {
        foreach(var tile in tiles)
        {
            Debug.Log(" " + tile);
        }
    }
}
