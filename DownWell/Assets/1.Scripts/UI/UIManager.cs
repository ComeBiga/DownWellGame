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
        Screen.SetResolution(Screen.height * (10 / 16), Screen.height, true);

        SoundManager.instance.SoundOff();
        startPanel.SetActive(true);
        charPanel.SetActive(false);

        clickPlay = false;

        SettingMgr.instance.BgmSlider.value = PlayerPrefs.GetFloat("BgmVolume");
        SettingMgr.instance.effSlider.value = PlayerPrefs.GetFloat("EffectVolume");

        versionInfo.text = "Ver." + Application.version;
    }

    public void startBtn()
    {
        clickPlay = true;
        startPanel.SetActive(false);
        charPanel.SetActive(true);
    }
}
