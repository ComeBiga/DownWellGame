using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    [CreateAssetMenu(fileName = "new StageDB", menuName = "Database/StageDB")]
    public class StageDatabase : ScriptableObject
    {
        public string path = "Assets/99.CatDownMaker/Database/StageList.asset";

        private static string stagelistPath = "Assets/99.CatDownMaker/Database/StageList.asset";

        // 이거처럼 StageDatabase를 StageInfo로 바꿔야함
        public List<StageInfo> stages;

        public void AddStage()
        {
            var newStage = new CatDown.StageInfo();
            UnityEditor.AssetDatabase.CreateAsset(newStage, path + "/new Stage" + stages.Count.ToString() + ".asset");

            newStage.AssetPath = UnityEditor.AssetDatabase.GetAssetPath(newStage);
            stages.Add(newStage);
        }

        public void DeleteStage(CatDown.StageInfo deleteStage)
        {
            if (stages.IndexOf(deleteStage) == 0) return;

            stages.Remove(deleteStage);

            UnityEditor.AssetDatabase.DeleteAsset(UnityEditor.AssetDatabase.GetAssetPath(deleteStage));
        }

        public static StageDatabase GetStageList()
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<StageDatabase>(stagelistPath);
        }
    }

}
