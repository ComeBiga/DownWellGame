using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        PlayerManager.instance.selectedCharacter.InitPlayerValues(playerPrefab);

        playerPrefab = Instantiate(playerPrefab, startPos.position, Quaternion.identity);

        Camera.main.GetComponent<SmoothFollow>().InitFollowCamera(playerPrefab.transform);

        PlayerManager.instance.player = playerPrefab;
    }
}
