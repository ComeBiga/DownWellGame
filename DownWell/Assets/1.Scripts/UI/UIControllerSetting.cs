using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerSetting : MonoBehaviour
{
    public Text text;
    [SerializeField] private int amount = 5;
    [SerializeField] private int minSize = 50;
    [SerializeField] private int maxSize = 100;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("ControllerSize")) PlayerPrefs.SetInt("ControllerSize", 60);

        text.text = PlayerPrefs.GetInt("ControllerSize").ToString();
    }

    public void SetSizeDown()
    {
        var size = PlayerPrefs.GetInt("ControllerSize") - amount;
        size = Mathf.Clamp(size, minSize, maxSize);
        PlayerPrefs.SetInt("ControllerSize", size);

        text.text = size.ToString();

        //if (InputManager.instance != null) InputManager.instance.SetControllerSize();

        Debug.Log($"SetSizeDown");
    }

    public void SetSizeUp()
    {
        var size = PlayerPrefs.GetInt("ControllerSize") + amount;
        size = Mathf.Clamp(size, minSize, maxSize);
        PlayerPrefs.SetInt("ControllerSize", size);

        text.text = size.ToString();

        //if (InputManager.instance != null) InputManager.instance.SetControllerSize();

        Debug.Log($"SetSizeUp");
    }
}
