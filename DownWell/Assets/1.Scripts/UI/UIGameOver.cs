using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGameOverTitle;
    [SerializeField] private Button btnHome;

    [Header("Achievement")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private Text achievementName;
    [SerializeField] private Text achievementDescription;
    [SerializeField] private Image achievementImage;
    [SerializeField] private Queue<IAchievementInfo> _achievedList;
    [SerializeField] private Button btnRetry;

    private void Awake()
    {
        btnRetry.onClick.AddListener(() => {
            SettingMgr.instance.RestartGameScene();
        });

        btnHome.onClick.AddListener(() => {
            SceneManager.LoadSceneAsync(0);
        });
    }

    public void TurnOnAchievementPanel(Queue<IAchievementInfo> achievedList)
    {
        if (achievedList.Count > 0)
            achievementPanel.SetActive(true);
        else
        {
            achievementPanel.SetActive(false);
            return;
        }

        _achievedList = achievedList;

        SetAchievementPanel();
    }

    public void SetAchievementPanel()
    {
        var achieved = _achievedList.Dequeue();

        achievementName.text = achieved.Name;
        achievementDescription.text = achieved.Description;
        achievementImage.sprite = achieved.Image;
    }

    public void GameClear()
    {
        txtGameOverTitle.text = "Game Clear";

        btnHome.gameObject.SetActive(true);
    }
}
