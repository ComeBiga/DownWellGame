using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

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
    }
    #endregion

    public GameObject playerPrefab;
    public Transform startPos;

    public float enemyActiveRangeOffset = 0;

    public int coin = 0;

    private GameObject gameoverPanel;

    //public GameObject boss;

    private void Start()
    {
        // Player Init
        PlayerManager.instance.selectedCharacter.InitPlayerValues(playerPrefab);

        playerPrefab = Instantiate(playerPrefab, startPos.position, Quaternion.identity);
        //playerPrefab.SetActive(false);

        Camera.main.GetComponent<SmoothFollow>().InitFollowCamera(playerPrefab.transform);

        PlayerManager.instance.player = playerPrefab;

        // Timer
        //GetComponent<Timer>().StartTimer();

        // Play BGM
        SoundManager.instance.PlayBGMSound("Background");

        // Gameover Panel Init
        gameoverPanel = GameObject.Find("GameOver");
        gameoverPanel.GetComponent<Image>().enabled = true;
        gameoverPanel.SetActive(false);

        StartStage();
    }

    public void StartStage()
    {
        // Level Generation
        MapManager.instance.Generate();

        // Player Position
        playerPrefab.transform.position = startPos.position;

        // Timer
        GetComponent<Timer>().StartTimer();
    }

    public void ClearStage()
    {
        StageManager.instance.NextStage();
        StartStage();
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

    public void GainCoin(int amount = 1)
    {
        coin += amount;
    }

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
