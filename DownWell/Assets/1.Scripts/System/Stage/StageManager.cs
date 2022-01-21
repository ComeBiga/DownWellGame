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
    public int CurrentStageIndex { get { return index; } }

    public void Start()
    {
        current = stages[index];
    }

    public void SetCurrentStage(string stageName)
    {
        var newStage = stages.Find(s => s.Name == stageName);

        if (newStage == null)
            Debug.LogWarning($"Can't Find stage from name : {stageName}");
        else
            current = newStage;
    }

    public void SetCurrentStage(int stageNum)
    {
        var newStage = stages.Find(s => s.Num == stageNum);

        if (newStage == null)
            Debug.LogWarning($"Can't Find stage from number : {stageNum}");
        else
            current = newStage;
    }

    public void NextStage()
    {
        index++;
        current = stages[index];
    }
}
