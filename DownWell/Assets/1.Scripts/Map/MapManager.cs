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
    }

    #region Public Functions

    public void Clear()
    {
        //var mo = GetComponentsInChildren<Transform>();

        //foreach(var m in mo)
        //{
        //    if (m != this.transform)
        //        Destroy(m.gameObject);
        //}
        mapDisplay.ClearAll();
    }

    #endregion

    #region Generating Elements Function

    #region Deprecated(GenerateLevel(objName)
    //private void GenerateLevel(string objName, int index = 0)
    //{
    //    List<Level> lvs = LoadLevel.instance.GetObjects(objName);
    //    Level lv = lvs[index];
    //    currentYpos -= mapDisplay.Display(lv, currentYpos);
    //}
    #endregion

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

    #region Deprecated(Generate)
    //public void Generate()
    //{
    //    StartCoroutine(GenerateMap());
    //}

    //IEnumerator GenerateMap()
    //{
    //    yield return null;

    //    // Ÿ���� �� �����ϰ� �� �� Y position;
    //    currentYpos = 0;

    //    //List<Level> stageStarts = LoadLevel.instance.GetObjects("StageStart");
    //    //Level stageStart = stageStarts[0];
    //    //currentYpos -= mapDisplay.Display(stageStart, currentYpos);

    //    var stageEntre = LoadLevel.instance.GetLevels(LoadLevel.LevelType.ENTRE, sm.Current.Num);
    //    currentYpos -= mapDisplay.Display(stageEntre[CatDown.Random.Get().Next(stageEntre.Count)], currentYpos);

    //    Debug.Log("After Display");

    //    if (SceneManager.GetActiveScene().name=="StartScene") yield break;

    //    for (;(-currentYpos) < height;)
    //    {
    //        // �������� �ҷ��� ������ ���� y ��ġ���� ����
    //        currentYpos -= mapDisplay.Display(RandomLevel(sm.CurrentStageIndex), currentYpos);
    //    }

    //    // �������� ���� �����ϴ� �ڵ�
    //    List<Level> stageGrounds = LoadLevel.instance.GetObjects("StageGround");
    //    Level stageGround = stageGrounds[0];

    //    currentYpos -= mapDisplay.Display(stageGround, currentYpos);
    //}
    #endregion

    public void GenerateBeforeUpdate()
    {
        // Ÿ���� �� �����ϰ� �� �� Y position;
        currentYpos = 0;

        // �������� ���� �κ�
        //GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.ENTRE, sm.Current.Num)));
        GenerateLevel(loadLevel.GetLevels(LoadLevel.LevelType.ENTRE, sm.Current.Num));

        // ���� ���� ����
        GenerateLevels(loadLevel.GetLevels(LoadLevel.LevelType.MAIN, sm.Current.Num), height);

        // �������� ���� �����ϴ� �ڵ�
        if (sm.Current.BossObject != null)
        {
            //GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.BOSS, sm.Current.Num)));
            GenerateLevel(loadLevel.GetLevels(LoadLevel.LevelType.BOSS, sm.Current.Num));
        }
        else
        {
            //GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.EXIT, sm.Current.Num)));
            GenerateLevel(loadLevel.GetLevels(LoadLevel.LevelType.EXIT, sm.Current.Num));
        }
        
    }

    public void GenerateStageEnd()
    {
        //GenerateLevel(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.EXIT, sm.Current.Num)));
        GenerateLevel(loadLevel.GetLevels(LoadLevel.LevelType.EXIT, sm.Current.Num));
    }

    #region Deprecated(GenerateByTimes)
    // Generate levels by times
    //public void Generate(int times)
    //{
    //    StartCoroutine(GenerateMap(times));
    //}

    //IEnumerator GenerateMap(int times)
    //{
    //    for (int i = 0; i < times; i++)
    //    {
    //        currentYpos -= mapDisplay.Display(RandomLevel(sm.CurrentStageIndex), currentYpos);

    //        yield return null;
    //    }
    //}
    #endregion

    #region Deprecated(RandomLevel)
    //Level RandomLevel(int stageIndex)
    //{
    //    string seed = (Time.time + Random.value).ToString();
    //    System.Random rand = new System.Random(seed.GetHashCode());

    //    List<Level> levels = LoadLevel.instance.GetLevels(stageIndex);
    //    //Debug.Log(levels.Count);
    //    Level randomWall = levels[rand.Next(0, levels.Count)];

    //    return randomWall;
    //}
    #endregion
    #endregion


    #region Infinity Generating

    #region Deprecated(GenerateInfinity)
    //public void GenerateInfinity(Transform mainPos, int times)
    //{
    //    crInfinity = StartCoroutine(GenerateMapInfinity(mainPos, times));
    //}

    //IEnumerator GenerateMapInfinity(Transform mainPos, int times)
    //{
    //    reGenerate = true;

    //    while(true)
    //    {
    //        if (!reGenerate) break;

    //        if (mainPos.position.y < currentYpos + reGenerateOffset)
    //            Generate(times);

    //        yield return null;
    //    }

    //    reGenerate = false;
    //}
    #endregion

    public void GenerateBossLevels(Transform mainPos, int times)
    {
        //Debug.Log("GenerateInfinity");
        crInfinity = StartCoroutine(EGenerateBossLevels(mainPos, times));
    }

    private IEnumerator EGenerateBossLevels(Transform mainPos, int times)
    {
        reGenerate = true;

        while (true)
        {
            if (!reGenerate) break;

            if (mainPos.position.y < currentYpos + reGenerateOffset)
            {
                //GenerateLevelsSeveralTimes(loadLevel.LoadAndGetLevels(loadLevel.GetPath(LoadLevel.LevelType.BOSS_LEVEL, sm.Current.Num)), times);
                GenerateLevelsSeveralTimes(loadLevel.GetLevels(LoadLevel.LevelType.BOSS_LEVEL, sm.Current.Num), times);
            }
            //Debug.Log("EGenerateBossLevels");
            //Debug.Log(crInfinity);
            yield return null;
        }

        reGenerate = false;
    }

    public void StopGenerateInfinity()
    {
        StopCoroutine(crInfinity);
        reGenerate = false;
        //Debug.Log("Stopped");
    }

    #endregion

    
}
