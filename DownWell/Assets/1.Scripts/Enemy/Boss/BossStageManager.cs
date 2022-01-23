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
    public float appearSpeed = 1f;

    bool bossStage = false;
    public bool IsBossStage { get { return bossSpawner.activeSelf; } }

    //Vector3 bossAppearPos;

    private void Start()
    {
        bossObject = StageManager.instance.Current.BossObject;
        bossObject = Instantiate(bossObject, bossSpawner.transform);
        bossObject.transform.localPosition = Vector3.zero;
        bossSpawner.SetActive(false);
    }

    public void StartBossStage()
    {
        //SceneManager.LoadScene(0);
        bossStage = true;
        bossSpawner.SetActive(true);
        //SetBossAppearPos();
        StartCoroutine(AppearAnimation());
        Camera.main.GetComponent<CameraShake>().Shake(.02f, 1f, true);

        MapManager.instance.GenerateInfinity(PlayerManager.instance.player.transform, 10);

        SoundManager.instance.SoundOff();
        SoundManager.instance.PlayBGMSound("BackgroundBoss");
    }

    void SetBossAppearPos()
    {
        //bossAppearPos = boss.transform.position;
    }

    IEnumerator AppearAnimation()
    {
        var moveDistance = 0f;
        var bossAppearPos = bossObject.transform.localPosition;
        var lateBossPos = new Vector3();

        PlayerManager.instance.player.GetComponent<PlayerController>().cantMove = true;

        while(true)
        {
            //Debug.Log(moveDistance);
            if (moveDistance > bossAppearDistance)
                break;

            var deltaMoveY = Time.fixedDeltaTime * appearSpeed;
            //Debug.Log(deltaMoveY);

            bossObject.transform.localPosition = new Vector3(bossObject.transform.localPosition.x,
                bossObject.transform.localPosition.y - deltaMoveY,
                bossObject.transform.localPosition.z);

            moveDistance = bossAppearPos.y - bossObject.transform.localPosition.y;

            yield return new WaitForFixedUpdate();
        }

        PlayerManager.instance.player.GetComponent<PlayerController>().cantMove = false;
        //boss.GetComponent<BossCombat>().active = true;
        bossObject.GetComponent<BossBrain>().Use();
        Camera.main.GetComponent<SmoothFollow>().StartBossCamera();
    }
}
