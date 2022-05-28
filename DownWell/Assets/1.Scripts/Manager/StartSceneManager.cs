
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : Singleton<StartSceneManager>
{
    [Header("UI")]
    public GameObject openingPanel;
    public GameObject startPanel;
    public GameObject charPanel;
    public Text versionInfo;

    // Start is called before the first frame update
    void Start()
    {
        InitPanel();
    }

    public void InitPanel()
    {
        if (PlayerPrefs.GetInt("Opening") == 0)
        {
            openingPanel.SetActive(true);
            startPanel.SetActive(false);
            charPanel.SetActive(false);
        }
        else
        {
            openingPanel.SetActive(false);
            startPanel.SetActive(true);
            charPanel.SetActive(false);
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
    }

    public void LoadGameScene()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(1);
    }
}
