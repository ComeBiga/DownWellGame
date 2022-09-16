using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDisplay : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase tile;
    public Camera cam;

    private int pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Display();
    }

    void Display()
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 200; j++)
            {
                tileMap.SetTile(new Vector3Int(i + pos, j, 0), tile);
            }
        }

        pos += 11;
    }

    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 150, 100), "Create"))
        {
            Display();
        }
    }
}
