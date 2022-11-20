
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartSceneManager : Singleton<StartSceneManager>
{
    [Header("UI")]
    // public GameObject UIs;
    public bool viewOpening = true;
    public GameObject openingPanel;
    public GameObject startPanel;
    public Button startButton;
    // [System.Obsolete]
    // public GameObject charPanel;
    //public GameObject settingButton;
    public Text versionInfo;
    public TextMeshProUGUI versionInfoTMP;


    private void Awake()
    {
        startButton.onClick.AddListener(() => 
        {
            LoadGameScene();
        });
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitPanel();
    }

    // public void ResetCollectionData()
    // {
    //     AchievementSystem.Instance.ResetAllAchievements();
    //     UICharacterCollection.Instance.ResetCharacterProfiles();
    //     UIs.GetComponentInChildren<UICoinPanel>().Init();
    //     UIs.GetComponentInChildren<UICharacterSelection>().UpdateButton();
    // }

    public void InitPanel()
    {
        if (UserData.showOpening && viewOpening)
        {
            openingPanel.SetActive(true);
            startPanel.SetActive(false);

            //UserData.showOpening = false;
        }
        else
        {
            openingPanel.SetActive(false);
            startPanel.SetActive(true);
        }

        versionInfo.text = "Ver." + Application.version;
        versionInfoTMP.text = "Ver." + Application.version;
    }

    public void OpenCharPanel()
    {
        startPanel.SetActive(false);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
