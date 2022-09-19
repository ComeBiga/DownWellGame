using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    private StageManager stageManager;
    private MapManager mapManager;
    private PlayerManager playerManager;

    public CharacterCollector collector;
    public GameObject playerPrefab;
    private GameObject playerCharacter;
    public Transform startPos;
    public Vector3 instantiatePosition;
    public float dropCharacterDelay;
    public float stageClearDelay;
    public int lastStage = 3;

    [Header("UI")]
    public Transform worldSpaceUI;
    public Score score;
    public CoinCount coin;

    [HideInInspector] public float enemyActiveRangeOffset = 0;

    private GameObject gameoverPanel;

    //public GameObject boss;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        stageManager = StageManager.instance;
        mapManager = MapManager.instance;
        playerManager = PlayerManager.instance;

        // Player Init
        //PlayerManager.instance.selectedCharacter.InitPlayerValues(playerPrefab);

        //playerPrefab = Instantiate(playerPrefab, startPos.position, Quaternion.identity);
        //playerPrefab.SetActive(false);

        //Camera.main.GetComponent<SmoothFollow>().InitFollowCamera(playerPrefab.transform);

        //PlayerManager.instance.player = playerPrefab;

        // Timer
        //GetComponent<Timer>().StartTimer();

        // Play BGM
        //SoundManager.instance.PlayBGMSound("Background");
        //if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Background");

        // Gameover Panel Init
        gameoverPanel = GameObject.Find("GameOver");
        gameoverPanel.GetComponent<Image>().enabled = true;
        gameoverPanel.SetActive(false);

        StartStage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClearStage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartStage()
    {
        // Material


        // Level Generation
        MapManager.instance.GenerateBeforeUpdate();

        // Player Initialization
        playerManager.Instantiate(instantiatePosition);
        DropCharacterLateSeconds();

        // Camera
        Camera.main.GetComponent<SmoothFollow>().StartStage();

        // Sound
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play(stageManager.Current.BGM);

        // Timer
        GetComponent<Timer>().StartTimer();
    }

    public void ClearStage()
    {

        var lastBGM = stageManager.Current.BGM;
        stageManager.NextStage();

        if (stageManager.CurrentStageIndex == lastStage + 1)
        {
            GameOver();
            return;
        }

        mapManager.Clear();

        mapManager.GenerateBeforeUpdate();

        playerManager.playerObject.transform.position = instantiatePosition;
        DropCharacterLateSeconds();

        Camera.main.GetComponent<SmoothFollow>().StartStage();

        if (Comebiga.SoundManager.instance != null && stageManager.Current.BGM != lastBGM)
        {
            Comebiga.SoundManager.instance.Stop(lastBGM);
            Comebiga.SoundManager.instance.Play(stageManager.Current.BGM);
        }
    }

    public void EndStage()
    {
        Camera.main.GetComponent<SmoothFollow>().StageEnd();

        FallIntoNextStage();
    }

    public void GameOver()
    {
        // Timer
        GetComponent<Timer>().EndTimer();

        // Gameover UI Panel
        Invoke("GameOverPanel", 3f);
    }

    void GameOverPanel()
    {
        gameoverPanel.SetActive(true);
        // AchievementSystem.Instance.ProgressAchievement("Coin", UICollector.Instance.coin.Current);
        // gameoverPanel.GetComponent<UIGameOver>().TurnOnAchievementPanel(AchievementSystem.Instance.RewardAsAllAchieved());
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + UICollector.Instance.coin.Current);
    }

    private void DropCharacterLateSeconds()
    {
        StartCoroutine(EDropCharacterLateSeconds());
    }

    private IEnumerator EDropCharacterLateSeconds()
    {
        playerManager.playerObject.GetComponent<PlayerPhysics>().InitVelocity();
        playerManager.playerObject.GetComponent<PlayerPhysics>().UseGravity(false);
        playerManager.playerObject.GetComponent<PlayerController>().cantMove = true;

        yield return new WaitForSeconds(dropCharacterDelay);

        playerManager.playerObject.GetComponent<PlayerPhysics>().UseGravity(true);
        playerManager.playerObject.GetComponent<PlayerController>().cantMove = false;
    }

    private void FallIntoNextStage()
    {
        StartCoroutine(EFallIntoNextStage());
    }

    private IEnumerator EFallIntoNextStage()
    {
        while (true)
        {
            if (playerManager.playerObject.transform.position.y <= mapManager.CurrentYPos - 3f)
                break;

            yield return null;
        }

        playerManager.playerObject.GetComponent<PlayerController>().cantMove = true;
        playerManager.playerObject.GetComponent<PlayerPhysics>().InitVelocity();
        playerManager.playerObject.GetComponent<PlayerPhysics>().UseGravity(false);

        Invoke("ClearStage", stageClearDelay);
    }

    #region Deprecated
    public bool CheckTargetRange(Transform enemy)
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * (9 / 16);

        float h_tarTothis = Mathf.Abs(playerPrefab.transform.position.y - enemy.position.y);

        if (height / 2 + enemyActiveRangeOffset < enemy.position.y - playerPrefab.transform.position.y)
            Destroy(enemy.gameObject);

        if (h_tarTothis < height / 2 + enemyActiveRangeOffset)
            return true;

        return false;
    }
    #endregion


    //public void StageEnd()
    //{
    //    //SceneManager.LoadScene(0);
    //    boss.SetActive(true);
    //    Camera.main.GetComponent<SmoothFollow>().StartBossCamera();
    //    MapManager.instance.GenerateInfinity(PlayerManager.instance.player.transform, 10);
    //}

    public void homeBtn()
    {
        SceneManager.LoadScene(0);
    }
}
