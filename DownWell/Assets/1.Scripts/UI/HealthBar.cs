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
    bool gameStart;

    void Start()
    {
        gameStart = true;    
    }

    void Update()
    {
        if (!settingStop)
            heartSetting();
        PlayerHealthEvent();
    }

    void heartSetting()
    {
        if(gameStart)
        {
            for (int i = 0; i <= PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().MaxHealth; i++)
            {
                HpChild = Instantiate(heart, hp.transform.position, Quaternion.identity, hp.transform);
            }
            gameStart = false;
        }
        
        if(PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().CurrentHealth - hp.transform.childCount < 0)
            Destroy(hp.transform.GetChild(0).gameObject);
        else if(PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().CurrentHealth - hp.transform.childCount > 0)
            HpChild = Instantiate(heart, hp.transform.position, Quaternion.identity, hp.transform);
        
        settingStop = true;
    }

    public void UpdateBar()
    {
        settingStop = false;
    }

    public void PlayerHealthEvent()
    {
        PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().OnChangedHealth += UpdateBar;
    }
}
