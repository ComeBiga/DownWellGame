using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour
{
    private void Update()
    {
        if (GetComponent<Canvas>().worldCamera == null)
            GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }
    public void homeBtn()
    {
        SceneManager.LoadScene(0);
    }
}
