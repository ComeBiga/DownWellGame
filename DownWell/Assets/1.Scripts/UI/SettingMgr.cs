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
    public GameObject setBtn;
    public GameObject exitButton;
    public GameObject ingameSetting;

    [Header("Slider")]
    public Slider BgmSlider;
    public Slider effSlider;


    [Header("Image")]
    public Sprite muteImg;
    public Sprite originImg;
    public Image bgmImg;
    public Image effImg;

    public bool gPaused;
    int ClickCount = 0;

    public Canvas Setting;

    void Start()
    {
        setPanel.SetActive(false);
        setBtn.SetActive(true);
    }

    void Update()
    {
        if (Setting.worldCamera == null)
            Setting.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (BgmSlider.value == 0)
            bgmImg.sprite = muteImg;
        else
            bgmImg.sprite = originImg;
        
        if (effSlider.value == 0)
            effImg.sprite = muteImg;
        else
            effImg.sprite = originImg;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBtn();
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            exitBtn();
        }

    }

    void DoubleClick()
    {
        ClickCount = 0;
    }

    //setting
    public void SettingBtn()
    {
        gPaused = true;
        if ((SceneManager.GetActiveScene().buildIndex == 1) || UIManager.clickPlay)
        {
            exitButton.SetActive(false);
            ingameSetting.SetActive(true);
        }
        else
        {
            exitButton.SetActive(true);
            ingameSetting.SetActive(false);
        }
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
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
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
        if (pause) //유저가 홈이나 홀드버튼 눌렸을 때 일시정지
        {
            gPaused = true;
            SettingBtn();
        }
        else
        {
            if (gPaused) //유저가 게임으로 돌아왔을 때
            {
                gPaused = false;
            }
        }
    }


    //sound
    public void CheckBgmSlider(float value)
    {
        value = BgmSlider.value;
        SoundManager.instance.SetBgmVolume(value);
        PlayerPrefs.SetFloat("BgmVolume", value);
    }

    public void CheckEffectSlider(float value)
    {
        value = effSlider.value;
        SoundManager.instance.SetEffVolume(value);
        PlayerPrefs.SetFloat("EffectVolume", value);
    }

    public void muteSound(string sound)
    {
        switch (sound)
        {
            case "bgm":
                BgmSlider.value = 0;
                break;
            case "eff":
                effSlider.value = 0;
                break;
        }
    }
}
