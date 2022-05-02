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

    [Header("Wall")]
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<Sprite> wallSprites;
    [SerializeField] private List<Sprite> blockSprites;
    [SerializeField] private List<Sprite> platformSprites;

    [Header("Enemy")]
    [SerializeField] private List<GameObject> enemyObjects;

    [Header("Boss")]
    [SerializeField] private GameObject bossObject;

    [Header("Background")]
    //[SerializeField] private Sprite baseBackground;
    //[SerializeField] private BackgroundSprite[] background;
    public BackgroundInfo bgInfo;

    //[System.Serializable]
    //private class BackgroundSprite
    //{
    //    public Sprite[] sprite;

    //    public int width = 1;
    //    public int height = 1;
    //}

    public List<GameObject> MapObjects 
    { 
        get 
        {
            var mos = mapObjects;

            foreach (var mo in mos)
            {
                mo.GetComponent<Wall>().SetSpriteByStage(num);
            }

            return mos;
            //return mapObjects; 
        } 
    }

    public List<Sprite> WallSprites { get { return wallSprites; } }
    public List<Sprite> BlockSprites { get { return blockSprites; } }
    public List<Sprite> PlatformSprites { get { return platformSprites; } }
    public List<GameObject> EnemyObjects { get { return enemyObjects; } }
    public GameObject BossObject { get { return bossObject; } }


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



    public List<GameObject> GetMapObjects()
    {
        var mos = mapObjects;

        foreach(var mo in mos)
        {
            mo.GetComponent<Wall>().SetSpriteByStage(num);
        }

        return mos;
    }

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
