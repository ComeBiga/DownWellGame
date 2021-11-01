using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject hp;
    public GameObject heart;
    
    GameObject HpChild;

    bool settingStop;

    void Start()
    {
        
    }

    void Update()
    {
        if (!settingStop)
            heartSetting();
        PlayerHealthEvent();
    }

    void heartSetting()
    {
        for (int i = 0; i < PlayerManager.instance.player.GetComponent<PlayerHealth>().MaxHealth; i++)
        {
            HpChild = Instantiate(heart, hp.transform.position, Quaternion.identity, hp.transform);
        }
        settingStop = true;
    }

    public void UpdateBar()
    {
        Destroy(hp.transform.GetChild(0).gameObject);
    }

    public void PlayerHealthEvent()
    {
        PlayerManager.instance.player.GetComponent<PlayerHealth>().OnChangedHealth += UpdateBar;
    }
}
