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

    // [Deprecated]
    //public LevelEditor.Stage loadStage = LevelEditor.Stage.Stage1;

    public Dictionary<int, List<Level>> levels = new Dictionary<int, List<Level>>();
    public Dictionary<string, List<Level>> objects = new Dictionary<string, List<Level>>();
    //public List<int[]> levels = new List<int[]>();

    JsonIO jsonIO = new JsonIO();

    // Start is called before the first frame update
    void Start()
    {
        LoadAllLevel();
        LoadBlockObjects();
        LoadStageGround();
        LoadStageStart();

        Debug.Log("Loadlevel Start");
    }

    #region Deprecated
    //public List<Level> GetLevels(LevelEditor.Stage stage)
    //{
    //    return levels[(int)stage];
    //}
    #endregion

    public List<Level> GetLevels(int stageIndex)
    {
        return levels[stageIndex];
    }

    public List<Level> GetObjects(string objName)
    {
        return objects[objName];
    }

    List<Level> LoadAsDirectory(string path)
    {
        string[] directories = Directory.GetFiles(Application.dataPath + path + "/", "*.json");
        List<Level> lvList = new List<Level>();

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var lvs = JsonToLevel<Level>(jsonStr);

            lvList.Add(lvs);
        }

        return lvList;
    }

    List<Level> LoadAsResource(string path)
    {
        var textDatas = Resources.LoadAll(path + "/", typeof(TextAsset));
        List<Level> lvList = new List<Level>();

        foreach (var textData in textDatas)
        {
            Debug.Log(textData.ToString());
            var lvs = JsonToLevel<Level>(textData.ToString());

            lvList.Add(lvs);
        }

        return lvList;
    }

    List<Level> LoadStage(int index, bool isEditor = false)
    {
        var stages = StageManager.instance.stages;

        if(isEditor)
        {
            var path = stages[index].GetPath();

            return LoadAsDirectory(path);
        }
        else
        {
            var path = stages[index].GetPath(true);

            return LoadAsResource(path);
        }
    }

    public void LoadAllLevel()
    {
//#if UNITY_STANDALONE_WIN
//        for (int i = 0; i < 5; i++)
//        {
//            var textDatas = Resources.LoadAll("Levels/Stage" + (i + 1).ToString() + "/", typeof(TextAsset));
//            List<Level> lvList = new List<Level>();

//            foreach (var textData in textDatas)
//            {
//                Debug.Log(textData.ToString());
//                var lvs = JsonToLevel<Level>(textData.ToString());

//                lvList.Add(lvs);

//                //Debug.Log(JsonUtility.ToJson(lvs));
//            }

//            levels.Add(i, lvList);
//        }
//#endif

#if UNITY_EDITOR
        for (int i = 0; i < StageManager.instance.stages.Count; i++)
        {
            var lvList = LoadStage(i, true);

            levels.Add(i, lvList);
        }
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
        for (int i = 0; i < StageManager.instance.stages.Count; i++)
        {
            var lvList = LoadStage(i, false);

            levels.Add(i, lvList);
        }
#endif
    }

    public void LoadBlockObjects()
    {
        List<Level> objList = new List<Level>();

        string[] directories;

//#if UNITY_STANDALONE_WIN
//        var textDatas = Resources.LoadAll("Levels/Blocks", typeof(TextAsset));

//        foreach (var textData in textDatas)
//        {
//            Debug.Log(textData.ToString());
//            var obj = JsonToLevel<Level>(textData.ToString());

//            objList.Add(obj);
//        }

//        objects.Add("Block", objList);
//#endif

#if UNITY_EDITOR
        directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Blocks/", "*.json");

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var obj = JsonToLevel<Level>(jsonStr);

            objList.Add(obj);

            //Debug.Log(JsonUtility.ToJson(obj));
        }

        objects.Add("Block", objList);
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
        var textDatas = Resources.LoadAll("Levels/Blocks", typeof(TextAsset));

        foreach (var textData in textDatas)
        {
            Debug.Log(textData.ToString());
            var obj = JsonToLevel<Level>(textData.ToString());

            objList.Add(obj);
        }

        objects.Add("Block", objList);
#endif
    }

    public void LoadStageStart()
    {
        List<Level> objList = new List<Level>();

        string[] directories;

//#if UNITY_STANDALONE_WIN
//        var textDatas = Resources.LoadAll("Levels/StageStart", typeof(TextAsset));

//        foreach (var textData in textDatas)
//        {
//            Debug.Log(textData.ToString());
//            var obj = JsonToLevel<Level>(textData.ToString());

//            objList.Add(obj);
//        }

//        objects.Add("StageStart", objList);
//#endif

#if UNITY_EDITOR
        directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/StageStart/", "*.json");

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var obj = JsonToLevel<Level>(jsonStr);

            objList.Add(obj);

            //Debug.Log(JsonUtility.ToJson(obj));
        }

        objects.Add("StageStart", objList);
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
        var textDatas = Resources.LoadAll("Levels/StageStart", typeof(TextAsset));

        foreach (var textData in textDatas)
        {
            Debug.Log(textData.ToString());
            var obj = JsonToLevel<Level>(textData.ToString());

            objList.Add(obj);
        }

        objects.Add("StageStart", objList);
#endif
    }

    public void LoadStageGround()
    {
        List<Level> objList = new List<Level>();

        string[] directories;

//#if UNITY_STANDALONE_WIN
//        var textDatas = Resources.LoadAll("Levels/StageGround", typeof(TextAsset));

//        foreach (var textData in textDatas)
//        {
//            Debug.Log(textData.ToString());
//            var obj = JsonToLevel<Level>(textData.ToString());

//            objList.Add(obj);
//        }

//        objects.Add("StageGround", objList);
//#endif

#if UNITY_EDITOR
        directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/StageGround/", "*.json");

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var obj = JsonToLevel<Level>(jsonStr);

            objList.Add(obj);

            //Debug.Log(JsonUtility.ToJson(obj));
        }

        objects.Add("StageGround", objList);
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
        var textDatas = Resources.LoadAll("Levels/StageGround", typeof(TextAsset));

        foreach (var textData in textDatas)
        {
            Debug.Log(textData.ToString());
            var obj = JsonToLevel<Level>(textData.ToString());

            objList.Add(obj);
        }

        objects.Add("StageGround", objList);
#endif
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    //public Level RandomLevel(LevelEditor.Stage stage)
    //{
    //    string seed = (Time.time + Random.value).ToString();
    //    System.Random rand = new System.Random(seed.GetHashCode());

    //    List<Level> levels = GetLevels(stage);
    //    //Debug.Log(levels.Count);
    //    Level randomWall = levels[rand.Next(0, levels.Count)];

    //    return randomWall;
    //}
}
