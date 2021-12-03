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

    public GameObject boss;

    public float bossAppearDistance;
    public float appearSpeed = 1f;

    //Vector3 bossAppearPos;

    private void Start()
    {
        
    }

    public void StartBossStage()
    {
        //SceneManager.LoadScene(0);
        boss.SetActive(true);
        //SetBossAppearPos();
        StartCoroutine(AppearAnimation());
        Camera.main.GetComponent<CameraShake>().Shake(.02f, 1f, true);

        MapManager.instance.GenerateInfinity(PlayerManager.instance.player.transform, 10);
    }

    void SetBossAppearPos()
    {
        //bossAppearPos = boss.transform.position;
    }

    IEnumerator AppearAnimation()
    {
        var moveDistance = 0f;
        var bossAppearPos = boss.transform.localPosition;
        var lateBossPos = new Vector3();

        PlayerManager.instance.player.GetComponent<PlayerController>().cantMove = true;

        while(true)
        {
            //Debug.Log(moveDistance);
            if (moveDistance > bossAppearDistance)
                break;

            var deltaMoveY = Time.fixedDeltaTime * appearSpeed;
            //Debug.Log(deltaMoveY);

            boss.transform.localPosition = new Vector3(boss.transform.localPosition.x,
                boss.transform.localPosition.y - deltaMoveY,
                boss.transform.localPosition.z);

            moveDistance = bossAppearPos.y - boss.transform.localPosition.y;

            yield return new WaitForFixedUpdate();
        }

        PlayerManager.instance.player.GetComponent<PlayerController>().cantMove = false;
        boss.GetComponent<BossCombat>().active = true;
        Camera.main.GetComponent<SmoothFollow>().StartBossCamera();
    }
}
