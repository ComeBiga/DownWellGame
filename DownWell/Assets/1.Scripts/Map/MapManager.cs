using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    //MapGenerator mapGen;
    public MapDisplay mapDisplay;
    LevelGenerator lg;
    LoadLevel loadLevel;
    StageManager sm;

    public int width = 10;
    public int height = 100;

    //public LevelEditor.Stage currentStage = LevelEditor.Stage.Stage1;

    private int currentYpos = 0;
    public int CurrentYPos { get { return currentYpos; } }

    private bool reGenerate = false;
    [SerializeField] private int reGenerateOffset = 20;

    private Coroutine crInfinity;

    // Start is called before the first frame update
    void Start()
    {
        //mapGen = GetComponent<MapGenerator>();
        mapDisplay = GetComponent<MapDisplay>();
        lg = GetComponent<LevelGenerator>();
        loadLevel = LoadLevel.instance;
        sm = StageManager.instance;

        // Init Object
        //mapDisplay.SetObjects(sm.Current);

        // Generate
        //StartCoroutine(GenerateMap());
    }

    //IEnumerator FirstGenerateMap()
    //{
    //    yield return null;

    //    //Tile[,] genMap = mapGen.GenerateMap();
    //    //mapDisplay.Display(genMap);

    //    int[,] genLev = lg.GenerateLevel();
    //    int[,] genSgr = lg.GenerateStageGround();
    //    mapDisplay.Display(genLev, genSgr);
    //    //GameObject newGameObject = new GameObject();
    //    //Instantiate(new GameObject());
    //}

    #region Public Functions

    public void Clear()
    {
        var mo = GetComponentsInChildren<Transform>();

        foreach(var m in mo)
        {
            if (m != this.transform)
                Destroy(m.gameObject);
        }
    }

    #endregion

    #region Generating Elements Function

    private void GenerateLevel(string objName, int index = 0)
    {
        List<Level> lvs = LoadLevel.instance.GetObjects(objName);
        Level lv = lvs[index];
        currentYpos -= mapDisplay.Display(lv, currentYpos);
    }

    private void GenerateLevel(List<Level> levels)
    {
        currentYpos -= mapDisplay.Display(levels[CatDown.Random.Get().Next(levels.Count)], currentYpos);
    }

    private void GenerateLevels(List<Level> levels, int height)
    {
        for (; (-currentYpos) < height;)
        {
            // �������� �ҷ��� ������ ���� y ��ġ���� ����
            currentYpos -= mapDisplay.Display(levels[CatDown.Random.Get().Next(levels.Count)], currentYpos);
        }
    }

    private void GenerateLevelsSeveralTimes(List<Level> levels, int times)
    {
        crInfinity = StartCoroutine(EGenerateLevelsSeveralTimes(levels, times));
    }

    private IEnumerator EGenerateLevelsSeveralTimes(List<Level> levels, int times)
    {
        for (int i = 0; i < times; i++)
        {
            currentYpos -= mapDisplay.Display(levels[CatDown.Random.Get().Next(levels.Count)], currentYpos);

            yield return null;
        }
    }

    #endregion

    #region Basic Generating
    public void Generate()
    {
        StartCoroutine(GenerateMap());
    }

    IEnumerator GenerateMap()
    {
        yield return null;

        // Ÿ���� �� �����ϰ� �� �� Y position;
        currentYpos = 0;

        //List<Level> stageStarts = LoadLevel.instance.GetObjects("StageStart");
        //Level stageStart = stageStarts[0];
        //currentYpos -= mapDisplay.Display(stageStart, currentYpos);

        Level stageEntre = LoadLevel.instance.LoadAndGetLevel(sm.Current.name + "/Entre");
        currentYpos -= mapDisplay.Display(stageEntre, currentYpos);

        Debug.Log("After Display");

        if (SceneManager.GetActiveScene().name=="StartScene") yield break;

        for (;(-currentYpos) < height;)
        {
            // �������� �ҷ��� ������ ���� y ��ġ���� ����
            currentYpos -= mapDisplay.Display(RandomLevel(sm.CurrentStageIndex), currentYpos);
        }

        // �������� ���� �����ϴ� �ڵ�
        List<Level> stageGrounds = LoadLevel.instance.GetObjects("StageGround");
        Level stageGround = stageGrounds[0];

        currentYpos -= mapDisplay.Display(stageGround, currentYpos);
    }

    public void GenerateBeforeUpdate()
    {
        // Ÿ���� �� �����ϰ� �� �� Y position;
        currentYpos = 0;

        // �������� ���� �κ�
        //GenerateLevel("StageStart");
        GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.ENTRE, sm.Current.Num)));
        //Level stageEntre = LoadLevel.instance.LoadAndGet(sm.Current.Name + "/Entre");
        //currentYpos -= mapDisplay.Display(stageEntre, currentYpos);

        // ���� ���� ����
        GenerateLevels(LoadLevel.instance.GetLevels(sm.CurrentStageIndex), height);

        //Level stageExit = LoadLevel.instance.LoadAndGet(sm.Current.Name + "/Exit");
        //currentYpos -= mapDisplay.Display(stageExit, currentYpos);

        // �������� ���� �����ϴ� �ڵ�
        //GenerateLevel("StageGround");
        if (sm.Current.BossObject != null)
        {
            GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.BOSS, sm.Current.Num)));
        }
        else
            GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.EXIT, sm.Current.Num)));

        
    }

    public void GenerateStageEnd()
    {
        GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.EXIT, sm.Current.Num)));
    }

    // Generate levels by times
    public void Generate(int times)
    {
        StartCoroutine(GenerateMap(times));
    }

    IEnumerator GenerateMap(int times)
    {
        for (int i = 0; i < times; i++)
        {
            currentYpos -= mapDisplay.Display(RandomLevel(sm.CurrentStageIndex), currentYpos);

            yield return null;
        }
    }



    Level RandomLevel(int stageIndex)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        List<Level> levels = LoadLevel.instance.GetLevels(stageIndex);
        //Debug.Log(levels.Count);
        Level randomWall = levels[rand.Next(0, levels.Count)];

        return randomWall;
    }

    #endregion


    #region Infinity Generating

    public void GenerateInfinity(Transform mainPos, int times)
    {
        crInfinity = StartCoroutine(GenerateMapInfinity(mainPos, times));
    }

    IEnumerator GenerateMapInfinity(Transform mainPos, int times)
    {
        reGenerate = true;

        while(true)
        {
            if (!reGenerate) break;

            if (mainPos.position.y < currentYpos + reGenerateOffset)
                Generate(times);

            yield return null;
        }

        reGenerate = false;
    }

    public void GenerateBossLevels(Transform mainPos, int times)
    {
        Debug.Log("GenerateInfinity");
        crInfinity = StartCoroutine(EGenerateBossLevels(mainPos, times));
    }

    private IEnumerator EGenerateBossLevels(Transform mainPos, int times)
    {
        reGenerate = true;

        while (true)
        {
            if (!reGenerate) break;

            if (mainPos.position.y < currentYpos + reGenerateOffset)
                GenerateLevelsSeveralTimes(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.BOSS_LEVEL, sm.Current.Num)), times);

            Debug.Log("EGenerateBossLevels");
            Debug.Log(crInfinity);
            yield return null;
        }

        reGenerate = false;
    }

    public void StopGenerateInfinity()
    {
        StopCoroutine(crInfinity);
        reGenerate = false;
        Debug.Log("Stopped");
    }

    #endregion

    
}
