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

    // Start is called before the first frame update
    void Start()
    {
        //mapGen = GetComponent<MapGenerator>();
        mapDisplay = GetComponent<MapDisplay>();
        lg = GetComponent<LevelGenerator>();
        loadLevel = LoadLevel.instance;
        sm = StageManager.instance;

        // Init Object
        mapDisplay.SetObjects(sm.Current);

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

    private void Display(string objName, int index = 0)
    {
        List<Level> lvs = LoadLevel.instance.GetObjects(objName);
        Level lv = lvs[index];
        currentYpos -= mapDisplay.Display(lv, currentYpos);
    }

    private void Display(List<Level> levels, int height)
    {
        for (; (-currentYpos) < height;)
        {
            // 랜덤으로 불러온 레벨을 현재 y 위치에서 생성
            currentYpos -= mapDisplay.Display(levels[CatDown.Random.Get().Next(levels.Count)], currentYpos);
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

        // 타일을 다 생성하고 난 후 Y position;
        currentYpos = 0;

        List<Level> stageStarts = LoadLevel.instance.GetObjects("StageStart");
        Level stageStart = stageStarts[0];
        currentYpos -= mapDisplay.Display(stageStart, currentYpos);

        Debug.Log("After Display");

        if (SceneManager.GetActiveScene().name=="StartScene") yield break;

        for (;(-currentYpos) < height;)
        {
            // 랜덤으로 불러온 레벨을 현재 y 위치에서 생성
            currentYpos -= mapDisplay.Display(RandomLevel(sm.CurrentStageIndex), currentYpos);
        }

        // 스테이지 끝을 생성하는 코드
        List<Level> stageGrounds = LoadLevel.instance.GetObjects("StageGround");
        Level stageGround = stageGrounds[0];

        currentYpos -= mapDisplay.Display(stageGround, currentYpos);
    }

    public void GenerateBeforeUpdate()
    {
        // 타일을 다 생성하고 난 후 Y position;
        currentYpos = 0;

        // 스테이지 시작 부분
        Display("StageStart");

        // 랜덤 레벨 생성
        Display(LoadLevel.instance.GetLevels(sm.CurrentStageIndex), height);

        // 스테이지 끝을 생성하는 코드
        Display("StageGround");
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
        StartCoroutine(GenerateMapInfinity(mainPos, times));
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

    #endregion

    
}
