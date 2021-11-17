using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Database", menuName = "Database/levelDB")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelDBInfo> jsonLevelDBs = new List<LevelDBInfo>();

    public void Add(string filename, Stage stage, string path)
    {
        LevelDBInfo newInfo = new LevelDBInfo(filename, stage, path);
        jsonLevelDBs.Add(newInfo);
    }

    public void Remove(LevelDBInfo deleteLevel)
    {
        jsonLevelDBs.Remove(deleteLevel);
    }
}

[System.Serializable]
public class LevelDBInfo : IComparable<LevelDBInfo>
{
    public string filename;
    public Stage stage;
    public string path;

    public LevelDBInfo()
    {

    }

    public LevelDBInfo(string filename, Stage stage, string path)
    {
        this.filename = filename;
        this.stage = stage;
        this.path = path;
    }

    public int CompareTo(LevelDBInfo other)
    {
        throw new NotImplementedException();
    }

    public void Save(string filename, Stage stage, string path)
    {
        this.filename = filename;
        this.stage = stage;
        this.path = path;
    }
}
