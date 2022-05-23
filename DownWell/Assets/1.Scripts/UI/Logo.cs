using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    public GameObject Opening;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Opening") != 0)
            Opening.SetActive(false);
        else
            Opening.SetActive(true);

        PlayerPrefs.SetInt("Opening", 1);

        GameObject gameSceneUI = GameObject.Find("inGameUI");
        
        Destroy(gameSceneUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Opening.activeInHierarchy)
        {
            if (!Opening.GetComponent<Image>().enabled)
                Opening.SetActive(false);
        }
    }
}