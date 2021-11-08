using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public bool mouseClick = false;
    public bool blockInput = false;
    public float horizontal = 0;
    public float sens = 10f;
    public float dead = .001f;

    public GameObject controllerPanel;

    void Start()
    {
#if UNITY_EDITOR
        controllerPanel.SetActive(false);
#elif UNITY_ANDROID
        controllerPanel.SetActive(true);  
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if(!blockInput)
            MobileTouch();
    }

    void MobileTouch()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton(0) && Input.mousePosition.x < Camera.main.pixelWidth / 2)
        {
            if (Input.mousePosition.x > 0 && Input.mousePosition.x <= Camera.main.pixelWidth / 4)
            {
                //Debug.Log("left");
                horizontal = (horizontal > 0) ? 0 : Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
            }
            else if (Input.mousePosition.x > Camera.main.pixelWidth / 4 && Input.mousePosition.x < Camera.main.pixelWidth / 2)
            {
                //Debug.Log("right");
                horizontal = (horizontal < 0) ? 0 : Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
            }
        }
        else
        {
            horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
        }
#endif
#if UNITY_ANDROID
        //if (Input.touchCount > 0)
        //{
        //    Debug.Log("touched");
        //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //        return;

        //    foreach(var touch in Input.touches)
        //    {
        //        if (touch.position.x > 0 && touch.position.x <= Camera.main.pixelWidth / 4)
        //        {
        //            horizontal = Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
        //        }
        //        else if(touch.position.x > Camera.main.pixelWidth / 4 && touch.position.x < Camera.main.pixelWidth / 2)
        //        {
        //            horizontal = Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
        //        }
        //    }
        //}
        if (Input.touchCount > 0)
        {
            var moveTouchCount = 0;
            foreach (var touch in Input.touches)
            {
                if (touch.position.x < Camera.main.pixelWidth / 2)
                {
                    moveTouchCount++;

                    if (touch.position.x > 0 && touch.position.x <= Camera.main.pixelWidth / 4)
                    {
                        Debug.Log("left");
                        //horizontal = Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                        horizontal = (horizontal > 0) ? 0 : Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
                        //horizontal = -1;
                    }
                    else if (touch.position.x > Camera.main.pixelWidth / 4 && touch.position.x < Camera.main.pixelWidth / 2)
                    {
                        Debug.Log("right");
                        //horizontal = Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                        horizontal = (horizontal < 0) ? 0 : Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
                        //horizontal = 1;
                    }
                }
            }

            // 터치는 있지만, 방향키가 눌리지 않았을 때
            if (moveTouchCount <= 0)
            {
                horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
                //horizontal = 0;
            }
        }
        else
        {
            horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
            //horizontal = 0;
        }
#endif
    }

    public bool GetJumpButtonDown()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.position.x > Camera.main.pixelWidth / 2 && (touch.phase == TouchPhase.Began))
                {
                    return true;
                }
            }
        }
        return false;
        //return Input.GetMouseButtonDown(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#endif
    }

    public bool GetJumpButton()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#endif
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            foreach(var touch in Input.touches)
            {
                if(touch.position.x > Camera.main.pixelWidth / 2 && (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved))
                {
                    return true;
                }
            }
        }
        return false;
        //return Input.GetMouseButton(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#endif
    }

    public bool GetJumpButtonUp()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonUp(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.position.x > Camera.main.pixelWidth / 2 && (touch.phase == TouchPhase.Ended))
                {
                    return true;
                }
            }
        }
        return false;
        //return Input.GetMouseButtonUp(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#endif
    }
}
