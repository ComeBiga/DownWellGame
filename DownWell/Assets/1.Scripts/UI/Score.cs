using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    #region Singleton
    public static Score instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    LevelObject g_Info;
    public Text scoreTxt;
    int curScore = 0;

    public void getScore(GameObject gameObject)
    {
        if(gameObject.CompareTag("Enemy"))
            g_Info = gameObject.GetComponent<Enemy>().info;
        curScore += g_Info.score;
        scoreTxt.text = curScore.ToString();
    }
}
