using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using UnityEngine;

namespace LevelEditor
{
    public enum Stage { Stage1, Stage2, Stage3, Stage4, Stage5, Blocks, StageGround }
}

public class JsonIO : MonoBehaviour
{
    public LevelDatabase levelDB;
    public LevelDBInfo selectedDB;
    public static bool levelChanged = false;

    public LevelEditor.Stage stage;
    public string fileName = " ";

    // UpdateFile
    public string updateStage = " ";
    public string updatefileName = " ";
    public int fromCode;
    public int toCode;

    private void Start()
    {
        SelectDB(new LevelDBInfo());

        fileName = "";
        //tiles = GameObject.Find("LevelTiles").GetComponentsInChildren<TileInfo>();
        UpdateAllDatabase();
    }

    public void Update()
    {
        if (JsonIO.levelChanged)
        {
            Save(selectedDB);
            JsonIO.levelChanged = false;
        }
    }

    #region Save
    public LevelDBInfo CreateNewLevel(int width, int height)
    {
        LevelEditorManager.instance.ResetCanvas(width, height);
        SaveAsNew();

        return levelDB.jsonLevelDBs.Find(n => n.filename.Equals(fileName));
    }

    // Write into Text File
    private void SaveToTextFile(string path, string fileName)
    {
        var jsonStr = LevelToJson(fileName);

        File.WriteAllText(path, jsonStr);
    }

    // Save to create
    public void SaveAsNew()
    {
        var path = "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json";

        SaveToTextFile(Application.dataPath + path, fileName);

        //var jsonStr = LevelToJson(fileName);

        //File.WriteAllText(Application.dataPath + path, jsonStr);

        SaveIntoDatabase(fileName, stage, path);

        Debug.Log("Saved(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    }

    // Save for auto
    public void Save(LevelDBInfo levelInfo)
    {
        var path = Application.dataPath + levelInfo.path;

        var fileName = new DirectoryInfo(path).Name;

        SaveToTextFile(path, fileName.Replace(".json", ""));

        //var jsonStr = LevelToJson(fileName.Replace(".json", ""));

        //File.WriteAllText(path, jsonStr);


        Debug.Log("Saved(" + levelInfo.filename + ")");
    }

    string LevelToJson(string fileName = "")
    {
        Level level = new Level();
        level.tiles = new int[LevelEditorManager.instance.tiles.Count];

        level.name = fileName;

        for (int i = 0; i < level.tiles.Length; i++)
            level.tiles[i] = (int)LevelEditorManager.instance.tiles[i].GetComponent<TileInfo>().tileCode;

        level.width = (int)LevelEditorManager.instance.getCanvasSize().x;
        level.height = (int)LevelEditorManager.instance.getCanvasSize().y;

        LevelEditorManager.instance.DesignateTileCorner(level);

        return JsonUtility.ToJson(level);
    }

    #endregion

    #region Load

    public T LoadFromTextFile<T>(string path)
    {
        // Read From Text File
        var jsonStr = File.ReadAllText(path);

        // Convert into json
        var level = JsonToLevel<T>(jsonStr);

        return level;
    }

    // Deprecated
    //public void LoadJson()
    //{
    //    var path = Application.dataPath + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json";

    //    var level = LoadFromTextFile<Level>(path);

    //    //string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json");

    //    //var level = JsonToLevel<Level>(jsonStr);

    //    LevelEditorManager.instance.LoadLevel(level);

    //    LevelEditorManager.instance.SetCanvasActive(true);

    //    Debug.Log("Loaded(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    //}

    public void LoadJson(string path)
    {
        var level = LoadFromTextFile<Level>(Application.dataPath + path);
        //string jsonStr = File.ReadAllText(Application.dataPath + path);
        //var level = JsonToLevel<Level>(jsonStr);

        LevelEditorManager.instance.LoadLevel(level);

        JsonIO.levelChanged = false;

        LevelEditorManager.instance.SetCanvasActive(true);

        Debug.Log("Loaded(" + path + ")");
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    #endregion

    #region Delete
    public void DeleteJson(LevelDBInfo levelInfo)
    {
        levelDB.Remove(levelInfo);
        File.Delete(Application.dataPath + levelInfo.path);
        SelectDB(new LevelDBInfo());

        LevelEditorManager.instance.SetCanvasActive(false);
    }
    #endregion

    #region Database

    public void SelectDB(LevelDBInfo levelDB)
    {
        selectedDB = levelDB;
    }

    void SaveIntoDatabase(string filename, LevelEditor.Stage stage, string path)
    {
        levelDB.Add(filename, stage, path);
    }

    void SaveIntoDatabase(LevelDBInfo newDB)
    {
        SaveIntoDatabase(newDB.filename, newDB.stage, newDB.path);
    }

    void RemoveAllDatabase()
    {
        levelDB.jsonLevelDBs = new List<LevelDBInfo>();
    }

    LevelDBInfo LoadDatabase(string path, LevelEditor.Stage stage)
    {
        LevelDBInfo newDB = new LevelDBInfo();
        newDB.path = path.Replace(Application.dataPath, "");

        var lvs = LoadFromTextFile<Level>(path);
        //string jsonStr = File.ReadAllText(dir);
        //var lvs = JsonToLevel<Level>(jsonStr);

        // 왜 있는지 모르겠는 코드라인
        LevelEditorManager.instance.DesignateTileCorner(lvs);

        newDB.filename = lvs.name;
        newDB.stage = stage;

        return newDB;
    }

    List<LevelDBInfo> LoadAllDatabase(string path, LevelEditor.Stage stage)
    {
        List<LevelDBInfo> loadedDBs = new List<LevelDBInfo>();

        string[] directories = Directory.GetFiles(path, "*.json");

        foreach(var dir in directories)
        {
            var db = LoadDatabase(dir, stage);
            loadedDBs.Add(db);
        }

        return loadedDBs;
    }

    void UpdateAllDatabase()
    {
        RemoveAllDatabase();

        string[] directories;
        List<LevelDBInfo> loadedDBs = new List<LevelDBInfo>();

        for (int i = 0; i < 5; i++)
        {
            var path = Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/";

            var dbs = LoadAllDatabase(path, (LevelEditor.Stage)i);

            loadedDBs.AddRange(dbs);
            //directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/", "*.json");

            //foreach (var dir in directories)
            //{
            //    LevelDBInfo newDB = new LevelDBInfo();
            //    newDB.path = dir.Replace(Application.dataPath, "");

            //    var lvs = LoadFromTextFile<Level>(dir);
            //    //string jsonStr = File.ReadAllText(dir);
            //    //var lvs = JsonToLevel<Level>(jsonStr);

            //    // 왜 있는지 모르겠는 코드라인
            //    LevelEditorManager.instance.DesignateTileCorner(lvs);

            //    newDB.filename = lvs.name;
            //    newDB.stage = (LevelEditor.Stage)i;

            //}
        }
        //SaveIntoDatabase(newDB);

        // StageStart
        var pathStageStart = Application.dataPath + "/Resources/Levels/StageStart/";

        loadedDBs.AddRange(LoadAllDatabase(pathStageStart, LevelEditor.Stage.StageGround));
        //directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/StageStart/", "*.json");

        //foreach (var dir in directories)
        //{
        //    LevelDBInfo newDB = new LevelDBInfo();
        //    newDB.path = dir.Replace(Application.dataPath, "");

        //    string jsonStr = File.ReadAllText(dir);
        //    var lvs = JsonToLevel<Level>(jsonStr);

        //    newDB.filename = lvs.name;
        //    newDB.stage = LevelEditor.Stage.StageGround;

        //    SaveIntoDatabase(newDB);
        //}

        // StageGround
        var pathStageGround = Application.dataPath + "/Resources/Levels/StageGround/";

        loadedDBs.AddRange(LoadAllDatabase(pathStageGround, LevelEditor.Stage.StageGround));
        //directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/StageGround/", "*.json");

        //foreach (var dir in directories)
        //{
        //    LevelDBInfo newDB = new LevelDBInfo();
        //    newDB.path = dir.Replace(Application.dataPath, "");

        //    string jsonStr = File.ReadAllText(dir);
        //    var lvs = JsonToLevel<Level>(jsonStr);

        //    newDB.filename = lvs.name;
        //    newDB.stage = LevelEditor.Stage.StageGround;

        //    SaveIntoDatabase(newDB);
        //}

        foreach(var db in loadedDBs)
        {
            SaveIntoDatabase(db);
        }

        // Sorting
        List<string> fns = new List<string>();

        foreach(var fn in levelDB.jsonLevelDBs)
        {
            fns.Add(fn.filename);
        }

        fns.Sort(new StringAsNumericComparer());

        foreach(var fn in fns)
        {
            var lv = levelDB.jsonLevelDBs.Find(l => l.filename == fn);
            levelDB.jsonLevelDBs.Remove(lv);
            levelDB.jsonLevelDBs.Add(lv);
        }
    }

    #endregion

    #region Updating DB

    public void UpdateTileCode()
    {
        if (updateStage == "*")
        {
            UpdateAllFiles();
            return;
        }

        if (updatefileName == "*")
        {
            Debug.Log("*");
            UpdateAllOfStage();
            return;
        }

        string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + updateStage + "/" + updatefileName + ".json");
        var lvs = JsonToLevel<Level>(jsonStr);

        var width = lvs.width;
        var height = lvs.height;

        for (int y = 0; y < lvs.height; y++)
        {
            for (int x = 0; x < lvs.width; x++)
            {
                // 한 레벨에 하고싶은 짓
                if (lvs.tiles[lvs.width * y + x] == fromCode)
                    lvs.tiles[lvs.width * y + x] = toCode;
            }
        }

        jsonStr = JsonUtility.ToJson(lvs);

        File.WriteAllText(Application.dataPath + "/Resources/Levels/" + updateStage + "/" + updatefileName + ".json", jsonStr);

        Debug.Log("TileCode Changed from "+ fromCode.ToString() + "to" + toCode.ToString()
            + "(" + "/Resources/Levels/" + updateStage + "/" + updatefileName + ".json" + ")");
    }

    private void UpdateAllOfStage()
    {
        string[] directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/" + updateStage + "/", "*.json");
        List<Level> lvList = new List<Level>();

        foreach (var dir in directories)
        {
            string jsonStr = File.ReadAllText(dir);
            var lvs = JsonToLevel<Level>(jsonStr);

            var width = lvs.width;
            var height = lvs.height;

            for (int y = 0; y < lvs.height; y++)
            {
                for (int x = 0; x < lvs.width; x++)
                {
                    // 한 레벨에 하고싶은 짓
                    if (lvs.tiles[lvs.width * y + x] == fromCode)
                        lvs.tiles[lvs.width * y + x] = toCode;
                }
            }

            jsonStr = JsonUtility.ToJson(lvs);

            File.WriteAllText(Application.dataPath + "/Resources/Levels/" + updateStage + "/" + lvs.name + ".json", jsonStr);
        }
    }

    private void UpdateAllFiles()
    {
        for (int i = 0; i < 5; i++)
        {
            string[] directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/", "*.json");
            List<Level> lvList = new List<Level>();

            foreach (var dir in directories)
            {
                string jsonStr = File.ReadAllText(dir);
                var lvs = JsonToLevel<Level>(jsonStr);

                var width = lvs.width;
                var height = lvs.height;

                for (int y = 0; y < lvs.height; y++)
                {
                    for (int x = 0; x < lvs.width; x++)
                    {
                        // 한 레벨에 하고싶은 짓
                        if (lvs.tiles[lvs.width * y + x] == fromCode)
                            lvs.tiles[lvs.width * y + x] = toCode;
                    }
                }

                jsonStr = JsonUtility.ToJson(lvs);

                File.WriteAllText(Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/" + lvs.name + ".json", jsonStr);

                //lvList.Add(lvs);

                //Debug.Log(JsonUtility.ToJson(lvs));
            }

            //levels.Add(i, lvList);
        }
    }

    #endregion
}
