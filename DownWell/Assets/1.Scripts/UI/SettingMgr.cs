using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMgr : MonoBehaviour
{
    public static SettingMgr instance = null;

    public GameObject setPanel;

    [Header("Image")]
    [UnityEngine.Serialization.FormerlySerializedAs("spriteOn")]
    public Sprite spriteOn;
    [UnityEngine.Serialization.FormerlySerializedAs("spriteOff")]
    public Sprite spriteOff;
    [UnityEngine.Serialization.FormerlySerializedAs("bgmImg")]
    public Image imgBGM;
    [UnityEngine.Serialization.FormerlySerializedAs("effImg")]
    public Image imgSFX;

    public Canvas Setting;

    private Comebiga.SoundManager soundManager;

    [SerializeField]
    private Button btnBGM;
    [SerializeField]
    private Button btnSFX;
    [SerializeField]
    private Button btnResume;
    [SerializeField]
    private Button btnHome;

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

        btnResume.onClick.AddListener(() =>
        {
            CancelBtn();
        });

        btnHome.onClick.AddListener(() =>
        {
            homeBtn();
        });
    }

    void Start()
    {
        soundManager = Comebiga.SoundManager.instance;

        setPanel.SetActive(false);

        InitSoundButton();
    }

    void Update()
    {
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

    //setting
    public void SettingBtn()
    {
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
        Time.timeScale = 1;
        setPanel.SetActive(false);
        soundManager.StopAll();
        SceneManager.LoadScene(0);
    }

    public void RestartGameScene()
    {
        SceneManager.LoadScene(1);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1) SettingBtn();
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
}
