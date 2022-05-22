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
    public LevelDBInfo selectedLevelDB;
    public static bool levelChanged = false;

    public LevelEditor.Stage stage;
    public string fileName = " ";

    // UpdateFile
    public string updateStage = " ";
    public string updatefileName = " ";
    public int fromCode;
    public int toCode;

    public event System.Action<LevelDatabase> OnSaveDB;

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
            Save(selectedLevelDB);
            JsonIO.levelChanged = false;
        }
    }

    #region Save
    public LevelDBInfo CreateNewLevel(string fileName, LevelEditor.Stage stage, int width, int height)
    {
        LevelEditorManager.instance.ResetCanvas(width, height);
        SaveAsNew(fileName, stage);

        return levelDB.jsonLevelDBs.Find(n => n.filename.Equals(fileName));
    }
    
    public LevelDBInfo CreateNewLevel(string fileName, string directory, int width, int height)
    {
        LevelEditorManager.instance.ResetCanvas(width, height);
        SaveAsNew(fileName, directory);

        return levelDB.jsonLevelDBs.Find(n => n.filename.Equals(fileName));
    }

    // Write into Text File
    private void SaveToTextFile(string path, string fileName)
    {
        var jsonStr = LevelToJson(fileName);

        File.WriteAllText(path, jsonStr);
    }

    private void SaveToTextFile(Level level, string path, string fileName)
    {
        var jsonStr = LevelToJson(level, fileName);

        File.WriteAllText(path, jsonStr);
    }

    // Save to create
    public void SaveAsNew(string fileName, LevelEditor.Stage stage)
    {
        var path = "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json";

        // Json
        SaveToTextFile(Application.dataPath + path, fileName);

        //var jsonStr = LevelToJson(fileName);

        //File.WriteAllText(Application.dataPath + path, jsonStr);

        // Database
        SaveIntoDatabase(fileName, stage, path);

        Debug.Log("Saved(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    }
    
    public void SaveAsNew(string fileName, string directory)
    {
        var path = directory;

        if (directory[directory.Length - 1].Equals('/'))
            path += fileName + ".json";
        else
            path += "/" + fileName + ".json";

        // Json
        SaveToTextFile(Application.dataPath + path, fileName);

        // Database
        SaveIntoDatabase(fileName, stage, path);

        Debug.Log("Saved(" + path + ")");
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

    public string Save(Level level, string fileName, LevelEditor.Stage stage)
    {
        var path =  "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json";

        // Json
        SaveToTextFile(level, Application.dataPath + path, fileName);

        // Database
        SaveIntoDatabase(fileName, stage, path);

        Debug.Log("Saved(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");

        return path;
    }
    
    public string Save(Level level, string fileName, string directory)
    {
        var path = directory;
        Debug.Log(directory[directory.Length - 1]);

        if (directory[directory.Length - 1].Equals('/'))
            path += fileName + ".json";
        else
            path += "/" + fileName + ".json";

        // Json
        SaveToTextFile(level, Application.dataPath + path, fileName);

        // Database
        SaveIntoDatabase(fileName, stage, path);

        Debug.Log("Saved(" + path + ")");

        return path;
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

        //LevelEditorManager.instance.DesignateTileCorner(level);
        level.AssignCorner();

        return JsonUtility.ToJson(level);
    }

    string LevelToJson(Level level, string fileName)
    {
        level.name = fileName;

        //Level level = new Level();
        //level.tiles = new int[LevelEditorManager.instance.tiles.Count];

        //level.name = fileName;

        //for (int i = 0; i < level.tiles.Length; i++)
        //    level.tiles[i] = (int)LevelEditorManager.instance.tiles[i].GetComponent<TileInfo>().tileCode;

        //level.width = (int)LevelEditorManager.instance.getCanvasSize().x;
        //level.height = (int)LevelEditorManager.instance.getCanvasSize().y;

        //LevelEditorManager.instance.DesignateTileCorner(level);

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

        LevelEditorManager.instance.DrawLevelOnCanvas(level);

        JsonIO.levelChanged = false;

        LevelEditorManager.instance.SetCanvasActive(true);

        Debug.Log("Loaded(" + path + ")");
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    #endregion

    #region Edit

    public void Edit(LevelDBInfo levelDB, string fileName, LevelEditor.Stage stage, bool reLoad = false)
    {
        // Delete Database, Json;
        var backUpLevel = DeleteJson(levelDB);

        // Save Database, Json
        var newPath = Save(backUpLevel, fileName, stage);

        // ReLoad
        var editedDB = this.levelDB.jsonLevelDBs.Find(n => n.filename.Equals(fileName));
        if (reLoad)
        {
            LoadJson(newPath);
            SelectDB(editedDB);
        }
    }
    
    public void Edit(LevelDBInfo levelDB, string fileName, string directory, bool reLoad = false)
    {
        // Delete Database, Json;
        var backUpLevel = DeleteJson(levelDB);

        // Save Database, Json
        var newPath = Save(backUpLevel, fileName, directory);

        // ReLoad
        var editedDB = this.levelDB.jsonLevelDBs.Find(n => n.filename.Equals(fileName));
        if (reLoad)
        {
            LoadJson(newPath);
            SelectDB(editedDB);
        }
    }

    #endregion

    #region Delete
    public Level DeleteJson(LevelDBInfo levelInfo)
    {
        //Debug.Log("BackUped");

        // BackUp
        Level backUpLevel = new Level();
        backUpLevel = LoadFromTextFile<Level>(Application.dataPath + levelInfo.path);

        //Debug.Log("BackUped");

        // Database
        RemoveDatabase(levelInfo);
        
        // Json
        File.Delete(Application.dataPath + levelInfo.path);

        if (levelInfo.CompareTo(selectedLevelDB) == 1)
        {
            SelectDB(new LevelDBInfo());

            LevelEditorManager.instance.SetCanvasActive(false);
        }

        return backUpLevel;
    }
    #endregion

    #region Database

    public void SelectDB(LevelDBInfo levelDB)
    {
        selectedLevelDB = levelDB;
    }

    void SaveIntoDatabase(string filename, LevelEditor.Stage stage, string path)
    {
        levelDB.Add(filename, stage, path);

        //UnityEditor.EditorUtility.SetDirty(levelDB);
        OnSaveDB.Invoke(levelDB);
    }

    void SaveIntoDatabase(LevelDBInfo newDB)
    {
        SaveIntoDatabase(newDB.filename, newDB.stage, newDB.path);
    }

    void RemoveDatabase(LevelDBInfo removeDB)
    {
        levelDB.Remove(removeDB);

        //UnityEditor.EditorUtility.SetDirty(levelDB);
        OnSaveDB.Invoke(levelDB);
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

        // �� �ִ��� �𸣰ڴ� �ڵ����
        //LevelEditorManager.instance.DesignateTileCorner(lvs);

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

    public void UpdateAllDatabase()
    {
        RemoveAllDatabase();

        string[] directories;
        List<LevelDBInfo> loadedDBs = new List<LevelDBInfo>();

        for (int i = 0; i < levelDB.folderPaths.Length; i++)
        {
            // Levels
            //var path = Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/";
            var path = Application.dataPath + "/Resources/Levels/" + levelDB.folderPaths[i] + "/";

            var dbs = LoadAllDatabase(path + "Main/", (LevelEditor.Stage)i);

            loadedDBs.AddRange(dbs);

            // Entres
            dbs = LoadAllDatabase(path + "Entre/", (LevelEditor.Stage)i);

            loadedDBs.AddRange(dbs);

            // Exits
            dbs = LoadAllDatabase(path + "Exit/", (LevelEditor.Stage)i);

            loadedDBs.AddRange(dbs);

            // Bosses
            dbs = LoadAllDatabase(path + "Boss/Entre/", (LevelEditor.Stage)i);

            loadedDBs.AddRange(dbs);

            // Bosses Entre
            dbs = LoadAllDatabase(path + "Boss/Main/", (LevelEditor.Stage)i);

            loadedDBs.AddRange(dbs);

            //directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/", "*.json");

            //foreach (var dir in directories)
            //{
            //    LevelDBInfo newDB = new LevelDBInfo();
            //    newDB.path = dir.Replace(Application.dataPath, "");

            //    var lvs = LoadFromTextFile<Level>(dir);
            //    //string jsonStr = File.ReadAllText(dir);
            //    //var lvs = JsonToLevel<Level>(jsonStr);

            //    // �� �ִ��� �𸣰ڴ� �ڵ����
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
                // �� ������ �ϰ���� ��
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
                    // �� ������ �ϰ���� ��
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
                        // �� ������ �ϰ���� ��
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
