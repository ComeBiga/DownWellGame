using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new StageDB", menuName = "Database/StageDB")]
public class StageDatabase : ScriptableObject
{
    [Header("Info")]
    [SerializeField]
    private string name = "new stage";
    public string Name { get { return name; } }

    [SerializeField]
    private int num;
    public int Num { get { return num; } }

    [SerializeField]
    private string path;
    public string Path { get { return path; } }

    private string assetPath;
    public string AssetPath { get { return assetPath; } set { assetPath = value; } }

    [Header("Wall")]
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<Sprite> wallSprites;

    [Header("Enemy")]
    [SerializeField] private List<GameObject> enemyObjects;

    [Header("Boss")]
    [SerializeField] private GameObject bossObject;

    public List<GameObject> MapObjects { get { return mapObjects; } }
    public List<Sprite> WallSprites { get { return wallSprites; } }
    public List<GameObject> EnemyObjects { get { return enemyObjects; } }
    public GameObject BossObject { get { return bossObject; } }

    /// <summary>
    /// If resourceLoad is true, remove 'Resources/' directory.
    /// </summary>
    /// <param name="resourceLoad"></param>
    /// <returns></returns>
    public string GetPath()
    {
#if UNITY_EDITOR
        return path;
#elif UNITY_ANDROID || UNITY_STANDALONE_WIN
        return path.Replace("/Resources", "");
#endif
        //if (resourceLoad)
        //    return path.Replace("/Resources", "");
        //else
        //    return path;
    }
}
