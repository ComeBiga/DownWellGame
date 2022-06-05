using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [Header("Achievement")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private Text achievementName;
    [SerializeField] private Text achievementDescription;
    [SerializeField] private Image achievementImage;
    [SerializeField] private Queue<IAchievementInfo> _achievedList;

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
}
