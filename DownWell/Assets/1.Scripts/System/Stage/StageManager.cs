using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    #region Singleton
    public static StageManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public List<StageDatabase> stages;

    private StageDatabase current;
    public StageDatabase Current
    {
        get
        {
            return current;
        }
    }
    private int index = 0;

    public void Start()
    {
        current = stages[index];
    }

    public void NextStage()
    {
        index++;
        current = stages[index];
    }
}
