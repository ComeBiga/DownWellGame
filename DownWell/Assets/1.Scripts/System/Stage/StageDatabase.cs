using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public int stageLength = 200;

    public List<MapObjectInfo> mapObjectInfos;

    [Header("Wall")]
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<GameObject> dropItems;
    [SerializeField] private List<TileBase> tileBases;
    [SerializeField] private List<Material> materials;

    public List<GameObject> DropItems { get { return dropItems; } }

    [Header("Enemy")]
    [SerializeField] private List<GameObject> enemyObjects;

    [Header("Boss")]
    [SerializeField] private GameObject bossObject;

    [Header("Sound")]
    [SerializeField] private string bgm;

    public string BGM
    {
        get
        {
            return bgm;
        }
    }

    public List<TileBase> TileBases { get { return tileBases; } }
    public List<GameObject> EnemyObjects { get { return enemyObjects; } }
    public GameObject BossObject { get { return bossObject; } }
    public List<Material> Materials { get { return materials; } }

    public MapObjectInfo GetMapObjectInfo(int code)
    {
        return mapObjectInfos.Find(info => info.id == code);
    }

    [System.Serializable]
    public class MapObjectInfo
    {
        public string name;
        public int id;
        public Sprite sprite;
    }
}
