using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    LevelObject g_Info;
    ItemInfo i_info;
    public Text scoreTxt;
    int curScore = 0;

    public void getScore(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                g_Info = gameObject.GetComponent<Enemy>().info;
                curScore += g_Info.score;
                break;
            case "Item":
                i_info = gameObject.GetComponent<Item>().i_Info;
                curScore += i_info.score;
                break;
        }
        scoreTxt.text = curScore.ToString();
    }

    public void Add(int amount)
    {
        curScore += amount;

        scoreTxt.text = curScore.ToString();
    }
}
