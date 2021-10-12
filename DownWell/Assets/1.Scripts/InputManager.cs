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
    public float horizontal = 0;
    public float sens = 10f;
    public float dead = .001f;

    // Update is called once per frame
    void Update()
    {
        MobileTouch();
    }

    void MobileTouch()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton(0) && Input.mousePosition.x < Camera.main.pixelWidth / 2)
        {
            if (Input.mousePosition.x > 0 && Input.mousePosition.x <= Camera.main.pixelWidth / 4)
            {
                Debug.Log("left");
                horizontal = Mathf.MoveTowards(horizontal, -1, sens * Time.deltaTime);
            }
            else if (Input.mousePosition.x > Camera.main.pixelWidth / 4 && Input.mousePosition.x < Camera.main.pixelWidth / 2)
            {
                Debug.Log("right");
                horizontal = Mathf.MoveTowards(horizontal, 1, sens * Time.deltaTime);
            }
        }
        else
        {
            horizontal = (Mathf.Abs(horizontal) < dead) ? 0 : Mathf.MoveTowards(horizontal, 0, sens * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2)
        {

        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Debug.Log("touched");
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            else
            {
                foreach(var touch in Input.touches)
                {
                    if (touch.position.x < Camera.main.pixelWidth)
                        Debug.Log("");
                }
            }
        }
#endif
    }

    public bool GetJumpButtonDown()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#elif UNITY_ANDROID
#endif
    }

    public bool GetJumpButton()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#elif UNITY_ANDROID
#endif
    }

    public bool GetJumpButtonUp()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonUp(0) && Input.mousePosition.x > Camera.main.pixelWidth / 2;
#elif UNITY_ANDROID
#endif
    }
}
