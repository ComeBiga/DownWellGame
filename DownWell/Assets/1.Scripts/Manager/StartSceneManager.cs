
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : Singleton<StartSceneManager>
{
    [Header("UI")]
    public GameObject UIs;
    public bool viewOpening = true;
    public GameObject openingPanel;
    public GameObject startPanel;
    public GameObject charPanel;
    public GameObject settingButton;
    public Text versionInfo;
    public Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(() => 
        {
            LoadGameScene();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerManager.instance.Init();

        InitPanel();

        InitSound();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCollectionData();
        }
    }

    public void ResetCollectionData()
    {
        AchievementSystem.Instance.ResetAllAchievements();
        UICharacterCollection.Instance.ResetCharacterProfiles();
        UIs.GetComponentInChildren<UICoinPanel>().Init();
        UIs.GetComponentInChildren<UICharacterSelection>().UpdateButton();
    }

    public void InitPanel()
    {
        if (SettingMgr.instance.Opening && viewOpening)
        {
            openingPanel.SetActive(true);
            startPanel.SetActive(false);
            charPanel.SetActive(false);
            //settingButton.SetActive(false);
            SettingMgr.instance.SetActiveSettingButton(false);

            SettingMgr.instance.Opening = false;
        }
        else
        {
            openingPanel.SetActive(false);
            startPanel.SetActive(true);
            charPanel.SetActive(false);
            //settingButton.SetActive(false);
            SettingMgr.instance.SetActiveSettingButton(false);
        }

        versionInfo.text = "Ver." + Application.version;
    }

    public void InitSound()
    {
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Stop(Sound.SoundType.BACKGROUND);

        SettingMgr.instance.bgmOff = PlayerPrefs.GetInt("BgmVolume");
        SettingMgr.instance.effOff = PlayerPrefs.GetInt("EffVolume");

        var bgmvalue = (SettingMgr.instance.bgmOff == 1) ? true : false;
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Mute(Sound.SoundType.BACKGROUND, bgmvalue);

        //SoundManager.instance.SetEffVolume(SettingMgr.instance.effOff);
        var effvalue = (SettingMgr.instance.effOff == 1) ? true : false;
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Mute(Sound.SoundType.EFFECT, effvalue);
    }

    public void OpenCharPanel()
    {
        startPanel.SetActive(false);
        charPanel.SetActive(true);
        //settingButton.SetActive(true);
        SettingMgr.instance.SetActiveSettingButton(true);

    }

    public void LoadGameScene()
    {
        SettingMgr.instance.SetActiveSettingButton(false);
        var operation = SceneManager.LoadSceneAsync(1);
    }
}
