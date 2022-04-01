using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public LevelObject info;

    public void SetSpriteByStage(int stageNum)
    {
        GetComponent<SpriteRenderer>().sprite = info.GetSprite(stageNum);
    }
}
