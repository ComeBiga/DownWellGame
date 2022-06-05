using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoinPanel : MonoBehaviour
{
    [SerializeField] private Text coinText;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Coin")) PlayerPrefs.SetInt("Coin", 0);

        SetText();
    }

    public void Init()
    {
        PlayerPrefs.SetInt("Coin", 0);

        SetText();
    }

    public void SetText()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
}
