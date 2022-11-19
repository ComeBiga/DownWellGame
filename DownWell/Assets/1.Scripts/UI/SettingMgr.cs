using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMgr : MonoBehaviour
{
    public static SettingMgr instance = null;

    public bool buttonOnStart = false;

    public GameObject setPanel;
    public GameObject setBtn;

    public bool Opening = true;


    [Header("Image")]
    [UnityEngine.Serialization.FormerlySerializedAs("spriteOn")]
    public Sprite spriteOn;
    [UnityEngine.Serialization.FormerlySerializedAs("spriteOff")]
    public Sprite spriteOff;
    [UnityEngine.Serialization.FormerlySerializedAs("bgmImg")]
    public Image imgBGM;
    [UnityEngine.Serialization.FormerlySerializedAs("effImg")]
    public Image imgSFX;

    public bool gPaused;
    int ClickCount = 0;

    public Canvas Setting;

    [HideInInspector]
    public int bgmOff;
    [HideInInspector]
    public int effOff;

    private Comebiga.SoundManager soundManager;

    [SerializeField]
    private Button btnBGM;
    [SerializeField]
    private Button btnSFX;

    private Comebiga.AudioState stateBGM;
    private Comebiga.AudioState stateSFX;

    private const string KEY_STATE_BGM = "stateBGM";
    private const string KEY_STATE_SFX = "stateSFX";

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

        btnBGM.onClick.AddListener(() =>
        {
            SwitchBgmState();
        });

        btnSFX.onClick.AddListener(() =>
        {
            SwitchSfxState();
        });

    }

    void Start()
    {
        soundManager = Comebiga.SoundManager.instance;

        setPanel.SetActive(false);
        if (buttonOnStart) setBtn.SetActive(true);

        InitSoundButton();
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
        setPanel.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    public void RestartGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

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
        if (pause)
        {
            gPaused = true;
            if (SceneManager.GetActiveScene().buildIndex == 1) SettingBtn();
        }
        else
        {
            if (gPaused)
            {
                gPaused = false;
            }
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Opening");
    }

    private void InitSoundButton()
    {
        var stateBGM = (Comebiga.AudioState)PlayerPrefs.GetInt(KEY_STATE_BGM);
        SetBgmState(stateBGM);

        var stateSFX = (Comebiga.AudioState)PlayerPrefs.GetInt(KEY_STATE_SFX);
        SetSfxState(stateSFX);
    }

    private void SetBgmState(Comebiga.AudioState state)
    {
        switch(state)
        {
            case Comebiga.AudioState.ON:
                stateBGM = Comebiga.AudioState.ON;
                soundManager.UnmuteBGM();
                imgBGM.sprite = spriteOn;
                PlayerPrefs.SetInt(KEY_STATE_BGM, (int)Comebiga.AudioState.ON);
                break;
            case Comebiga.AudioState.OFF:
                stateBGM = Comebiga.AudioState.OFF;
                soundManager.MuteBGM();
                imgBGM.sprite = spriteOff;
                PlayerPrefs.SetInt(KEY_STATE_BGM, (int)Comebiga.AudioState.OFF);
                break;
        }
    }

    private void SetSfxState(Comebiga.AudioState state)
    {
        switch (state)
        {
            case Comebiga.AudioState.ON:
                stateSFX = Comebiga.AudioState.ON;
                soundManager.UnmuteSFX();
                imgSFX.sprite = spriteOn;
                PlayerPrefs.SetInt(KEY_STATE_SFX, (int)Comebiga.AudioState.ON);
                break;
            case Comebiga.AudioState.OFF:
                stateSFX = Comebiga.AudioState.OFF;
                soundManager.MuteSFX();
                imgSFX.sprite = spriteOff;
                PlayerPrefs.SetInt(KEY_STATE_SFX, (int)Comebiga.AudioState.OFF);
                break;
        }
    }

    private void SwitchBgmState()
    {
        switch (stateBGM)
        {
            case Comebiga.AudioState.ON:
                SetBgmState(Comebiga.AudioState.OFF);
                break;
            case Comebiga.AudioState.OFF:
                SetBgmState(Comebiga.AudioState.ON);
                break;
        }
    }

    private void SwitchSfxState()
    {
        switch (stateSFX)
        {
            case Comebiga.AudioState.ON:
                SetSfxState(Comebiga.AudioState.OFF);
                break;
            case Comebiga.AudioState.OFF:
                SetSfxState(Comebiga.AudioState.ON);
                break;
        }
    }

    //sound
    // public void muteSound(string sound)
    // {
    //     switch (sound)
    //     {
    //         case "bgm":
    //             if (bgmOff == 0)
    //             {
    //                 soundManager?.MuteBGM();
    //                 bgmOff = 1;
    //             }
    //             else
    //             {
    //                 soundManager?.UnmuteBGM();
    //                 bgmOff = 0;
    //             }

    //             // var bgmvalue = (bgmOff == 1) ? true : false;
    //             // if (soundManager != null) soundManager.Mute(Sound.SoundType.BACKGROUND, bgmvalue);

    //             imgBGM.sprite = (bgmOff == 1) ? spriteOff : spriteOn;

    //             PlayerPrefs.SetInt("BgmVolume", bgmOff);
    //             break;
    //         case "eff":
    //             if (effOff == 0)
    //                 effOff = 1;
    //             else
    //                 effOff = 0;
    //             var effvalue = (effOff == 1) ? true : false;
    //             if (soundManager != null) soundManager.Mute(Sound.SoundType.EFFECT, effvalue);

    //             imgSFX.sprite = (effOff == 1) ? spriteOff : spriteOn;

    //             PlayerPrefs.SetInt("EffVolume", effOff);
    //             break;
    //     }
    // }
}
