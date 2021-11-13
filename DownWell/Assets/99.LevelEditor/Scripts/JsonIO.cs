using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Stage { Stage1, Stage2, Stage3, Stage4, Stage5, Blocks, StageGround }

public class JsonIO : MonoBehaviour
{
    public LevelDatabase levelDB;
    public LevelDBInfo selectedDB;
    public static bool levelChanged = false;

    public Stage stage;
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
        //SaveAllDatabase();
    }

    public void Update()
    {
        if(JsonIO.levelChanged)
            SaveToJson(selectedDB);
    }

    public void SelectDB(LevelDBInfo levelDB)
    {
        selectedDB = levelDB;
    }

    public LevelDBInfo CreateLevelJson(int width, int height)
    {
        LevelEditorManager.instance.ResetCanvas(width, height);
        SaveToJson();

        return levelDB.jsonLevelDBs.Find(n => n.filename.Equals(fileName));
    }

    public void SaveToJson()
    {
        var jsonStr = LevelToJson();

        var path = Application.dataPath + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json";

        File.WriteAllText(path, jsonStr);

        SaveIntoDatabase(fileName, stage, path);

        Debug.Log("Saved(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    }

    public void SaveToJson(LevelDBInfo levelInfo)
    {
        var jsonStr = LevelToJson();

        var path = levelInfo.path;

        File.WriteAllText(path, jsonStr);

        JsonIO.levelChanged = false;

        Debug.Log("Saved(" + levelInfo.filename + ")");
    }

    string LevelToJson()
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

    void SaveIntoDatabase(LevelDBInfo newDB)
    {
        SaveIntoDatabase(newDB.filename, newDB.stage, newDB.path);
    }

    void SaveIntoDatabase(string filename, Stage stage, string path)
    {
        levelDB.Add(filename, stage, path);
    }

    void SaveAllDatabase()
    {
        for (int i = 0; i < 5; i++)
        {
            string[] directories = Directory.GetFiles(Application.dataPath + "/Resources/Levels/Stage" + (i + 1).ToString() + "/", "*.json");
            List<Level> lvList = new List<Level>();

            foreach (var dir in directories)
            {
                LevelDBInfo newDB = new LevelDBInfo();
                newDB.path = dir;

                string jsonStr = File.ReadAllText(dir);
                var lvs = JsonToLevel<Level>(jsonStr);

                newDB.filename = lvs.name;
                newDB.stage = (Stage)(i + 1);

                SaveIntoDatabase(newDB);
            }
        }
    }

    public void LoadJson()
    {
        string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json");

        var level = JsonToLevel<Level>(jsonStr);

        LevelEditorManager.instance.LoadLevel(level);

        LevelEditorManager.instance.SetCanvasActive(true);

        Debug.Log("Loaded(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    }

    public void LoadJson(string path)
    {
        string jsonStr = File.ReadAllText(path);

        var level = JsonToLevel<Level>(jsonStr);

        LevelEditorManager.instance.LoadLevel(level);

        JsonIO.levelChanged = false;

        LevelEditorManager.instance.SetCanvasActive(true);

        Debug.Log("Loaded(" + path +")");
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void DeleteJson(LevelDBInfo levelInfo)
    {
        levelDB.Remove(levelInfo);
        File.Delete(levelInfo.path);
        SelectDB(new LevelDBInfo());

        LevelEditorManager.instance.SetCanvasActive(false);
    }

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
}
