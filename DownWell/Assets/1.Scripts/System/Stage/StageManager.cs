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

    public enum Stage { Stage_1, Stage_2, Stage_3, Stage_4, Stage_5, TestStage }
    //public Stage stage = Stage.Stage_1;

    [Header("DB")]
    public List<StageDatabase> stages;

    private StageDatabase current;
    public StageDatabase Current
    {
        get
        {
            if(current == null)
            {
                //SetCurrentStage();
                index = 0;
                current = stages[index];
            }
            return current;
        }
    }
    private int index = -1;
    public int CurrentStageIndex 
    { 
        get 
        {
            if (index == -1)
                current = stages[0];
                //SetCurrentStage();

            return index;
        }
    }

    //private void SetCurrentStage()
    //{
    //    switch(stage)
    //    {
    //        case Stage.Stage_1:
    //            SetCurrentStage(1);
    //            break;
    //        case Stage.Stage_2:
    //            SetCurrentStage(2);
    //            break;
    //        case Stage.Stage_3:
    //            SetCurrentStage(3);
    //            break;
    //        case Stage.Stage_4:
    //            SetCurrentStage(4);
    //            break;
    //        case Stage.Stage_5:
    //            SetCurrentStage(5);
    //            break;
    //        case Stage.TestStage:
    //            SetCurrentStage(0);
    //            break;
    //    }
    //}

    public void SetCurrentStage(string stageName)
    {
        var newStage = stages.Find(s => s.Name == stageName);

        if (newStage == null)
            Debug.LogWarning($"Can't Find stage from name : {stageName}");
        else
        {
            index = stages.IndexOf(newStage);
            current = newStage;
        }
    }

    public void SetCurrentStage(int stageNum)
    {
        var newStage = stages.Find(s => s.Num == stageNum);

        if (newStage == null)
            Debug.LogWarning($"Can't Find stage from number : {stageNum}");
        else
        {
            index = stages.IndexOf(newStage);
            current = newStage;
        }
    }

    public void NextStage()
    {
        index++;

        if (index >= stages.Count)
            Debug.LogWarning("No more Stages!");

        current = stages[index];
        //MapManager.instance.mapDisplay.SetObjects(current);
    }
}
