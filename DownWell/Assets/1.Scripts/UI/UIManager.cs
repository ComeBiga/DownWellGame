using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject openingPanel;
    public GameObject startPanel;
    public GameObject charPanel;
    public GameObject character;

    public Text versionInfo;

    public static bool clickPlay;

    void Start()
    {
        //Screen.SetResolution(Screen.height * (9 / 16), Screen.height, true);
        //PlayerPrefs.DeleteAll();
        
        //SoundManager.instance.SoundOff();
        
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Stop(Sound.SoundType.BACKGROUND);

        if (PlayerPrefs.GetInt("Opening") != 0)
            openingPanel.SetActive(false);
        else
            openingPanel.SetActive(true);

        startPanel.SetActive(true);
        charPanel.SetActive(false);

        clickPlay = false;

        PlayerPrefs.SetInt("Opening", 1);

        SettingMgr.instance.bgmOff = PlayerPrefs.GetInt("BgmVolume");
        SettingMgr.instance.effOff = PlayerPrefs.GetInt("EffVolume");

        //SoundManager.instance.SetBgmVolume(SettingMgr.instance.bgmOff);
        var bgmvalue = (SettingMgr.instance.bgmOff == 1) ? true : false;
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Mute(Sound.SoundType.BACKGROUND, bgmvalue);

        //SoundManager.instance.SetEffVolume(SettingMgr.instance.effOff);
        var effvalue = (SettingMgr.instance.effOff == 1) ? true : false;
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Mute(Sound.SoundType.EFFECT, effvalue);

        versionInfo.text = "Ver." + Application.version;
    }

    void Update()
    {
        if (openingPanel.activeInHierarchy)
            if (openingPanel.GetComponent<Image>().enabled == false)
                openingPanel.SetActive(false);
    }
    public void startBtn()
    {
        clickPlay = true;
        startPanel.SetActive(false);
        charPanel.SetActive(true);
    }
}
