using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 startPos;
    public float endPos;
    public Vector3 offset;
    public float smooth = 3f;

    public bool followActive = true;

    public bool followCharacter = true;
    public float scrollSpeed = 3f;

    private float StopFollowYPosition
    {
        get
        {
            return MapManager.instance.CurrentYPos + endPos;
        }
    }

    [Header("Boss Stage")]
    public bool bossScroll = false;
    public float bossScrollDistance;
    private Vector3 bossScrollOffset;
    Vector3 scrollTarget;
    float scrollOffset = 0f;

    private bool closeToUnder = false;
    private bool closeToUpper = false;
    private float bossFollowPosY;


    private BossStageCamera bossCamera;

    [Header("StageEnd")]
    public float offset_End;

    // Start is called before the first frame update
    void Start()
    {
        followActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (bossScroll)
        //    CameraScroll();
    }

    private void FixedUpdate()
    {
        //if (transform.position.y < -MapManager.instance.height + offset_End) followActive = false;
        //else followActive = true;

        if (followActive)
        {
            var lerpedPosY = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth).y;

            if(bossScroll)
            {
                var underPos = BossStageManager.instance.BossObject.transform.localPosition.y + bossScrollDistance;
                var upperPos = BossStageManager.instance.BossObject.GetComponent<Boss>().upperBossObject.transform.position.y - bossScrollDistance;
                lerpedPosY = Mathf.Clamp(lerpedPosY, underPos, upperPos);
            }

            transform.position = new Vector3(transform.position.x, lerpedPosY, transform.position.z);
        }
        else if (bossScroll && !followActive)
        {
            //if (BossStageManager.instance.BossObject != null)
            //{
            //    var bossFollowPos = new Vector3(0, bossFollowPosY, -10);
            //    transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, bossFollowPos, Time.deltaTime * smooth).y, transform.position.z);
            //}
        }


        // 보스 등장 후 카메라 움직임
        if (bossScroll && BossStageManager.instance.BossObject != null)
        {
            //bossCamera.Update();

            //if (followCharacter)
            //    CameraScrollFollowCharacter();      // 캐릭터 따라감
            //else
            //    CameraScrollOnly();                 // 화면만 움직임

            CameraScrollDistanceFromBoss();
        }
            
    }

    public void SetStartPosition()
    {
        transform.position = startPos;

        StartCoroutine(EFollowCharacterAtStageStart());
    }

    private IEnumerator EFollowCharacterAtStageStart()
    {
        target = PlayerManager.instance.playerObject.transform;

        while (true)
        {
            if (PlayerManager.instance.playerObject.transform.position.y <= transform.position.y - 3f)
                break;

            yield return null;
        }

        //InitFollowCamera(PlayerManager.instance.playerObject.transform);

        //transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        followActive = true;
    }

    public void InitFollowCamera(Transform playerPos)
    {
        target = playerPos;
        transform.position = new Vector3(transform.position.x, (target.position + offset).y, (target.position + offset).z);
        followActive = true;
    }

    public void StartStage()
    {
        bossScroll = false;
        followActive = false;

        SetStartPosition();
    }

    public void StageEnd()
    {
        StartCoroutine(EStopFollowCharacterAtStageEnd());
    }

    private IEnumerator EStopFollowCharacterAtStageEnd()
    {
        while(true)
        {
            if (PlayerManager.instance.playerObject.transform.position.y <= StopFollowYPosition)
                break;

            yield return null;
        }

        bossScroll = false;
        followActive = false;
    }

    #region BossStageCamera

    public void StartBossCamera()
    {
        SetCameraScrollTarget();
        bossScroll = true;
        //followActive = false;
    }

    public void EndBossCamera()
    {
        bossScroll = false;
        followActive = true;
    }

    public void SetCameraScrollTarget()
    {
        scrollTarget = new Vector3(0, target.transform.position.y, 0);
    }

    private void CameraScrollDistanceFromBoss()
    {
        var dis = (transform.position.y - bossScrollDistance) - BossStageManager.instance.BossObject.transform.localPosition.y;
        var upperDis = BossStageManager.instance.BossObject.GetComponent<Boss>().upperBossObject.transform.position.y - (transform.position.y + bossScrollDistance);

        //Debug.Log("dis : " + dis);
        //Debug.Log("upperDis : " + upperDis);
        //Debug.Log(target.transform.position.y >= transform.position.y);

        var underPos = BossStageManager.instance.BossObject.transform.localPosition.y + bossScrollDistance;
        var upperPos = BossStageManager.instance.BossObject.GetComponent<Boss>().upperBossObject.transform.position.y - bossScrollDistance;

        //if(target.position.y >= underPos && target.position.y<= upperPos)
        //{
        //    followActive = true;
        //}
        //else if(target.position.y < underPos)
        //{
        //    followActive = false;
        //    bossFollowPosY = BossStageManager.instance.BossObject.transform.localPosition.y + bossScrollDistance;
        //}
        //else
        //{
        //    followActive = false;
        //    bossFollowPosY = BossStageManager.instance.BossObject.GetComponent<Boss>().upperBossObject.transform.position.y - bossScrollDistance;
        //}

        //if (target.transform.position.y >= transform.position.y && closeToUnder)
        //{
        //    transform.position = new Vector3(0, target.transform.position.y, -10);
        //    followActive = true;
        //    closeToUnder = false;
        //}

        //if (dis <= 0)
        //{
        //    followActive = false;
        //    closeToUnder = true;
        //}

        //if (upperDis <= 0)
        //{
        //    followActive = false;
        //    closeToUpper = true;
        //}

        //if (target.transform.position.y <= transform.position.y && closeToUpper)
        //{
        //    transform.position = new Vector3(0, target.transform.position.y, -10);
        //    followActive = true;
        //    closeToUpper = false;
        //}
    }

    void CameraScrollOnly()
    {
        var Yscroll = Vector3.down * scrollSpeed * Time.fixedDeltaTime;
        scrollTarget += Yscroll;

        LerpToTarget(scrollTarget + offset);
        //transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, scrollTarget + offset, Time.fixedDeltaTime * smooth).y, transform.position.z);
    }

    public void CameraScrollFollowCharacter()
    {
        //var Yfollow = Vector3.down * (transform.position.y - Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth).y);
        //Yfollow = (transform.position.y > target.position.y) ? Yfollow : Vector3.zero;

        //Debug.Log(Yfollow);

        //transform.position += Yscroll;
        //transform.position += Yfollow;

        if (scrollTarget.y > target.position.y)                             // Follow Charact
        {
            LerpToTarget(target.position + offset);

            //Debug.Log(target.GetComponent<PlayerController>().Grounded);

            if (target.GetComponent<PlayerPhysics>().Grounded)
            {
                SetCameraScrollTarget();
            }
        }
        else
        {
            var Yscroll = Vector3.down * scrollSpeed * Time.fixedDeltaTime;
            scrollTarget += Yscroll;

            LerpToTarget(scrollTarget + offset);
        }
    }

    private void LerpToTarget(Vector3 to)
    {
        // Lerp
        var yDelta = Vector3.Lerp(transform.position,
                                      to,
                                      Time.fixedDeltaTime * smooth).y;

        // translate
        transform.position = new Vector3(transform.position.x,
                                         yDelta,
                                         transform.position.z);
    }

    #endregion
}
