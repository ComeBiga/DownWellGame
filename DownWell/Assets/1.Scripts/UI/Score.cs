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

    public Text scoreTxt;
    int score = 0;

    public void getScore(string typeName)
    {
        switch(typeName)
        {
            /*case "Enemy(Clone)":
                score += 10;
                scoreTxt.text = score.ToString();
                break;
            case "Octopus(Clone)":
                score += 20;
                scoreTxt.text = score.ToString();
                break;
            case "Wallbug(Clone)":
                score += 30;
                scoreTxt.text = score.ToString();
                break; 
            case "EnemyFaster(Clone)":
                score += 40;
                scoreTxt.text = score.ToString();
                break;*/
            default:
                score += 20;
                scoreTxt.text = score.ToString();
                break;
        }
    }

}
