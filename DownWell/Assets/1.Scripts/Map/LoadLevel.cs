using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
#if UNITY_EDITOR
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
#endif
*/

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

    //[SerializeField] private string blockPath = "/Resources/Levels/Blocks/";
    //[SerializeField] private string stageStartPath = "/Resources/Levels/StageStart/";
    //[SerializeField] private string stageGroundPath = "/Resources/Levels/StageGround/";

    [Header("Path")]
    [SerializeField] private string levelPath = "Levels/";
    [SerializeField] private string entrePath = "Entre/";
    [SerializeField] private string mainPath = "Main/";
    [SerializeField] private string exitPath = "Exit/"; 
    [SerializeField] private string bossPath = "Boss/Entre";
    [SerializeField] private string bossLevelPath = "Boss/Main";

    public enum LevelType { BASE, ENTRE, MAIN, EXIT, BOSS, BOSS_LEVEL }

    //public Dictionary<int, List<Level>> levels = new Dictionary<int, List<Level>>();
    //public Dictionary<string, List<Level>> objects = new Dictionary<string, List<Level>>();

    [Header("Loader")]
    public bool useLoaderAsEditor = false;
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        //LoadAllLevel();
        //LoadAllObjects("Block", blockPath);
        //LoadAllObjects("StageStart", stageStartPath);
        //LoadAllObjects("StageGround", stageGroundPath);

        if(!useLoaderAsEditor) levelLoader = new LevelLoaderAsBuild();
        else  levelLoader = new LevelLoaderAsEditor(); 
    }

    public string GetPath(LevelType type, int stageNum)
    {
        string path = levelPath + "Stage" + stageNum.ToString() + "/";

        switch(type)
        {
            case LevelType.BASE:
                break;
            case LevelType.ENTRE:
                path += entrePath;
                break;
            case LevelType.MAIN:
                path += mainPath;
                break;
            case LevelType.EXIT:
                path += exitPath;
                break;
            case LevelType.BOSS:
                path += bossPath;
                break;
            case LevelType.BOSS_LEVEL:
                path += bossLevelPath;
                break;
        }

        return path;
    }

    #region Deprecated(GetLevels)

    //public List<Level> GetLevels(int stageIndex)
    //{
    //    return levels[stageIndex];
    //}

    //public List<Level> GetObjects(string objName)
    //{
    //    return objects[objName];
    //}

//    public static string ReplacePath(string path)
//    {

//#if UNITY_EDITOR
//        return path;
//#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
//        return path.Replace("/Resources", "");
//#endif

//    }

    #endregion

    public static string ConvertPathResourcesToDirectory(string path)
    {
        return path.Replace("/Resources/", Application.dataPath + "Resources/");
    }

    public Level GetLevel(string path)
    {
        var level = levelLoader.Load(path);

        return level;
    }
    
    public Level GetLevel(LevelType type, int stageNum, string levelName)
    {
        string path = GetPath(type, stageNum) + levelName;

        if (useLoaderAsEditor) path = ConvertPathResourcesToDirectory(path);

        var level = levelLoader.Load(path);

        return level;
    }

    public List<Level> GetLevels(string path)
    {
        if (useLoaderAsEditor) path = ConvertPathResourcesToDirectory(path);

        var levels = levelLoader.LoadAll(path);

        return levels;
    }

    public List<Level> GetLevels(LevelType type, int stageNum)
    {
        string path = GetPath(type, stageNum);

        if (useLoaderAsEditor) path = ConvertPathResourcesToDirectory(path);

        var levels = levelLoader.LoadAll(path);

        return levels;
    }

    #region Deprecated(LoadAndGetLevel)
    //    public Level LoadAndGetLevel(string name)
    //    {
    //#if UNITY_EDITOR
    //        var path = "/Resources/Levels/" + name + "/";

    //        var levels = Load(LoadLevel.ReplacePath(path));

    //        return levels[0];
    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //        var textDatas = Resources.LoadAll("Levels/" + name + "/", typeof(TextAsset));
    //        List<Level> lvList = new List<Level>();

    //        foreach (var textData in textDatas)
    //        {
    //            Debug.Log(textData.ToString());
    //            var lvs = JsonToLevel<Level>(textData.ToString());

    //            lvList.Add(lvs);
    //        }

    //        return lvList[0];
    //#endif
    //    }


    //    public List<Level> LoadAndGetLevels(string path)
    //    {
    //#if UNITY_EDITOR
    //        //var levelPath = "/Resources/Levels/" + name + "/";

    //        var levels = Load(LoadLevel.ReplacePath(path));

    //        return levels;
    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //        var levelPath = path.Replace("/Resources/", "");
    //        Debug.Log(levelPath);
    //        var textDatas = Resources.LoadAll(levelPath, typeof(TextAsset));
    //        List<Level> lvList = new List<Level>();

    //        foreach (var textData in textDatas)
    //        {
    //            Debug.Log(textData.ToString());
    //            var lvs = JsonToLevel<Level>(textData.ToString());

    //            lvList.Add(lvs);
    //        }

    //        Debug.Log(lvList.Count);

    //        return lvList;
    //#endif
    //    }

    #endregion

    #region Deprecated(Load)
    // Load Json Files into Level
    //    List<Level> Load(string path)
    //    {
    //#if UNITY_EDITOR
    //        string[] directories = Directory.GetFiles(Application.dataPath + path + "/", "*.json");
    //        List<Level> lvList = new List<Level>();

    //        foreach (var dir in directories)
    //        {
    //            string jsonStr = File.ReadAllText(dir);
    //            var lvs = JsonToLevel<Level>(jsonStr);

    //            lvList.Add(lvs);
    //        }

    //        return lvList;
    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //        var textDatas = Resources.LoadAll(path + "/", typeof(TextAsset));
    //        List<Level> lvList = new List<Level>();

    //        foreach (var textData in textDatas)
    //        {
    //            Debug.Log(textData.ToString());
    //            var lvs = JsonToLevel<Level>(textData.ToString());

    //            lvList.Add(lvs);
    //        }

    //        return lvList;
    //#endif
    //    }
    #endregion

    #region Deprecated
    //List<Level> LoadAsDirectory(string path)
    //{
    //    string[] directories = Directory.GetFiles(Application.dataPath + path + "/", "*.json");
    //    List<Level> lvList = new List<Level>();

    //    foreach (var dir in directories)
    //    {
    //        string jsonStr = File.ReadAllText(dir);
    //        var lvs = JsonToLevel<Level>(jsonStr);

    //        lvList.Add(lvs);
    //    }

    //    return lvList;
    //}

    //List<Level> LoadAsResource(string path)
    //{
    //    var textDatas = Resources.LoadAll(path + "/", typeof(TextAsset));
    //    List<Level> lvList = new List<Level>();

    //    foreach (var textData in textDatas)
    //    {
    //        Debug.Log(textData.ToString());
    //        var lvs = JsonToLevel<Level>(textData.ToString());

    //        lvList.Add(lvs);
    //    }

    //    return lvList;
    //}

    //List<Level> Load(string path, bool isEditor = false)
    //{
    //    if (isEditor)
    //    {
    //        return LoadAsDirectory(path);
    //    }
    //    else
    //    {
    //        return LoadAsResource(path);
    //    }
    //}

    //List<Level> LoadStage(int index)
    //{
    //    var stages = StageManager.instance.stages;

    //    return Load(stages[index].GetPath());

    //    //if(isEditor)
    //    //{
    //    //    var path = stages[index].GetPath();

    //    //    return LoadAsDirectory(path);
    //    //}
    //    //else
    //    //{
    //    //    var path = stages[index].GetPath();

    //    //    return LoadAsResource(path);
    //    //}
    //}
    #endregion

    //public void LoadAllLevel()
    //{
    //    if (StageManager.instance == null) return;

    //    var stages = StageManager.instance.stages;

    //    for (int i = 0; i < stages.Count; i++)
    //    {
    //        var lvList = Load(stages[i].GetPath());

    //        levels.Add(i, lvList);
    //    }

    //    //#if UNITY_EDITOR
    //    //        for (int i = 0; i < StageManager.instance.stages.Count; i++)
    //    //        {
    //    //            var lvList = LoadStage(i, true);

    //    //            levels.Add(i, lvList);
    //    //        }
    //    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //    //        for (int i = 0; i < StageManager.instance.stages.Count; i++)
    //    //        {
    //    //            var lvList = LoadStage(i, false);

    //    //            levels.Add(i, lvList);
    //    //        }
    //    //#endif
    //}

    #region Deprecated
    //public void LoadAllObjects(string objectName, string path)
    //{
    //    var objList = Load(LoadLevel.ReplacePath(path));

    //    objects.Add(objectName, objList);
    //}


    //    public void LoadBlockObjects()
    //    {
    //        List<Level> objList = new List<Level>();

    //        string[] directories;

    //#if UNITY_EDITOR
    //        directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Blocks/", "*.json");

    //        foreach (var dir in directories)
    //        {
    //            string jsonStr = File.ReadAllText(dir);
    //            var obj = JsonToLevel<Level>(jsonStr);

    //            objList.Add(obj);

    //            //Debug.Log(JsonUtility.ToJson(obj));
    //        }

    //        objects.Add("Block", objList);
    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //        var textDatas = Resources.LoadAll("Levels/Blocks", typeof(TextAsset));

    //        foreach (var textData in textDatas)
    //        {
    //            Debug.Log(textData.ToString());
    //            var obj = JsonToLevel<Level>(textData.ToString());

    //            objList.Add(obj);
    //        }

    //        objects.Add("Block", objList);
    //#endif
    //    }

    //    public void LoadStageStart()
    //    {
    //        List<Level> objList = new List<Level>();

    //        string[] directories;

    //#if UNITY_EDITOR
    //        directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/StageStart/", "*.json");

    //        foreach (var dir in directories)
    //        {
    //            string jsonStr = File.ReadAllText(dir);
    //            var obj = JsonToLevel<Level>(jsonStr);

    //            objList.Add(obj);

    //            //Debug.Log(JsonUtility.ToJson(obj));
    //        }

    //        objects.Add("StageStart", objList);
    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //        var textDatas = Resources.LoadAll("Levels/StageStart", typeof(TextAsset));

    //        foreach (var textData in textDatas)
    //        {
    //            Debug.Log(textData.ToString());
    //            var obj = JsonToLevel<Level>(textData.ToString());

    //            objList.Add(obj);
    //        }

    //        objects.Add("StageStart", objList);
    //#endif
    //    }

    //    public void LoadStageGround()
    //    {
    //        List<Level> objList = new List<Level>();

    //        string[] directories;

    //#if UNITY_EDITOR
    //        directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/StageGround/", "*.json");

    //        foreach (var dir in directories)
    //        {
    //            string jsonStr = File.ReadAllText(dir);
    //            var obj = JsonToLevel<Level>(jsonStr);

    //            objList.Add(obj);

    //            //Debug.Log(JsonUtility.ToJson(obj));
    //        }

    //        objects.Add("StageGround", objList);
    //#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
    //        var textDatas = Resources.LoadAll("Levels/StageGround", typeof(TextAsset));

    //        foreach (var textData in textDatas)
    //        {
    //            Debug.Log(textData.ToString());
    //            var obj = JsonToLevel<Level>(textData.ToString());

    //            objList.Add(obj);
    //        }

    //        objects.Add("StageGround", objList);
    //#endif
    //    }

    #endregion

    #region Deprecated(JsonToLevel)
    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
    #endregion
}
