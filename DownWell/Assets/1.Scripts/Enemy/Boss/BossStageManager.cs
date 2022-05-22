using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : MonoBehaviour
{
    #region Singleton
    public static BossStageManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public GameObject bossSpawner;
    private GameObject bossObject;

    public float bossAppearDistance;
    public int appearDirection = -1;
    public float appearSpeed = 1f;
    [SerializeField] private float delayAfterAppear = 2f;

    [SerializeField] private float posOffset = -3f;

    private bool bossStage = false;
    public bool IsBossStage { get { return bossStage; } }

    public GameObject BossObject { get { return bossObject; } }

    //Vector3 bossAppearPos;
    public float bugTime = 0f;

    private void Init()
    {
        bossObject = StageManager.instance.Current.BossObject;

        if(bossObject != null)
        {
            bossObject = Instantiate(bossObject, bossSpawner.transform);
            bossObject.transform.localPosition = new Vector3(0, Camera.main.transform.position.y + posOffset, 0);//Vector3.zero;
        }

        bossSpawner.SetActive(false);
    }

    public void StartBossStage()
    {
        Init();
        //SceneManager.LoadScene(0);
        bossStage = true;
        bossSpawner.SetActive(true);
        //SetBossAppearPos();
        StartCoroutine(AppearAnimation());
        Camera.main.GetComponent<CameraShake>().Shake(.02f, 1f, true);

        // Player
        PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().CheckOutOfScreen();

        // Map Generation
        //MapManager.instance.GenerateInfinity(PlayerManager.instance.playerObject.transform, 3);
        MapManager.instance.GenerateBossLevels(PlayerManager.instance.playerObject.transform, 3);

        FX();
    }

    public void EndBossStage()
    {
        bossStage = false;

        // Camera
        Camera.main.GetComponent<SmoothFollow>().EndBossCamera();
        
        // Player
        PlayerManager.instance.playerObject.GetComponent<PlayerCombat>().StopCheckOutOfScreen();

        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("Background");

        //GameManager.instance.ClearStage();
        MapManager.instance.StopGenerateInfinity();
        MapManager.instance.GenerateStageEnd();

        Debug.Log("Clear Boss");
    }

    IEnumerator AppearAnimation()
    {
        var moveDistance = 0f;
        var bossAppearPos = bossObject.transform.localPosition;
        var lateBossPos = new Vector3();

        PlayerManager.instance.playerObject.GetComponent<PlayerController>().cantMove = true;

        while(true)
        {
            //Debug.Log(moveDistance);
            if (moveDistance > bossAppearDistance)
                break;

            var deltaMoveY = Time.fixedDeltaTime * appearSpeed * appearDirection;
            //Debug.Log(deltaMoveY);

            bossObject.transform.localPosition = new Vector3(bossObject.transform.localPosition.x,
                bossObject.transform.localPosition.y - deltaMoveY,
                bossObject.transform.localPosition.z);

            moveDistance = Mathf.Abs(bossAppearPos.y - bossObject.transform.localPosition.y);

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(delayAfterAppear);

        PlayerManager.instance.playerObject.GetComponent<PlayerController>().cantMove = false;
        //boss.GetComponent<BossCombat>().active = true;
        bossObject.GetComponent<Boss>().upperBossObject.GetComponent<SpriteRenderer>().color = Color.white;
        bossObject.GetComponent<BossBrain>().Use();
        bossObject.GetComponent<BossMovement>().Move();

        Camera.main.GetComponent<SmoothFollow>().StartBossCamera();
    }

    private void FX()
    {
        //SoundManager.instance.SoundOff();
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Stop(Sound.SoundType.BACKGROUND);

        //SoundManager.instance.PlayBGMSound("BackgroundBoss");
        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Play("BackgroundBoss");
    }
}
