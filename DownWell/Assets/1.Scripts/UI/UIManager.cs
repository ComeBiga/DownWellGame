using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject charPanel;

    void Start()
    {
        SoundManager.instance.SoundOff();
        startPanel.SetActive(true);
        charPanel.SetActive(false);

        SettingMgr.instance.BgmSlider.value = PlayerPrefs.GetFloat("BgmVolume");
        SettingMgr.instance.effSlider.value = PlayerPrefs.GetFloat("EffectVolume");
    }

    public void startBtn()
    {
        startPanel.SetActive(false);
        charPanel.SetActive(true);
    }
}
