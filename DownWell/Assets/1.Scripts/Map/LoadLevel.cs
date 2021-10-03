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

    public Dictionary<int, List<Level>> levels = new Dictionary<int, List<Level>>();
    public Dictionary<string, List<Level>> objects = new Dictionary<string, List<Level>>();
    //public List<int[]> levels = new List<int[]>();

    JsonIO jsonIO = new JsonIO();

    // Start is called before the first frame update
    void Start()
    {
        LoadAllLevel();
        LoadCloudObjects();
    }

    public List<Level> GetLevels(Stage stage)
    {
        return levels[(int)stage];
    }

    public List<Level> GetObjects(string objName)
    {
        return objects[objName];
    }

    public void LoadAllLevel()
    {
        for (int i = 0; i < 5; i++)
        {
            string[] directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Stage" + (i+1).ToString() + "/", "*.json");
            List<Level> lvList = new List<Level>();

            foreach (var dir in directories)
            {
                string jsonStr = File.ReadAllText(dir);
                var lvs = JsonToLevel<Level>(jsonStr);

                lvList.Add(lvs);

                //Debug.Log(JsonUtility.ToJson(lvs));
            }

            levels.Add(i, lvList);
        }
    }

    public void LoadCloudObjects()
    {
        string[] directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Clouds/", "*.json");
        List<Level> objList = new List<Level>();

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var obj = JsonToLevel<Level>(jsonStr);

            objList.Add(obj);

            //Debug.Log(JsonUtility.ToJson(obj));
        }

        objects.Add("Cloud", objList);
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}
