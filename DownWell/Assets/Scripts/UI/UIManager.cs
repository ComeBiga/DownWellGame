using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject startPanel;


    void Awake()
    {
        startPanel = GameObject.Find("startPanel");
        
        startPanel.SetActive(true);
    }

    public void startBtn()
    {
        startPanel.SetActive(false);
    }

    public void exitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
/*#else
        Application.Quit();*/
#endif
    }

}
