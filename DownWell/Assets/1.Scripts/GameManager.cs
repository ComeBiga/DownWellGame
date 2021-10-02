using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public string title = "underground";
    public string version = "0.1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
