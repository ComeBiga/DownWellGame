using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charSetting : MonoBehaviour
{
    public GameObject player;

    void Awake()
    {
        if (SelectMan.charNum == 0)
            player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        else if (SelectMan.charNum == 1)  //blue
            player.GetComponent<SpriteRenderer>().color = new Color32(0, 171, 255, 255);
        else  //black
            player.GetComponent<SpriteRenderer>().color = new Color32(72, 72, 72, 255);

    }
}
