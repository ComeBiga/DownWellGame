using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMgr : MonoBehaviour
{
    #region Singleton
    public static SettingMgr instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public GameObject setPanel;
    public GameObject exitButton;
    public GameObject ingameSetting;

    [Space(10f)]
    public Image pauseImg;

    void Start()
    {
        setPanel.SetActive(false);
    }

    //setting
    public void SettingBtn()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            pauseImg.enabled = true;
            exitButton.SetActive(false);
            ingameSetting.SetActive(true);
        }
        else
        {
            pauseImg.enabled = false;
            exitButton.SetActive(true);
            ingameSetting.SetActive(false);
        }
        setPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void CancelBtn()
    {
        Time.timeScale = 1;
        setPanel.SetActive(false);
    }

    public void homeBtn()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }
    public void exitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        /*#else
                Application.Quit();*/
#endif
    }
}
