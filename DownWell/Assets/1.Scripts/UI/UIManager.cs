using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject charPanel;


    void Awake()
    {
        startPanel = GameObject.Find("startPanel");
        charPanel = GameObject.Find("charPanel");
        
        startPanel.SetActive(true);
        charPanel.SetActive(false);
    }

    public void startBtn()
    {
        startPanel.SetActive(false);
        charPanel.SetActive(true);
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
