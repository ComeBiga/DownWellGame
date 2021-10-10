using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject charPanel;

    void Start()
    {
        startPanel.SetActive(true);
        charPanel.SetActive(false);
    }

    public void startBtn()
    {
        startPanel.SetActive(false);
        charPanel.SetActive(true);
    }
}
