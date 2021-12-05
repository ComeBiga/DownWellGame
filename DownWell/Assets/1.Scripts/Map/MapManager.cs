using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelGenerator))]
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
    MapDisplay mapDisplay;
    LevelGenerator lg;
    LoadLevel loadLevel;

    public int width = 10;
    public int height = 100;

    public Stage currentStage = Stage.Stage1;

    int currentYpos = 0;
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

        StartCoroutine(GenerateMap());
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

    IEnumerator GenerateMap()
    {
        yield return null;

        // Ÿ���� �� �����ϰ� �� �� Y position;
        currentYpos = 0;

        List<Level> stageStarts = LoadLevel.instance.GetObjects("StageStart");
        Level stageStart = stageStarts[0];
        currentYpos -= mapDisplay.Display(stageStart, currentYpos);

        if (SceneManager.GetActiveScene().name=="StartScene") yield break;

        for (;(-currentYpos) < height;)
        {
            // �������� �ҷ��� ������ ���� y ��ġ���� ����
            currentYpos -= mapDisplay.Display(loadLevel.RandomLevel(currentStage), currentYpos);
        }

        // �������� ���� �����ϴ� �ڵ�
        List<Level> stageGrounds = LoadLevel.instance.GetObjects("StageGround");
        Level stageGround = stageGrounds[0];

        currentYpos -= mapDisplay.Display(stageGround, currentYpos);
    }

    IEnumerator GenerateMap(int times)
    {
        for (int i = 0; i < times; i++)
        {
            currentYpos -= mapDisplay.Display(loadLevel.RandomLevel(currentStage), currentYpos);

            yield return null;
        }
    }

    public void Generate(int times)
    {
        StartCoroutine(GenerateMap(times));
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

    public void GenerateInfinity(Transform mainPos, int times)
    {
        StartCoroutine(GenerateMapInfinity(mainPos, times));
    }
}
