using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    #region Singleton
    public static LoadLevel instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public Dictionary<int, List<int[]>> levels = new Dictionary<int, List<int[]>>();
    //public List<int[]> levels = new List<int[]>();

    JsonIO jsonIO = new JsonIO();

    // Start is called before the first frame update
    void Start()
    {
        LoadAllLevel();
    }

    public List<int[]> GetLevels(Stage stage)
    {
        return levels[(int)stage];
    }

    public void LoadAllLevel()
    {
        for (int i = 0; i < 5; i++)
        {
            string[] directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Stage" + (i+1).ToString() + "/", "*.json");
            List<int[]> lvList = new List<int[]>();

            foreach (var dir in directories)
            {
                string jsonStr = File.ReadAllText(dir);
                var lvs = JsonToLevel<Level>(jsonStr);

                lvList.Add(lvs.tiles);

                Debug.Log(JsonUtility.ToJson(lvs));
            }

            levels.Add(i, lvList);
        }
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}
