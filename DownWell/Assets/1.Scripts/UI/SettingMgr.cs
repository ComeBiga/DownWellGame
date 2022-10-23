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
            Destroy(this.gameObject);
        }
    }
    #endregion

    public bool buttonOnStart = false;

    public GameObject setPanel;
    public GameObject setBtn;

    public bool Opening = true;


    [Header("Image")]
    public Sprite muteImg;
    public Sprite originImg;
    public Image bgmImg;
    public Image effImg;

    public bool gPaused;
    int ClickCount = 0;

    public Canvas Setting;

    [HideInInspector]
    public int bgmOff;
    [HideInInspector]
    public int effOff;

    void Start()
    {
        setPanel.SetActive(false);
        if (buttonOnStart) setBtn.SetActive(true);
    }

    void Update()
    {
        if (!setPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SettingBtn();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SettingBtn();
            }
        }

        if (Setting.worldCamera == null)
            Setting.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // if (bgmOff == 1)
        //     bgmImg.sprite = muteImg;
        // else
        //     bgmImg.sprite = originImg;

        // if (effOff == 1)
        //     effImg.sprite = muteImg;
        // else
        //     effImg.sprite = originImg;
    }

    public void ResetCollectionData()
    {
        StartSceneManager.Instance.ResetCollectionData();
    }

    void DoubleClick()
    {
        ClickCount = 0;
    }

    public void SetActiveSettingButton(bool value)
    {
        setBtn.SetActive(value);
    }

    public void ActivateSettingButton(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            setBtn.SetActive(true);
    }

    //setting
    public void SettingBtn()
    {
        gPaused = true;
        setPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void CancelBtn()
    {
        Time.timeScale = 1;
        gPaused = false;
        setPanel.SetActive(false);
    }
    public void homeBtn()
    {
        Time.timeScale = 1;
        //PlayerManager.instance.DestoryPlayerObject();
        setPanel.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public void RestartGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    /*    private void OnApplicationQuit()
        {
            Application.CancelQuit();
    #if !UNITY_EDITOR
            System.Diagnostics.Process.GetCurrentProcess().Kill();
    #endif
        }*/

    public void exitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnApplicationPause(bool pause)
    {
        if (pause) //������ Ȩ�̳� Ȧ���ư ������ �� �Ͻ�����
        {
            gPaused = true;
            if (SceneManager.GetActiveScene().buildIndex == 1) SettingBtn();
        }
        else
        {
            if (gPaused) //������ �������� ���ƿ��� ��
            {
                gPaused = false;
            }
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Opening");
    }

    //sound
    public void muteSound(string sound)
    {
        switch (sound)
        {
            case "bgm":
                if (bgmOff == 0)
                    bgmOff = 1;
                else
                    bgmOff = 0;
                //SoundManager.instance.SetBgmVolume(bgmOff);
                var bgmvalue = (bgmOff == 1) ? true : false;
                if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Mute(Sound.SoundType.BACKGROUND, bgmvalue);

                bgmImg.sprite = (bgmOff == 1) ? muteImg : originImg;

                PlayerPrefs.SetInt("BgmVolume", bgmOff);
                break;
            case "eff":
                if (effOff == 0)
                    effOff = 1;
                else
                    effOff = 0;
                //SoundManager.instance.SetEffVolume(effOff);
                var effvalue = (effOff == 1) ? true : false;
                if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Mute(Sound.SoundType.EFFECT, effvalue);

                effImg.sprite = (effOff == 1) ? muteImg : originImg;

                PlayerPrefs.SetInt("EffVolume", effOff);
                break;
        }
    }
}
