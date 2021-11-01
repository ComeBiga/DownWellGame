using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject charPanel;

    public Text versionInfo;

    public static bool clickPlay;

    void Start()
    {
<<<<<<< HEAD
        //Screen.SetResolution(Screen.height * (9 / 16), Screen.height, true);
=======
<<<<<<< Updated upstream
        Screen.SetResolution(Screen.height * (9 / 16), Screen.height, true);
>>>>>>> ede2731 (SoundSlider delete & UI set)

=======
        //Screen.SetResolution(Screen.height * (9 / 16), Screen.height, true);
        //PlayerPrefs.DeleteAll();
        
>>>>>>> Stashed changes
        SoundManager.instance.SoundOff();
        startPanel.SetActive(true);
        charPanel.SetActive(false);

        clickPlay = false;

        SettingMgr.instance.bgmOff = PlayerPrefs.GetInt("BgmVolume");
        SettingMgr.instance.effOff = PlayerPrefs.GetInt("EffVolume");

        SoundManager.instance.SetBgmVolume(SettingMgr.instance.bgmOff);
        SoundManager.instance.SetEffVolume(SettingMgr.instance.effOff);

        versionInfo.text = "Ver." + Application.version;
    }

    public void startBtn()
    {
        clickPlay = true;
        startPanel.SetActive(false);
        charPanel.SetActive(true);
    }
}
