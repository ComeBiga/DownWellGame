using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDown
{
    [CreateAssetMenu(fileName = "new StageDB", menuName = "Database/StageDB")]
    public class StageDatabase : ScriptableObject
    {
        public string path = "Assets/99.CatDownMaker/Database/StageList.asset";

        private static string stageDBPath = "Assets/99.CatDownMaker/Database/StageList.asset";

        // 이거처럼 StageDatabase를 StageInfo로 바꿔야함
        public List<StageInfo> stages;

        public void AddStage()
        {
            var newStage = new CatDown.StageInfo();
            UnityEditor.AssetDatabase.CreateAsset(newStage, CatDown.Maker.CatDownMaker.StageDatabasePath + "/new Stage" + stages.Count.ToString() + ".asset");

            newStage.AssetPath = UnityEditor.AssetDatabase.GetAssetPath(newStage);
            stages.Add(newStage);
            UnityEditor.EditorUtility.SetDirty(this);
        }

        public void DeleteStage(CatDown.StageInfo deleteStage)
        {
            if (stages.IndexOf(deleteStage) == 0) return;

            stages.Remove(deleteStage);
            UnityEditor.EditorUtility.SetDirty(this);

            UnityEditor.AssetDatabase.DeleteAsset(deleteStage.GetAssetPath());
        }

        public static StageDatabase GetStageDB()
        {
            //var path = UnityEditor.AssetDatabase.GetAssetPath()

            return UnityEditor.AssetDatabase.LoadAssetAtPath<StageDatabase>(stageDBPath);
        }
    }

}
