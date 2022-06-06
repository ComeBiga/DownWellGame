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

    private CatDown.Input input;

    public bool mouseClick = false;
    public bool blockInput = false;
    public float horizontal = 0;
    public float sens = 10f;
    public float dead = .001f;

    public float Horizontal { get { return input.GetAxisHorizontal(); } }

    public GameObject controllerPanel;
    [Range(0, 100)] public int controllerSize = 100;
    public float controllerOffset = 30f;
    public float controllerPartition = 50f;

    void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (!mouseClick)
            input = new CatDown.InputPC();
        else
            input = new CatDown.InputPCTouch();
#elif UNITY_ANDROID
        input = new CatDown.InputAndroid();
        input.SetController(controllerPanel);
        
#endif

        input.Init(sens, dead);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (controllerPanel != null) controllerPanel.SetActive(false);
#elif UNITY_ANDROID
        if (!PlayerPrefs.HasKey("ControllerSize")) PlayerPrefs.SetInt("ControllerSize", 60);
        var csize = PlayerPrefs.GetInt("ControllerSize");

        input.SetPartition(controllerPartition);
        input.SetControllerSize(csize, controllerOffset);
        if(controllerPanel != null) controllerPanel.SetActive(true);  
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //if(!blockInput)
        //    MobileTouch();

        if(!blockInput) input.Update();
    }

    public bool GetJumpButtonDown()
    {
        return input.GetJumpButtonDown();
    }

    public bool GetJumpButton()
    {
        return input.GetJumpButton();
    }

    public bool GetJumpButtonUp()
    {
        return input.GetJumpButtonUp();
    }

    public void SetControllerSize()
    {
        var size = PlayerPrefs.GetInt("ControllerSize");
        input.SetControllerSize(size, controllerOffset);
    }
}
