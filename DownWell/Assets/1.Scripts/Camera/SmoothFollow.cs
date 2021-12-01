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
    Vector3 scrollTarget;
    float scrollOffset = 0f;

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

        if(followActive)
         transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth).y, transform.position.z);

        if (bossScroll)
        {
            if (followCharacter)
                CameraScrollFollowCharacter();
            else
                CameraScrollOnly();
        }
            
    }

    public void InitFollowCamera(Transform playerPos)
    {
        target = playerPos;
        transform.position = new Vector3(transform.position.x, (target.position + offset).y, (target.position + offset).z);
    }

    #region BossStageCamera

    public void StartBossCamera()
    {
        SetCameraScrollTarget();
        bossScroll = true;
        followActive = false;
    }

    public void SetCameraScrollTarget()
    {
        scrollTarget = new Vector3(0, target.transform.position.y, 0);
    }

    void CameraScrollOnly()
    {
        var Yscroll = Vector3.down * scrollSpeed * Time.fixedDeltaTime;
        scrollTarget += Yscroll;

        transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, scrollTarget + offset, Time.fixedDeltaTime * smooth).y, transform.position.z);
    }

    public void CameraScrollFollowCharacter()
    {
        //var Yfollow = Vector3.down * (transform.position.y - Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth).y);
        //Yfollow = (transform.position.y > target.position.y) ? Yfollow : Vector3.zero;

        //Debug.Log(Yfollow);

        //transform.position += Yscroll;
        //transform.position += Yfollow;

        if (scrollTarget.y > target.position.y)
        {
            transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * smooth).y, transform.position.z);

            //Debug.Log(target.GetComponent<PlayerController>().Grounded);

            if (target.GetComponent<PlayerController>().Grounded)
            {
                SetCameraScrollTarget();
            }
        }
        else
        {
            var Yscroll = Vector3.down * scrollSpeed * Time.fixedDeltaTime;
            scrollTarget += Yscroll;

            transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, scrollTarget + offset, Time.fixedDeltaTime * smooth).y, transform.position.z);
        }
    }

    #endregion
}
