using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth = 3f;

    public bool followActive = true;

    public bool followCharacter = true;
    public float scrollSpeed = 3f;
    public bool bossScroll = false;
    public float bossScrollDistance;
    private Vector3 bossScrollOffset;
    Vector3 scrollTarget;
    float scrollOffset = 0f;

    private BossStageCamera bossCamera;

    [Header("StageEnd")]
    public float offset_End;

    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth).y, transform.position.z);
        else
            transform.position = new Vector3(0, BossStageManager.instance.BossObject.transform.localPosition.y + bossScrollDistance, -10);


        // 보스 등장 후 카메라 움직임
        if (bossScroll)
        {
            //bossCamera.Update();

            //if (followCharacter)
            //    CameraScrollFollowCharacter();      // 캐릭터 따라감
            //else
            //    CameraScrollOnly();                 // 화면만 움직임

            CameraScrollDistanceFromBoss();
        }
            
    }

    public void InitFollowCamera(Transform playerPos)
    {
        target = playerPos;
        transform.position = new Vector3(transform.position.x, (target.position + offset).y, (target.position + offset).z);
    }

    public void StartStage()
    {
        bossScroll = false;
        followActive = true;
    }

    #region BossStageCamera

    public void StartBossCamera()
    {
        SetCameraScrollTarget();
        bossScroll = true;
        //followActive = false;
    }

    public void SetCameraScrollTarget()
    {
        scrollTarget = new Vector3(0, target.transform.position.y, 0);
    }

    private void CameraScrollDistanceFromBoss()
    {
        var dis = (transform.position.y - bossScrollDistance) - BossStageManager.instance.BossObject.transform.localPosition.y;

        Debug.Log("dis : " + dis);
        Debug.Log(target.transform.position.y >= transform.position.y);

        if(target.transform.position.y >= transform.position.y)
        {
            transform.position = new Vector3(0, target.transform.position.y, -10);
            followActive = true;
        }

        if(dis <= 0)
        {
            followActive = false;
        }
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
