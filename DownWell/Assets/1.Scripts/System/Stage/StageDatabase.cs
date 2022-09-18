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

    [SerializeField]
    private string path;
    public string Path { get { return path; } }

    public int stageLength = 200;

    [Header("Wall")]
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<Sprite> wallSprites;
    [SerializeField] private List<Sprite> mapObjectSprites;
    [SerializeField] private List<Sprite> blockSprites;
    [SerializeField] private List<Sprite> platformSprites;
    [SerializeField] private List<Sprite> itemGiverSprites;
    [SerializeField] private List<Sprite> itemGiverLockSprites;
    [SerializeField] private List<GameObject> dropItems;
    [SerializeField] private List<TileBase> tileBases;

    public List<GameObject> DropItems { get { return dropItems; } }

    [Header("Enemy")]
    [SerializeField] private List<GameObject> enemyObjects;

    [Header("Boss")]
    [SerializeField] private GameObject bossObject;

    [Header("Background")]
    //[SerializeField] private Sprite baseBackground;
    //[SerializeField] private BackgroundSprite[] background;
    public BackgroundInfo bgInfo;

    [Header("Sound")]
    [SerializeField] private string bgm;

    public string BGM
    {
        get
        {
            return bgm;
        }
    }

    public List<GameObject> MapObjects 
    { 
        get 
        {
            //var mos = mapObjects;

            //foreach (var mo in mos)
            //{
            //    mo.GetComponent<Wall>().SetSpriteByStage(num);
            //}

            //return mos;
            return mapObjects; 
        } 
    }

    public List<Sprite> WallSprites { get { return wallSprites; } }
    public List<Sprite> BlockSprites { get { return blockSprites; } }
    public List<Sprite> PlatformSprites { get { return platformSprites; } }
    public List<Sprite> ItemGiverSprites { get { return itemGiverSprites; } }
    public List<Sprite> ItemGiverLockSprites { get { return itemGiverLockSprites; } }
    public List<TileBase> TileBases { get { return tileBases; } }
    public List<GameObject> EnemyObjects { get { return enemyObjects; } }
    public GameObject BossObject { get { return bossObject; } }

    public GameObject InstantiateMapObject(int tileCode, Vector3 position, Transform parent)
    {
        GameObject newGo = null;

        if(tileCode >= 100 && tileCode < 1000)
        {
            newGo = Instantiate(mapObjects[0], position, Quaternion.identity, parent);
            newGo.GetComponent<SpriteRenderer>().sprite = WallSprites[tileCode - 100];
        }
        else
        {
            var go = mapObjects.Find(g => g.GetComponent<Wall>().info.code == tileCode);
            newGo = Instantiate(go, position, Quaternion.identity, parent);

            var index = mapObjects.IndexOf(go);
            if(mapObjectSprites[index] != null) newGo.GetComponent<SpriteRenderer>().sprite = mapObjectSprites[index];
        }

        return newGo;
    }


    //public Sprite BaseBackGround { get { return baseBackground; } }
    //public Sprite Background
    //{
    //    get
    //    {
    //        string seed = (Time.time + Random.value).ToString();
    //        System.Random rand = new System.Random(seed.GetHashCode());

    //        return background[rand.Next(0, background.Length)].sprite[0];
    //    }
    //}

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
        return path.Replace("/Resources/", "");
#endif
        //if (resourceLoad)
        //    return path.Replace("/Resources", "");
        //else
        //    return path;
    }
}
