using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public float activeRangeOffset = 0;

    public int coin = 0;

    public GameObject boss;

    private void Start()
    {
        PlayerManager.instance.selectedCharacter.InitPlayerValues(playerPrefab);

        playerPrefab = Instantiate(playerPrefab, startPos.position, Quaternion.identity);
        //playerPrefab.SetActive(false);

        Camera.main.GetComponent<SmoothFollow>().InitFollowCamera(playerPrefab.transform);

        PlayerManager.instance.player = playerPrefab;

        GetComponent<Timer>().StartTimer();
    }

    public bool CheckTargetRange(Transform enemy)
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * (9 / 16);

        float h_tarTothis = Mathf.Abs(playerPrefab.transform.position.y - enemy.position.y);

        if (height / 2 + activeRangeOffset < enemy.position.y - playerPrefab.transform.position.y)
            Destroy(enemy.gameObject);

        if (h_tarTothis < height / 2 + activeRangeOffset)
            return true;

        return false;
    }

    public void GainCoin(int amount = 1)
    {
        coin += amount;
    }

    public void StageEnd()
    {
        //SceneManager.LoadScene(0);
        boss.SetActive(true);
        Camera.main.GetComponent<SmoothFollow>().StartBossCamera();
        MapManager.instance.GenerateInfinity(PlayerManager.instance.player.transform, 10);
    }
}
