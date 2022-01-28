using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDB", menuName = "Database/StageList")]
public class StageList : ScriptableObject
{
    public string path = "Assets/99.CatDownMaker/Database/StageList.asset";

    private static string stagelistPath = "Assets/99.CatDownMaker/Database/StageList.asset";

    public List<StageDatabase> stages;

    public void AddStage()
    {
        var newStage = new StageDatabase();
        UnityEditor.AssetDatabase.CreateAsset(newStage, path + "/new Stage"+ stages.Count.ToString() +".asset");

        newStage.AssetPath = UnityEditor.AssetDatabase.GetAssetPath(newStage);
        stages.Add(newStage);
    }

    public void DeleteStage(StageDatabase deleteStage)
    {
        if (stages.IndexOf(deleteStage) == 0) return;

        stages.Remove(deleteStage);

        UnityEditor.AssetDatabase.DeleteAsset(UnityEditor.AssetDatabase.GetAssetPath(deleteStage));
    }

    public static StageList GetStageList()
    {
        return UnityEditor.AssetDatabase.LoadAssetAtPath<StageList>(stagelistPath);
    }
}
